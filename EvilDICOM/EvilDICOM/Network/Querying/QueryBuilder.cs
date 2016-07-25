using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using System.Threading;
using EvilDICOM.Core.Logging;
using System.Threading.Tasks;
using EvilDICOM.Network.Helpers;

namespace EvilDICOM.Network.Querying
{
    /// <summary>
    /// A class to help with CFind operations
    /// </summary>
    public class QueryBuilder
    {
        private DICOMSCU _scu;
        private Entity _scp;

        public QueryBuilder(DICOMSCU scu, Entity scp)
        {
            _scu = scu;
            _scp = scp;
        }

        public List<CFindStudyIOD> GetStudyUids(string patientId)
        {
            var query = CFind.CreateStudyQuery(patientId);
            var studyUids = _scu.GetResponse(query, _scp) // Studies
                .Where(r => r.Status == (ushort)Status.PENDING)
                .Where(r => r.HasData)
                .ToList();

            return studyUids.Select(r => r.GetIOD<CFindStudyIOD>()).ToList();
        }

        public List<CFindSeriesIOD> GetSeriesUids(List<CFindStudyIOD> studies)
        {
            List<CFindSeriesIOD> results = new List<CFindSeriesIOD>();

            foreach (var study in studies)
            {
                var req = CFind.CreateSeriesQuery(study.StudyInstanceUID);
                var seriesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindSeriesIOD>())
                    .ToList();
                results.AddRange(seriesUids);
            }
            return results;
        }

        public List<CFindImageIOD> GetImageUids(List<CFindSeriesIOD> series)
        {
            List<CFindImageIOD> results = new List<CFindImageIOD>();

            foreach (var ser in series)
            {
                var req = CFind.CreateImageQuery(ser.SeriesInstanceUID);
                var seriesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindImageIOD>())
                    .ToList();
                results.AddRange(seriesUids);
            }
            return results;
        }


        public void SendImage(ImageResult ir, DICOMSCP reciever)
        {
            AutoResetEvent ar = new AutoResetEvent(false);
            var query = new CMoveIOD()
            {
                QueryLevel = QueryLevel.IMAGE,
                PatientId = ir.PatientId,
                SOPInstanceUID = ir.SopInstanceUid
            };
            if (ir.SeriesUid != null) { query.SeriesInstanceUID = ir.SeriesUid; }

            if (!reciever.IsListening) { reciever.ListenForIncomingAssociations(true); }

            ManualResetEvent mr = new ManualResetEvent(false);
            var cr = new EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CMoveResponse>((res, asc) =>
            {
                if (!(res.Status == (ushort)Status.PENDING))
                {
                    mr.Set();
                }
            });

            _scu.DIMSEService.CMoveResponseReceived += cr;
            _scu.SendMessage(new CMoveRequest(query, reciever.ApplicationEntity.AeTitle), _scp);
            mr.WaitOne();
            _scu.DIMSEService.CMoveResponseReceived -= cr;
        }

        void DIMSEService_CMoveResponseReceived(CMoveResponse req, Association asc)
        {
            throw new NotImplementedException();
        }
    }
}
