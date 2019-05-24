using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Services
{
    /// <summary>
    /// A implementation of a C-FIND service, where C-FIND Requests can be made. To make it work, you
    /// must connect the FindStudyService, FindSeriesService, or FindImageService and connect it to this.
    /// The idea is you would have a database or algorithm which could implement the simple method provided.
    /// This just routes all the DICOM communication
    /// </summary>
    public class CFindService
    {
        private DIMSEService dms;

        public CFindService(DIMSEService dIMSEService)
        {
            this.dms = dIMSEService;
        }

        //The only action that can be set outside of the class
        public Func<DICOMObject, Association, bool> CStorePayloadAction { get; set; }

        public void OnRequestRecieved(CFindRequest req, Association asc)
        {
            asc.Logger.Log("<-- DIMSE" + req.GetLogString());
            req.LogData(asc);
            asc.LastActive = DateTime.Now;
            asc.IdleClock.Restart();
            asc.State = NetworkState.TRANSPORT_CONNECTION_OPEN;
            var resp = new CFindResponse(req, Status.SUCCESS);
            dms.RaiseDIMSERequestReceived(req, asc);
            var results = RetrieveResults(req);

            if(results != null) {
                foreach (var result in results)
                {
                    resp.Data = new DICOMObject(result.Elements);
                    resp.Status = (ushort)Status.PENDING;
                    resp.GroupLength = (uint)GroupWriter.WriteGroupBytes(new DICOMObject(resp.Elements.Skip(1).ToList()),
                        new DICOMIOSettings(), "0000").Length;
                    PDataMessenger.Send(resp, asc,
                               asc.PresentationContexts.First(p => p.Id == req.DataPresentationContextId));
                }
                //Finish
                resp.Status = results.Any() ? (ushort)Status.SUCCESS : (ushort)Status.FAILURE_UNABLE_TO_FIND;
                resp.Data = null;
                resp.GroupLength = (uint)GroupWriter.WriteGroupBytes(new DICOMObject(resp.Elements.Skip(1).ToList()),
                    new DICOMIOSettings(), "0000").Length;
                PDataMessenger.Send(resp, asc);
            }
            else
            {
                resp.Status = (ushort)Status.FAILURE;
                resp.Data = null;
                resp.GroupLength = (uint)GroupWriter.WriteGroupBytes(new DICOMObject(resp.Elements.Skip(1).ToList()),
                    new DICOMIOSettings(), "0000").Length;
                PDataMessenger.Send(resp, asc);
            }
        }

        /// <summary>
        /// Parses the request and modifies the base response with the results
        /// </summary>
        /// <param name="req"></param>
        /// <param name="resp"></param>
        private IEnumerable<AbstractDIMSEIOD> RetrieveResults(CFindRequest req)
        {
            var keys = req.Data.Elements.Where(e => e.DData != null).ToList();
            if (req.Data.GetSelector().QueryRetrieveLevel?.Data == "STUDY")
            {
                if (FindStudyService != null)
                {
                    foreach(var studyIod in FindStudyService.RetrieveMatches(keys))
                    {
                        yield return studyIod;
                    }
                }
            }
            else if (req.Data.GetSelector().QueryRetrieveLevel?.Data == "SERIES")
            {
                if (FindSeriesService != null)
                {
                    foreach (var series in FindSeriesService.RetrieveMatches(keys))
                    {
                        yield return series;
                    }
                }
            }
            else if (req.Data.GetSelector().QueryRetrieveLevel?.Data == "IMAGE")
            {
                if (FindImageService != null)
                {
                    foreach (var image in FindImageService.RetrieveMatches(keys))
                    {
                        yield return image;
                    }
                }
            }
        }

        public void OnResponseRecieved(CFindResponse resp, Association asc)
        {
            asc.Logger.Log("<-- DIMSE" + resp.GetLogString());
            asc.LastActive = DateTime.Now;
            dms.RaiseDIMSEResponseReceived(resp, asc);
            resp.LogData(asc);
            if (resp.Status != (ushort)Status.PENDING)
                AssociationMessenger.SendReleaseRequest(asc);
        }

        public IFindService<CFindStudyIOD> FindStudyService { get; set; }
        public IFindService<CFindImageIOD> FindImageService { get; set; }
        public IFindService<CFindSeriesIOD> FindSeriesService { get; set; }
    }
}
