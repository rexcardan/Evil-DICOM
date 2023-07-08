#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.SCUOps;
using EvilDICOM.Core.Enums;
using EvilDICOM.Network.Helpers;
using System.Linq;
using System.Text;
using EvilDICOM.Network.Messaging;
using static EvilDICOM.Network.Services.AssociationService;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Network
{
    public class DICOMSCU : DICOMServiceClass
    {

        public int IdleTimeout { get; set; } = 60000; // 60 sec
        public int ConnectionTimeout { get; set; } = 3000; // 3 sec

        public DICOMSCU(Entity ae) : base(ae)
        {
#if NETCOREAPP

            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
#endif
        }

        /// <summary>
        /// Sends a message to an entity
        /// </summary>
        /// <param name="dimse">the message to send</param>
        /// <param name="ae">the entity to send the message</param>
        /// <returns>true if message send was success</returns>
        public SendStatus SendMessage(AbstractDIMSERequest dimse, Entity ae)
        {
            using (var client = new TcpClient())
            {
                var status = new SendStatus();
                try
                {
                    var connectionResult = client.BeginConnect(IPAddress.Parse(ae.IpAddress), ae.Port, null, null);
                    var completed = connectionResult.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(ConnectionTimeout));

                    if (completed && !client.Client.Connected)
                    {
                        status.DidConnect = false;
                        return status;
                    }

                    //Connected. Attempt association
                    status.DidConnect = true;
                    var assoc = new Association(this, client) { AeTitle = ae.AeTitle };

                    AssociationRejectedHandler rejectedHandler = (rej, asc) =>
                    {
                        status.WasRejected = true;
                        status.Reason = Enum.GetName(typeof(RejectReason_SCU), rej.Reason);
                    };

                    this.AssociationService.AssociationRejectionReceived += rejectedHandler;
                    PDataMessenger.Send(dimse, assoc);
                    assoc.Listen(TimeSpan.FromMilliseconds(IdleTimeout));
                    this.AssociationService.AssociationRejectionReceived -= rejectedHandler;
                    return status;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not connect to {ae.AeTitle} @{ae.IpAddress}:{ae.Port}");
                    Logger.LogError($"{e.ToString()}");
                    return status;
                }
            }
        }

        /// <summary>
        /// Sends a message to an entity and guarantees the message will be sent on a 
        /// specific port (set by the entity settings)
        /// </summary>
        /// <param name="dimse">the message to send</param>
        /// <param name="ae">the entity to send the message</param>
        /// <returns>true if message send was success</returns>
        public bool SendMessageForcePort(AbstractDIMSERequest dimse, Entity ae)
        {
            var (ipLocalEndPoint, success) =
                IpHelper.VerifyIPAddress(this.ApplicationEntity.IpAddress, this.ApplicationEntity.Port);
            if (!success) { return false; }

            using (var client = new TcpClient(ipLocalEndPoint))
            {
                try
                {
                    var connectionResult = client.BeginConnect(IPAddress.Parse(ae.IpAddress), ae.Port, null, null);
                    success = connectionResult.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(ConnectionTimeout));
                    if (!success)
                    {
                        throw new TimeoutException($"Couldn't connect to {IPAddress.Parse(ae.IpAddress)}:{ae.Port} within {ConnectionTimeout} ms!");
                    }
                    var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
                    PDataMessenger.Send(dimse, assoc);
                    assoc.Listen();
                    return true;
                }
                catch (Exception e)
                {
                    Logger.LogError($"Could not connect to {ae.AeTitle} @{ae.IpAddress}:{ae.Port}");
                    Logger.LogError($"{e.ToString()}");
                    return false;
                }
            }
        }

        public CFinder GetCFinder(Entity callingEntity)
        {
            return new CFinder(this, callingEntity);
        }

        public CMover GetCMover(Entity callingEntity)
        {
            return new CMover(this, callingEntity);
        }

        public CStorer GetCStorer(Entity callingEnity)
        {
            return new CStorer(this, callingEnity);
        }

        public T GetResponse<T, U>(U request, Entity e, ref ushort msgId) where U : AbstractDIMSERequest where T : AbstractDIMSEResponse
        {
            return GetResponses<T, U>(request, e, ref msgId).LastOrDefault();
        }

        public IEnumerable<T> GetResponses<T, U>(U request, Entity e, ref ushort msgId) where U : AbstractDIMSERequest where T : AbstractDIMSEResponse
        {
            System.DateTime lastContact = System.DateTime.Now;
            int msWait = IdleTimeout;

            var mr = new ManualResetEvent(false);
            List<T> responses = new List<T>();
            var cr = new Services.DIMSEService.DIMSEResponseHandler<T>((res, asc) =>
            {
                asc.IdleClock.Restart();
                lastContact = System.DateTime.Now;
                responses.Add(res);
                if (res.Status != (ushort)Status.PENDING)
                    mr.Set();
                else
                {
                    mr.Reset();
                }
            });

            DIMSEService.Subscribe(cr);

            bool clientConnected;
            //Only wait if message is successful
            var sendStatus = SendMessage(request, e);
            if (clientConnected = sendStatus.WasAccepted)
            {
                //Wait for final response
                mr.WaitOne(msWait);
            }
            DIMSEService.Unsubscribe(cr);
            msgId += 2;

            if (!sendStatus.DidConnect) { throw new Exception($"Could not connect to remote endpoint {e}"); }
            if (sendStatus.WasRejected) { throw new Exception($"Connected, but association refused by {e} : {sendStatus.Reason}"); }
            return responses;
        }

        /// <summary>
        /// Sends a CEcho request to the input entity
        /// </summary>
        /// <param name="ae">the entity to send the request</param>
        /// <param name="msTimeout">how long to wait in milliseconds before a timeout</param>
        /// <returns>true if success, false if failure</returns>
        public bool Ping(Entity ae, int msTimeout = 0)
        {
            msTimeout = (msTimeout == 0) ? IdleTimeout : msTimeout;

            var responseSuccess = false;
            var ar = new AutoResetEvent(false);
            DIMSEService.CEchoResponseReceived += (res, asc) =>
            {
                responseSuccess = true;
                ar.Set();
            };
            SendMessage(new CEchoRequest(), ae);
            ar.WaitOne(msTimeout); //Give it 3 seconds
            return responseSuccess;
        }

        /// <summary>
        /// Emits a CMove operation to an entity which moves an image from the entity to the specified AETitle
        /// </summary>
        /// <param name="scp">the provider which will perform the move</param>
        /// <param name="sopUid">the uid of the image to be moved</param>
        /// <param name="patientId">the patient id of the image</param>
        /// <param name="toAETitle">the entity title which will receive the image</param>
        /// <param name="msgId">the message id</param>
        /// <returns>the move response</returns>
        public CGetResponse SendGetImage(Entity daemon, CFindImageIOD iod, ref ushort msgId)
        {
            var mr = new ManualResetEvent(false);
            CGetResponse resp = null;
            var cr = new Services.DIMSEService.DIMSEResponseHandler<CGetResponse>((res, asc) =>
            {
                if (!(res.Status == (ushort)Status.PENDING))
                    mr.Set();
                resp = res;
            });

            iod.QueryLevel = QueryLevel.IMAGE;

            var request = new CGetRequest(iod, Root.STUDY, Core.Enums.Priority.MEDIUM, msgId);
            DIMSEService.CGetResponseReceived += cr;
            SendMessage(request, daemon);
            mr.WaitOne();
            DIMSEService.CGetResponseReceived -= cr;
            msgId += 2;
            return resp;
        }
    }
}