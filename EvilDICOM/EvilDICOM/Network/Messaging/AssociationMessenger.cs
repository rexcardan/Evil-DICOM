#region

using System.Linq;
using EvilDICOM.Network.Associations.PDUs;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;

#endregion

namespace EvilDICOM.Network.Messaging
{
    public class AssociationMessenger
    {
        public static void SendAccept(Accept accept, Association asc)
        {
            var stream = asc.Stream;
            var message = accept.Write();
            asc.Logger.Log("-->" + accept);
            stream.Write(message, 0, message.Length);
        }

        public static void SendReject(Association asc)
        {
            var rej = new Reject
            {
                Result = RejectResult.REJECTED_PERMANENT,
                Reason = (byte) RejectReason_SCU.NO_REASON_GIVEN
            };
            asc.Logger.Log("-->" + rej);
            var rejBytes = rej.Write();
            asc.Stream.Write(rejBytes, 0, rejBytes.Length);
        }

        ///// <summary>
        ///// Sends a request for DIMSE association to a DICOM service
        ///// </summary>
        ///// <param name="asc">the underlying association</param>
        ///// <param name="abstractSyntax">the proposed abstract syntaxes (what should the service be able to do)</param>
        public static void SendRequest(Association asc, params string[] abstractSyntaxes)
        {
            var request = new Request
            {
                CalledEntityTitle = asc.AeTitle,
                CallingEntityTitle = asc.ServiceClass.ApplicationEntity.AeTitle
            };
            abstractSyntaxes.Select((a, i) => new {a, i})
                .ToList()
                .ForEach(a =>
                {
                    var pres = new PresentationContext
                    {
                        AbstractSyntax = a.a,
                        Id = a.i * 2 + 1, //Convention of odd numbers
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

        public static void SendReleaseRequest(Association asc)
        {
            var req = new ReleaseRequest();
            asc.State = NetworkState.AWAITING_RELEASE_RESPONSE;
            asc.Logger.Log("-->" + req);
            var message = req.Write();
            if (asc.Stream.CanWrite)
                asc.Stream.Write(message, 0, message.Length);
        }

        public static void SendReleaseResponse(Association asc)
        {
            var resp = new ReleaseResponse();
            asc.Logger.Log("-->" + resp);
            var message = resp.Write();
            if (asc.Stream.CanWrite)
                asc.Stream.Write(message, 0, message.Length);
        }

        public static void SendAbort(Association asc, AbortSource abortSource = AbortSource.DICOM_UL_SERV_PROVIDER,
            AbortReason reason = AbortReason.REASON_NOT_SPECIFIED)
        {
            if (asc.Stream.CanWrite)
            {
                var abort = new Abort {Source = abortSource, Reason = reason};
                var message = abort.Write();
                asc.Stream.Write(message, 0, message.Length);
            }
        }
    }
}