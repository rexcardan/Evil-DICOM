using System;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.Messaging;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Services
{
    /// <summary>
    /// This class handles incoming DIMSE messaging. It logs the message, creates and sends the appropriate response
    /// </summary>
    public class DIMSEService
    {
        public DIMSEService()
        {
            SetDefaultActions();
        }

        public Action<CEchoRequest, Association> CEchoRequestReceivedAction { get; set; }
        public Action<CEchoResponse, Association> CEchoResponseReceivedAction { get; set; }
        public Action<CStoreRequest, Association> CStoreRequestReceivedAction { get; set; }
        public Func<DICOMObject, Association, bool> CStorePayloadAction { get; set; }
        public Action<CStoreResponse, Association> CStoreResponseReceivedAction { get; set; }
        public Action<CFindRequest, Association> CFindRequestReceivedAction { get; set; }
        public Action<CFindResponse, Association> CFindResponseReceivedAction { get; set; }
        public Action<CMoveRequest, Association> CMoveRequestReceivedAction { get; set; }
        public Action<CMoveResponse, Association> CMoveResponseReceivedAction { get; set; }

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
                RaiseDIMSERequestReceived<CEchoRequest>(cEchoReq, asc);
   
            };

            CEchoResponseReceivedAction = (cEchoRp, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cEchoRp.GetLogString());
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived<CEchoResponse>(cEchoRp, asc);
                AssociationMessenger.SendReleaseRequest(asc);
            };

            CFindResponseReceivedAction = (cFindResp, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cFindResp.GetLogString());
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived<CFindResponse>(cFindResp, asc);
                cFindResp.LogData(asc);
                if (cFindResp.Status != (ushort) Status.PENDING)
                {
                    AssociationMessenger.SendReleaseRequest(asc);
                }
            };

            CMoveRequestReceivedAction = (cMoveReq, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cMoveReq.GetLogString());
                cMoveReq.LogData(asc);
                asc.LastActive = DateTime.Now;
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                RaiseDIMSERequestReceived<CMoveRequest>(cMoveReq, asc);
                throw new NotImplementedException();
            };

            CMoveResponseReceivedAction = (cMoveRes, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cMoveRes.GetLogString());
                cMoveRes.LogData(asc);
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived<CMoveResponse>(cMoveRes, asc);
                if (cMoveRes.Status != (ushort) Status.PENDING)
                {
                    AssociationMessenger.SendReleaseRequest(asc);
                }
            };

            CStoreRequestReceivedAction = async (req, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + req.GetLogString());
                req.LogData(asc);
                asc.LastActive = DateTime.Now;
                asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
                var resp = new CStoreResponse(req, Status.SUCCESS);
                IDICOMElement syntax = req.Data.FindFirst(TagHelper.SOP​Class​UID);
                RaiseDIMSERequestReceived<CStoreRequest>(req, asc);

                if (syntax != null)
                {
                    //If can store (supported Abstract Syntax) - Try
                    if (asc.PresentationContexts.Any(p => p.Id == req.DataPresentationContextId))
                    {
                        try
                        {
                            bool success = CStorePayloadAction(req.Data, asc);
                            resp.Status = success ? resp.Status : (ushort)Status.FAILURE;
                            PDataMessenger.Send(resp, asc, asc.PresentationContexts.First(p => p.Id == req.DataPresentationContextId));
                        }
                        catch (Exception e)
                        {
                            resp.Status = (ushort) Status.FAILURE;
                            PDataMessenger.Send(resp, asc);
                        }
                    }
                    else
                    {
                        //Abstract syntax not supported
                        resp.Status = (ushort) Status.FAILURE;
                        PDataMessenger.Send(resp, asc);           
                    }
                }
            };

            CStoreResponseReceivedAction = (cStoreResp, asc) =>
            {
                asc.Logger.Log("<-- DIMSE" + cStoreResp.GetLogString());
                cStoreResp.LogData(asc);
                asc.LastActive = DateTime.Now;
                RaiseDIMSEResponseReceived<CStoreResponse>(cStoreResp, asc);
                if (cStoreResp.Status != (ushort)Status.PENDING)
                {
                    AssociationMessenger.SendReleaseRequest(asc);
                }
            };

            CStorePayloadAction = (dcm, asc) => { return true; };
        }

        //DICOM Object Received

        public delegate void DIMSERequestHandler<T>(T req, Association asc) where T : AbstractDIMSERequest;

        public delegate void DIMSEResponseHandler<T>(T req, Association asc) where T : AbstractDIMSEResponse;

        public delegate void LogHandler(string toLog);

        //---------------DIMSE REQUESTS----------------------

        public event DIMSERequestHandler<CEchoRequest> CEchoRequestReceived;
        public event DIMSERequestHandler<CFindRequest> CFindRequestReceived;
        public event DIMSERequestHandler<CMoveRequest> CMoveRequestReceived;
        public event DIMSERequestHandler<CStoreRequest> CStoreRequestReceived;

        private void RaiseDIMSERequestReceived<T>(T req, Association asc) where T : AbstractDIMSERequest
        {
            if (typeof(T) == typeof(CEchoRequest))
            {
                if (CEchoRequestReceived != null)
                {
                    CEchoRequestReceived(req as CEchoRequest, asc);
                }
            }
            if (typeof(T) == typeof(CFindRequest))
            {
                if (CFindRequestReceived != null)
                {
                    CFindRequestReceived(req as CFindRequest, asc);
                }
            }
            if (typeof(T) == typeof(CMoveRequest))
            {
                if (CMoveRequestReceived != null)
                {
                    CMoveRequestReceived(req as CMoveRequest, asc);
                }
            }
            if (typeof(T) == typeof(CStoreRequest))
            {
                if (CStoreRequestReceived != null)
                {
                    CStoreRequestReceived(req as CStoreRequest, asc);
                }
            }
        }

        //----------------DIMSE RESPONSES-------------------------

        public event DIMSEResponseHandler<CEchoResponse> CEchoResponseReceived;
        public event DIMSEResponseHandler<CFindResponse> CFindResponseReceived;
        public event DIMSEResponseHandler<CMoveResponse> CMoveResponseReceived;
        public event DIMSEResponseHandler<CStoreResponse> CStoreResponseReceived;

        private void RaiseDIMSEResponseReceived<T>(T resp, Association asc) where T : AbstractDIMSEResponse
        {
            if (typeof(T) == typeof(CEchoResponse))
            {
                if (CEchoResponseReceived != null)
                {
                    CEchoResponseReceived(resp as CEchoResponse, asc);
                }
            }
            if (typeof(T) == typeof(CFindResponse))
            {
                if (CFindResponseReceived != null)
                {
                    CFindResponseReceived(resp as CFindResponse, asc);
                }
            }
            if (typeof(T) == typeof(CMoveResponse))
            {
                if (CMoveResponseReceived != null)
                {
                    CMoveResponseReceived(resp as CMoveResponse, asc);
                }
            }
            if (typeof(T) == typeof(CStoreResponse))
            {
                if (CStoreResponseReceived != null)
                {
                    CStoreResponseReceived(resp as CStoreResponse, asc);
                }
            }
        }
    }
}