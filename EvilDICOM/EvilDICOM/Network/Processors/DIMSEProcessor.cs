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
            if (dimse is CEchoRequest)
            {
                asc.ServiceClass.DIMSEService.CEchoRequestReceivedAction(dimse as CEchoRequest, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CStoreRequest)
            {
                asc.ServiceClass.DIMSEService.CStoreService.OnRequestRecieved(dimse as CStoreRequest, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CMoveRequest)
            {
                asc.ServiceClass.DIMSEService.CMoveRequestReceivedAction(dimse as CMoveRequest, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CGetRequest)
            {
                asc.ServiceClass.DIMSEService.CGetRequestReceivedAction(dimse as CGetRequest, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            //C-FIND
            if (dimse is CFindRequest)
            {
                asc.ServiceClass.DIMSEService.CFindService.OnRequestRecieved(dimse as CFindRequest, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CFindResponse)
            {
                asc.ServiceClass.DIMSEService.CFindService.OnResponseRecieved(dimse as CFindResponse, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CEchoResponse)
            {
                asc.ServiceClass.DIMSEService.CEchoResponseReceivedAction(dimse as CEchoResponse, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CStoreResponse)
            {
                asc.ServiceClass.DIMSEService.CStoreService.OnResponseRecieved(dimse as CStoreResponse, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CGetResponse)
            {
                asc.ServiceClass.DIMSEService.CGetResponseReceivedAction(dimse as CGetResponse, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
            if (dimse is CMoveResponse)
            {
                asc.ServiceClass.DIMSEService.CMoveResponseReceivedAction(dimse as CMoveResponse, asc);
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            }
        }
    }
}