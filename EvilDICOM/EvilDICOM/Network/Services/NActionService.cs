using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.Messaging;
using Microsoft.Extensions.Logging;

namespace EvilDICOM.Network.Services
{
    public class NActionService
    {
        private DIMSEService _dms;

        public NActionService(DIMSEService dIMSEService)
        {
            this._dms = dIMSEService;
        }


        public void OnRequestReceived(NActionRequest req, Association asc)
        {
            asc.Logger.LogInformation("<-- DIMSE" + req.GetLogString());
            req.LogData(asc);
            asc.LastActive = DateTime.Now;
            var resp = new NActionResponse(req, Status.SUCCESS);
            _dms.RaiseDIMSERequestReceived(req, asc);

            //STORAGE COMMITMENT PUSH
            if (req.RequestedSOPClassUID == AbstractSyntax.StorageCommitment_Push)
            {
                resp.Status = (ushort)Status.SUCCESS;
                PDataMessenger.Send(resp, asc);
                PerformStorageCommitment(req, asc);
            }
            else
            {
                //Abstract syntax not supported
                resp.Status = (ushort)Status.FAILURE_UNABLE_TO_PROCESS;
                PDataMessenger.Send(resp, asc);
            }
        }

        private async Task PerformStorageCommitment(NActionRequest req, Association asc)
        {
            asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN; // Don't read stream...Wait for task to complete
            asc.Logger.LogInformation("Delaying 1.5 seconds to perform Storage Commitment Query...");
            await Task.Delay(1500);
            _dms.StorageCommitmentService.OnRequestReceived(req, asc);
        }

        public void OnResponseReceived(NActionResponse resp, Association asc)
        {
            asc.Logger.LogInformation("<-- DIMSE" + resp.GetLogString());
            resp.LogData(asc);
            asc.LastActive = DateTime.Now;
            _dms.RaiseDIMSEResponseReceived(resp, asc);

            //STORAGE COMMITMENT PUSH
            if (resp.AffectedSOPClassUID == AbstractSyntax.StorageCommitment_Push)
            {
                asc.State = NetworkState.ASSOCIATION_ESTABLISHED_WAITING_ON_DATA;
            }
            else
            {
                //Abstract syntax not supported
                if (resp.Status != (ushort)Status.PENDING)
                    AssociationMessenger.SendReleaseRequest(asc);
            }

          
        }
    }
}
