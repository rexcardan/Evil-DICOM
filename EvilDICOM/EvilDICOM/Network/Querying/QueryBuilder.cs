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

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder"/> class.
        /// </summary>
        /// <param name="scu">The scu.</param>
        /// <param name="scp">The SCP.</param>
        public QueryBuilder(DICOMSCU scu, Entity scp)
        {
            _scu = scu;
            _scp = scp;
        }

        /// <summary>
        /// Gets the study uids.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns>List&lt;StudyResult&gt;.</returns>
        public List<StudyResult> GetStudyUids(string patientId)
        {
            var query = new CFindIOD(QueryLevel.STUDY)
            {
                PatientId = patientId
            };
            var req = new CFindRequest(query, Root.STUDY);
            var studyUids = _scu.GetResponse(req, _scp) // Studies
                .Where(r => r.Status == (ushort)Status.PENDING)
                .Where(r => r.HasData)
                .ToList();

            return studyUids.Select(r => new StudyResult()
                {
                    PatientId = query.PatientId,
                    StudyUid = r.Data.GetSelector().StudyInstanceUID.Data
                })
                .ToList();
        }

        /// <summary>
        /// Gets the series uids.
        /// </summary>
        /// <param name="studies">The studies.</param>
        /// <returns>List&lt;SeriesResult&gt;.</returns>
        public List<SeriesResult> GetSeriesUids(List<StudyResult> studies)
        {
            List<SeriesResult> results = new List<SeriesResult>();

            foreach (var study in studies)
            {
                var query = new CFindIOD(QueryLevel.SERIES)
                {
                    PatientId = study.PatientId,
                    StudyInstanceUID = study.StudyUid
                };
                var req = new CFindRequest(query, Root.STUDY);
                var seriesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r =>
                        new SeriesResult()
                        {
                            PatientId = study.PatientId,
                            StudyUid = study.StudyUid,
                            SeriesUid = r.Data.GetSelector().SeriesInstanceUID.Data,
                            Modality = r.Data.GetSelector().Modality.Data
                        }
                    )
                    .ToList();
                results.AddRange(seriesUids);
            }
            return results;
        }

        /// <summary>
        /// Gets the image uids.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>List&lt;ImageResult&gt;.</returns>
        public List<ImageResult> GetImageUids(List<SeriesResult> series)
        {
            List<ImageResult> results = new List<ImageResult>();

            foreach (var ser in series)
            {
                var query = new CFindIOD(QueryLevel.IMAGE)
                {
                    PatientId = ser.PatientId,
                    StudyInstanceUID = ser.StudyUid,
                    SeriesInstanceUID = ser.SeriesUid
                };

                var req = new CFindRequest(query, Root.STUDY);
                var seriesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r =>
                        new ImageResult()
                        {
                            PatientId = ser.PatientId,
                            StudyUid = ser.StudyUid,
                            SeriesUid = ser.SeriesUid,
                            Modality = ser.Modality,
                            SopInstanceUid = r.Data.GetSelector().SOPInstanceUID.Data
                        }
                    )
                    .ToList();
                results.AddRange(seriesUids);
            }
            return results;
        }


        /// <summary>
        /// Sends the image.
        /// </summary>
        /// <param name="ir">The ir.</param>
        /// <param name="reciever">The reciever.</param>
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

        /// <summary>
        /// Dimses the service_ c move response received.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="asc">The asc.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void DIMSEService_CMoveResponseReceived(CMoveResponse req, Association asc)
        {
            throw new NotImplementedException();
        }
    }
}
