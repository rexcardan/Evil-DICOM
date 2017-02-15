using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Messaging;
using System.Net;
using System.Threading;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network
{
    public class DICOMSCU : DICOMServiceClass
    {
        public DICOMSCU(Entity ae) : base(ae) { }

        public void SendMessage(AbstractDIMSERequest dimse, Entity ae)
        {
            var client = new TcpClient();
            client.ConnectAsync(IPAddress.Parse(ae.IpAddress), ae.Port).Wait();
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(dimse, assoc);
            assoc.Listen();
        }

        public IEnumerable<CFindResponse> GetResponse(CFindRequest cFind, Entity ae)
        {
            var client = new TcpClient();
            client.ConnectAsync(IPAddress.Parse(ae.IpAddress), ae.Port).Wait();
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(cFind, assoc);
            List<CFindResponse> responses = new List<CFindResponse>();

            EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CFindResponse> action = null;
            action = (resp, asc) =>
             {
                 responses.Add(resp);
                 if (resp.Status != (ushort)Status.PENDING)
                 {
                     this.DIMSEService.CFindResponseReceived -= action;
                 }
             };
            this.DIMSEService.CFindResponseReceived += action;
            assoc.Listen();
            return responses;
        }

        public IEnumerable<CMoveResponse> GetResponse(CMoveRequest cMove, Entity ae)
        {
            var client = new TcpClient();
            client.ConnectAsync(IPAddress.Parse(ae.IpAddress), ae.Port).Wait();
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(cMove, assoc);
            List<CMoveResponse> responses = new List<CMoveResponse>();

            EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CMoveResponse> action = null;
            action = (resp, asc) =>
            {
                responses.Add(resp);
                if (resp.Status != (ushort)Status.PENDING)
                {
                    this.DIMSEService.CMoveResponseReceived -= action;
                }
            };
            this.DIMSEService.CMoveResponseReceived += action;
            assoc.Listen();
            return responses;
        }

        /// <summary>
        /// Sends a CEcho request to the input entity
        /// </summary>
        /// <param name="ae">the entity to send the request</param>
        /// <param name="msTimeout">how long to wait in milliseconds before a timeout</param>
        /// <returns>true if success, false if failure</returns>
        public bool Ping(Entity ae, int msTimeout = 3000)
        {
            var responseSuccess = false;
            AutoResetEvent ar = new AutoResetEvent(false);
            DIMSEService.CEchoResponseReceived += (res, asc) =>
            {
                responseSuccess = true;
                ar.Set();
            };
            SendMessage(new CEchoRequest(), ae);
            ar.WaitOne(msTimeout); //Give it 3 seconds
            return responseSuccess;
        }

        public CStoreRequest GenerateCStoreRequest(Core.DICOMObject toSend, ushort messageId = 1)
        {
            var sel = toSend.GetSelector();
            var cStoreReq = new CStoreRequest();
            cStoreReq.AffectedSOPClassUID = sel.SOPClassUID.Data;
            cStoreReq.Priority = 1;
            cStoreReq.Data = toSend;
            cStoreReq.MessageID = messageId;
            cStoreReq.AffectedSOPInstanceUID = sel.SOPInstanceUID.Data;
            cStoreReq.MoveOrigAETitle = this.ApplicationEntity.AeTitle;
            cStoreReq.MoveOrigMessageID = messageId;
            return cStoreReq;
        }

        /// <summary>
        /// Emits a CMove operation to an entity which moves an image from the entity to the specified AETitle
        /// </summary>
        /// <param name="scp">the provider which will perform the move</param>
        /// <param name="sopUid">the uid of the image to be moved</param>
        /// <param name="patientId">the patient id of the image</param>
        /// <param name="toAETite">the entity title which will receive the image</param>
        /// <param name="msgId">the message id</param>
        /// <returns>the move response</returns>
        public CMoveResponse SendCMoveImage(Entity daemon, CFindImageIOD iod, string toAETite, ref ushort msgId)
        {
            ManualResetEvent mr = new ManualResetEvent(false);
            CMoveResponse resp = null;
            var cr = new EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CMoveResponse>((res, asc) =>
            {
                if (!(res.Status == (ushort)Status.PENDING))
                {
                    mr.Set();
                }
                resp = res;
            });
            var result = new CMoveIOD() { QueryLevel = QueryLevel.IMAGE, SOPInstanceUID = iod.SOPInstanceUID, PatientId = iod.PatientId, StudyInstanceUID = iod.StudyInstanceUID, SeriesInstanceUID = iod.SeriesInstanceUID };
            var request = new CMoveRequest(result, toAETite, Root.STUDY, EvilDICOM.Core.Enums.Priority.MEDIUM, msgId);
            this.DIMSEService.CMoveResponseReceived += cr;
            this.SendMessage(request, daemon);
            mr.WaitOne();
            this.DIMSEService.CMoveResponseReceived -= cr;
            msgId += 2;
            return resp;
        }

        /// <summary>
        /// Emits a CMove operation to an entity which moves an image from the entity to the specified AETitle
        /// </summary>
        /// <param name="scp">the provider which will perform the move</param>
        /// <param name="sopUid">the uid of the image to be moved</param>
        /// <param name="patientId">the patient id of the image</param>
        /// <param name="toAETite">the entity title which will receive the image</param>
        /// <param name="msgId">the message id</param>
        /// <returns>the move response</returns>
        public CMoveResponse SendCMoveImage(Entity daemon, string patientId, string sopInstanceUid, string toAETite, ref ushort msgId)
        {
            var cfindIod = new CFindImageIOD() { PatientId = patientId, SOPInstanceUID = sopInstanceUid };
            return SendCMoveImage(daemon, cfindIod, toAETite, ref msgId);
        }
    }
}
