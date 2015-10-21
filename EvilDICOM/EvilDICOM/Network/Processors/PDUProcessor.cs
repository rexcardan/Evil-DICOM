using EvilDICOM.Network.Associations.PDUs;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs;

namespace EvilDICOM.Network.Processors
{
    public class PDUProcessor
    {
        public static void Process(IMessage message, Association asc)
        {
            var pdu = message.DynPayload as IPDU;
            switch (pdu.Type)
            {
                    //-----------------------------A-REQUEST------------------------------
                case PDUType.A_ASSOC_REQUEST:
                    var assRq = pdu as Request;
                    asc.ServiceClass.AssociationService.AssociationRequestReceivedAction(assRq, asc);
                    break;

                    //-----------------------------A-ACCEPT------------------------------
                case PDUType.A_ASSOC_ACCEPT:
                    var acc = pdu as Accept;
                    asc.ServiceClass.AssociationService.AssociationAcceptanceReceivedAction(acc, asc);
                    break;

                    //-----------------------------A-REJECT------------------------------
                case PDUType.A_ASSOC_REJECT:

                    var rej = pdu as Reject;
                    asc.ServiceClass.AssociationService.AssociationRejectAction(rej, asc);
                    break;

                    //-----------------------------A-ABORT------------------------------
                case PDUType.A_ABORT:

                    var abort = pdu as Abort;
                    asc.ServiceClass.AssociationService.AbortReceivedAction(abort, asc);
                    break;

                    //-----------------------------A-RELEASE-RQ------------------------------
                case PDUType.A_RELEASE_REQUEST:
                    var relReq = pdu as ReleaseRequest;
                    asc.ServiceClass.AssociationService.ReleaseRequestReceivedAction(relReq, asc);
                    break;

                    //-----------------------------A-RELEASE-RP------------------------------
                case PDUType.A_RELEASE_RESPONSE:
                    var relRes = pdu as ReleaseResponse;
                    asc.ServiceClass.AssociationService.ReleaseResponseAction(relRes, asc);
                    break;
            }
        }
    }
}