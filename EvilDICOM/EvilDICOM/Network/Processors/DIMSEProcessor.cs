#region

using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.Processors
{
    public class DIMSEProcessor
    {
        public static void Process(AbstractDIMSE dimse, Association asc)
        {
            asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            if (dimse is CEchoRequest)
            {
                asc.ServiceClass.DIMSEService.CEchoRequestReceivedAction(dimse as CEchoRequest, asc);
            }
            if (dimse is CStoreRequest)
            {
                asc.ServiceClass.DIMSEService.CStoreService.OnRequestRecieved(dimse as CStoreRequest, asc);
            }
            if (dimse is CMoveRequest)
            {
                asc.ServiceClass.DIMSEService.CMoveRequestReceivedAction(dimse as CMoveRequest, asc);
            }
            if (dimse is CGetRequest)
            {
                asc.ServiceClass.DIMSEService.CGetRequestReceivedAction(dimse as CGetRequest, asc);
            }
            //C-FIND
            if (dimse is CFindRequest)
            {
                asc.ServiceClass.DIMSEService.CFindService.OnRequestRecieved(dimse as CFindRequest, asc);
            }
            if (dimse is CFindResponse)
            {
                asc.ServiceClass.DIMSEService.CFindService.OnResponseRecieved(dimse as CFindResponse, asc);
            }
            if (dimse is CEchoResponse)
            {
                asc.ServiceClass.DIMSEService.CEchoResponseReceivedAction(dimse as CEchoResponse, asc);
            }
            if (dimse is CStoreResponse)
            {
                asc.ServiceClass.DIMSEService.CStoreService.OnResponseRecieved(dimse as CStoreResponse, asc);
            }
            if (dimse is CGetResponse)
            {
                asc.ServiceClass.DIMSEService.CGetResponseReceivedAction(dimse as CGetResponse, asc);
            }
            if (dimse is CMoveResponse)
            {
                asc.ServiceClass.DIMSEService.CMoveResponseReceivedAction(dimse as CMoveResponse, asc);
            }
            if (dimse is NActionRequest)
            {
                asc.ServiceClass.DIMSEService.NActionService.OnRequestReceived(dimse as NActionRequest, asc);
            }
            if (dimse is NActionResponse)
            {
                asc.ServiceClass.DIMSEService.NActionService.OnResponseReceived(dimse as NActionResponse, asc);
            }
            if (dimse is NEventReportRequest)
            {
                asc.ServiceClass.DIMSEService.NEventReportService.OnRequestReceived(dimse as NEventReportRequest, asc);
            }
            if (dimse is NEventReportResponse)
            {
                asc.ServiceClass.DIMSEService.NEventReportService.OnResponseReceived(dimse as NEventReportResponse, asc);
            }
        }
    }
}