using System;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.Messaging;
using Microsoft.Extensions.Logging;

namespace EvilDICOM.Network.Services
{
    public class NEventReportService
    {
        private DIMSEService _dms;

        public NEventReportService(DIMSEService dIMSEService)
        {
            this._dms = dIMSEService;
        }

        public void OnRequestReceived(NEventReportRequest req, Association asc)
        {
            asc.Logger.LogInformation("<-- DIMSE" + req.GetLogString());
            req.LogData(asc);
            asc.LastActive = DateTime.Now;
            var resp = new NEventReportResponse(req, Status.SUCCESS);
            _dms.RaiseDIMSERequestReceived(req, asc);
            PDataMessenger.Send(resp, asc);
        }

        public void OnResponseReceived(NEventReportResponse resp, Association asc)
        {
            asc.Logger.LogInformation("<-- DIMSE" + resp.GetLogString());
            resp.LogData(asc);
            asc.LastActive = DateTime.Now;
            _dms.RaiseDIMSEResponseReceived(resp, asc);
            asc.State = NetworkState.AWAITING_RELEASE;
        }
    }
}