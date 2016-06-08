using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;
using System.IO;
using EvilDICOM.Network.Associations.PDUs;

namespace EvilDICOM.Network.Messaging
{
    /// <summary>
    /// Class AssociationMessenger.
    /// </summary>
    public class AssociationMessenger
    {

        /// <summary>
        /// Sends the accept.
        /// </summary>
        /// <param name="accept">The accept.</param>
        /// <param name="asc">The asc.</param>
        public static void SendAccept(Accept accept, Association asc)
        {
            var stream = asc.Stream;
            byte[] message = accept.Write();
            asc.Logger.Log("-->" + accept);
            stream.Write(message, 0, message.Length);
        }

        /// <summary>
        /// Sends the reject.
        /// </summary>
        /// <param name="asc">The asc.</param>
        public static void SendReject(Association asc)
        {
            var rej = new Reject
            {
                Result = RejectResult.REJECTED_PERMANENT,
                Reason = (byte) RejectReason_SCU.NO_REASON_GIVEN
            };
            asc.Logger.Log("-->" + rej);
            byte[] rejBytes = rej.Write();
            asc.Stream.Write(rejBytes, 0, rejBytes.Length);
        }

        ///// <summary>
        ///// Sends a request for DIMSE association to a DICOM service
        ///// </summary>
        ///// <param name="asc">the underlying association</param>
        ///// <param name="abstractSyntax">the proposed abstract syntaxes (what should the service be able to do)</param>
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="asc">The asc.</param>
        /// <param name="abstractSyntaxes">The abstract syntaxes.</param>
        public static void SendRequest(Association asc, params string[] abstractSyntaxes)
        {
            var request = new Request {CalledEntityTitle = asc.AeTitle, CallingEntityTitle = asc.ServiceClass.ApplicationEntity.AeTitle};
            abstractSyntaxes.Select((a, i) => new {a, i})
                .ToList()
                .ForEach(a =>
                {
                    var pres = new PresentationContext
                    {
                        AbstractSyntax = a.a,
                        Id = a.i*2 + 1, //Convention of odd numbers
                        Reason = PresentationContextReason.ACCEPTANCE,
                        TransferSyntaxes = asc.ServiceClass.SupportedTransferSyntaxes
                    };
                    request.PresentationContexts.Add(pres);
                });
            asc.PresentationContexts.AddRange(request.PresentationContexts);
            request.UserInfo = new UserInfo();
            asc.Logger.Log("--> " + request);
            asc.SendMessage(request);
        }

        /// <summary>
        /// Sends the release request.
        /// </summary>
        /// <param name="asc">The asc.</param>
        public static void SendReleaseRequest(Association asc)
        {
            var req = new ReleaseRequest();
            asc.State = NetworkState.AWAITING_RELEASE_RESPONSE;
            asc.Logger.Log("-->" + req);
            byte[] message = req.Write();
            if ( asc.Stream.CanWrite)
            {
                asc.Stream.Write(message, 0, message.Length);
            }
           
        }

        /// <summary>
        /// Sends the release response.
        /// </summary>
        /// <param name="asc">The asc.</param>
        public static void SendReleaseResponse(Association asc)
        {
            var resp = new ReleaseResponse();
            asc.Logger.Log("-->" + resp);
            byte[] message = resp.Write();
            if (asc.Stream.CanWrite)
            {
                asc.Stream.Write(message, 0, message.Length);
            }
        }

        /// <summary>
        /// Sends the abort.
        /// </summary>
        /// <param name="asc">The asc.</param>
        /// <param name="abortSource">The abort source.</param>
        /// <param name="reason">The reason.</param>
        public static void SendAbort(Association asc, AbortSource abortSource = AbortSource.DICOM_UL_SERV_PROVIDER, AbortReason reason = AbortReason.REASON_NOT_SPECIFIED)
        {
            if (asc.Stream.CanWrite)
            {
                var abort = new Abort { Source = abortSource, Reason = reason };
                var message = abort.Write();
                asc.Stream.Write(message, 0, message.Length);
            }
        }
    }
}