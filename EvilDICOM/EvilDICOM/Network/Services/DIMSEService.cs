#region

using System;
using System.Linq;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.Messaging;

#endregion

namespace EvilDICOM.Network.Services
{
    /// <summary>
    /// This class handles incoming DIMSE messaging. It logs the message, creates and sends the appropriate response
    /// </summary>
    public class DIMSEService
    {
        //DICOM Object Received

        public delegate void DIMSERequestHandler<T>(T req, Association asc) where T : AbstractDIMSERequest;

        public delegate void DIMSEResponseHandler<T>(T req, Association asc) where T : AbstractDIMSEResponse;

        public delegate void LogHandler(string toLog);

        public DIMSEService()
        {
            this.CStoreService = new CStoreService(this);
            SetDefaultActions();
        }

        public CStoreService CStoreService { get; private set; }

        public Action<CEchoRequest, Association> CEchoRequestReceivedAction { get; private set; }
        public Action<CEchoResponse, Association> CEchoResponseReceivedAction { get; private set; }
        public Action<CFindRequest, Association> CFindRequestReceivedAction { get; private set; }
        public Action<CFindResponse, Association> CFindResponseReceivedAction { get; private set; }
        public Action<CMoveRequest, Association> CMoveRequestReceivedAction { get; set; }
        public Action<CMoveResponse, Association> CMoveResponseReceivedAction { get; private set; }
        public Action<CGetRequest, Association> CGetRequestReceivedAction { get; private set; }
        public Action<CGetResponse, Association> CGetResponseReceivedAction { get; private set; }


        private void SetDefaultActions()
        {
            
            CEchoRequestReceivedAction = (cEchoReq, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cEchoReq.GetLogString());
                if (!asc.ServiceClass.SupportedAbstractSyntaxes.Contains(AbstractSyntax.VERIFICATION)) return;
                asc.LastActive = DateTime.Now;
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                var response = new CEchoResponse(cEchoReq, Status.SUCCESS);
                PDataMessenger.Send(response, asc);
                RaiseDIMSERequestReceived(cEchoReq, asc);
            };

            CEchoResponseReceivedAction = (cEchoRp, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cEchoRp.GetLogString());
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived(cEchoRp, asc);
                AssociationMessenger.SendReleaseRequest(asc);
            };

            CGetRequestReceivedAction = (cGetReq, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cGetReq.GetLogString());
                cGetReq.LogData(asc);
                asc.LastActive = DateTime.Now;
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                RaiseDIMSERequestReceived(cGetReq, asc);
                throw new NotImplementedException();
            };

            CGetResponseReceivedAction = (cGetRes, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cGetRes.GetLogString());
                cGetRes.LogData(asc);
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived(cGetRes, asc);
                if (cGetRes.Status != (ushort)Status.PENDING)
                    AssociationMessenger.SendReleaseRequest(asc);
            };

            CFindResponseReceivedAction = (cFindResp, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cFindResp.GetLogString());
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived(cFindResp, asc);
                cFindResp.LogData(asc);
                if (cFindResp.Status != (ushort)Status.PENDING)
                    AssociationMessenger.SendReleaseRequest(asc);
            };

            CMoveRequestReceivedAction = (cMoveReq, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cMoveReq.GetLogString());
                cMoveReq.LogData(asc);
                asc.LastActive = DateTime.Now;
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                RaiseDIMSERequestReceived(cMoveReq, asc);
                throw new NotImplementedException();
            };

            CMoveResponseReceivedAction = (cMoveRes, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cMoveRes.GetLogString());
                cMoveRes.LogData(asc);
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived(cMoveRes, asc);
                if (cMoveRes.Status != (ushort)Status.PENDING)
                    AssociationMessenger.SendReleaseRequest(asc);
            };


        }

        public void Subscribe<T>(DIMSEResponseHandler<T> cr) where T : AbstractDIMSEResponse
        {
            if (typeof(T) == typeof(CEchoResponse))
                CEchoResponseReceived += (cr as DIMSEResponseHandler<CEchoResponse>);
            if (typeof(T) == typeof(CFindResponse))
                CFindResponseReceived += (cr as DIMSEResponseHandler<CFindResponse>);
            if (typeof(T) == typeof(CMoveResponse))
                CMoveResponseReceived += (cr as DIMSEResponseHandler<CMoveResponse>);
            if (typeof(T) == typeof(CStoreResponse))
                CStoreResponseReceived += (cr as DIMSEResponseHandler<CStoreResponse>);
            if (typeof(T) == typeof(CGetResponse))
                CGetResponseReceived += (cr as DIMSEResponseHandler<CGetResponse>);
        }

        public void Unsubscribe<T>(DIMSEResponseHandler<T> cr) where T : AbstractDIMSEResponse
        {
            if (typeof(T) == typeof(CEchoResponse))
                CEchoResponseReceived -= (cr as DIMSEResponseHandler<CEchoResponse>);
            if (typeof(T) == typeof(CFindResponse))
                CFindResponseReceived -= (cr as DIMSEResponseHandler<CFindResponse>);
            if (typeof(T) == typeof(CMoveResponse))
                CMoveResponseReceived -= (cr as DIMSEResponseHandler<CMoveResponse>);
            if (typeof(T) == typeof(CStoreResponse))
                CStoreResponseReceived -= (cr as DIMSEResponseHandler<CStoreResponse>);
            if (typeof(T) == typeof(CGetResponse))
                CGetResponseReceived -= (cr as DIMSEResponseHandler<CGetResponse>);
        }

        public void Subscribe<T>(DIMSERequestHandler<T> cr) where T : AbstractDIMSERequest
        {
            if (typeof(T) == typeof(CEchoRequest))
                CEchoRequestReceived += (cr as DIMSERequestHandler<CEchoRequest>);
            if (typeof(T) == typeof(CFindRequest))
                CFindRequestReceived += (cr as DIMSERequestHandler<CFindRequest>);
            if (typeof(T) == typeof(CMoveRequest))
                CMoveRequestReceived += (cr as DIMSERequestHandler<CMoveRequest>);
            if (typeof(T) == typeof(CStoreRequest))
                CStoreRequestReceived += (cr as DIMSERequestHandler<CStoreRequest>);
            if (typeof(T) == typeof(CGetRequest))
                CGetRequestReceived += (cr as DIMSERequestHandler<CGetRequest>);
        }

        public void Unsubscribe<T>(DIMSERequestHandler<T> cr) where T : AbstractDIMSERequest
        {
            if (typeof(T) == typeof(CEchoRequest))
                CEchoRequestReceived -= (cr as DIMSERequestHandler<CEchoRequest>);
            if (typeof(T) == typeof(CFindRequest))
                CFindRequestReceived -= (cr as DIMSERequestHandler<CFindRequest>);
            if (typeof(T) == typeof(CMoveRequest))
                CMoveRequestReceived -= (cr as DIMSERequestHandler<CMoveRequest>);
            if (typeof(T) == typeof(CStoreRequest))
                CStoreRequestReceived -= (cr as DIMSERequestHandler<CStoreRequest>);
            if (typeof(T) == typeof(CGetRequest))
                CGetRequestReceived -= (cr as DIMSERequestHandler<CGetRequest>);
        }

        //---------------DIMSE REQUESTS----------------------

        public event DIMSERequestHandler<CEchoRequest> CEchoRequestReceived;
        public event DIMSERequestHandler<CFindRequest> CFindRequestReceived;
        public event DIMSERequestHandler<CMoveRequest> CMoveRequestReceived;
        public event DIMSERequestHandler<CStoreRequest> CStoreRequestReceived;
        public event DIMSERequestHandler<CGetRequest> CGetRequestReceived;

        internal void RaiseDIMSERequestReceived<T>(T req, Association asc) where T : AbstractDIMSERequest
        {
            if (typeof(T) == typeof(CEchoRequest))
                CEchoRequestReceived?.Invoke(req as CEchoRequest, asc);
            if (typeof(T) == typeof(CFindRequest))
                CFindRequestReceived?.Invoke(req as CFindRequest, asc);
            if (typeof(T) == typeof(CMoveRequest))
                CMoveRequestReceived?.Invoke(req as CMoveRequest, asc);
            if (typeof(T) == typeof(CStoreRequest))
                CStoreRequestReceived?.Invoke(req as CStoreRequest, asc);
            if (typeof(T) == typeof(CGetRequest))
                CGetRequestReceived?.Invoke(req as CGetRequest, asc);
        }

        //----------------DIMSE RESPONSES-------------------------

        public event DIMSEResponseHandler<CEchoResponse> CEchoResponseReceived;
        public event DIMSEResponseHandler<CFindResponse> CFindResponseReceived;
        public event DIMSEResponseHandler<CMoveResponse> CMoveResponseReceived;
        public event DIMSEResponseHandler<CStoreResponse> CStoreResponseReceived;
        public event DIMSEResponseHandler<CGetResponse> CGetResponseReceived;

        internal void RaiseDIMSEResponseReceived<T>(T resp, Association asc) where T : AbstractDIMSEResponse
        {
            if (typeof(T) == typeof(CEchoResponse))
                CEchoResponseReceived?.Invoke(resp as CEchoResponse, asc);
            if (typeof(T) == typeof(CFindResponse))
                CFindResponseReceived?.Invoke(resp as CFindResponse, asc);
            if (typeof(T) == typeof(CMoveResponse))
                CMoveResponseReceived?.Invoke(resp as CMoveResponse, asc);
            if (typeof(T) == typeof(CStoreResponse))
                CStoreResponseReceived?.Invoke(resp as CStoreResponse, asc);
            if (typeof(T) == typeof(CGetResponse))
                CGetResponseReceived?.Invoke(resp as CGetResponse, asc);
        }
    }
}