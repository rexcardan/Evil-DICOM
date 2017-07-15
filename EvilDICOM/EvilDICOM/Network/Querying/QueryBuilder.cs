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
    /// A class to help with CFind and CMove operations
    /// </summary>
    public class QueryBuilder
    {
        private DICOMSCU _scu;
        private Entity _scp;

        /// <summary>
        /// A Query builder constructor which requires a SCU and SCP entity
        /// </summary>
        /// <param name="scu">The SCU client which will perform the operations and queries</param>
        /// <param name="scp">the SCP which will send the results</param>
        public QueryBuilder(DICOMSCU scu, Entity scp)
        {
            _scu = scu;
            _scp = scp;
        }

        public IEnumerable<CFindStudyIOD> GetStudyUids(string patientId)
        {
            var query = CFind.CreateStudyQuery(patientId);
            var studyUids = _scu.GetResponse(query, _scp) // Studies
                .Where(r => r.Status == (ushort)Status.PENDING)
                .Where(r => r.HasData);
            return studyUids.Select(r => r.GetIOD<CFindStudyIOD>());
        }

        public IEnumerable<CFindSeriesIOD> GetSeriesUids(IEnumerable<CFindStudyIOD> studies)
        {
            List<CFindSeriesIOD> results = new List<CFindSeriesIOD>();

            foreach (var study in studies)
            {
                var req = CFind.CreateSeriesQuery(study.StudyInstanceUID);
                var seriesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindSeriesIOD>());
                results.AddRange(seriesUids);
            }
            return results;
        }

        public IEnumerable<CFindSeriesIOD> GetSeriesUids(CFindStudyIOD study)
        {
            return GetSeriesUids(new CFindStudyIOD[] { study });
        }

        public IEnumerable<CFindImageIOD> GetImageUids(IEnumerable<CFindSeriesIOD> series)
        {
            List<CFindImageIOD> results = new List<CFindImageIOD>();

            foreach (var ser in series)
            {
                var req = CFind.CreateImageQuery(ser.SeriesInstanceUID);
                var imagesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindImageIOD>())
                    .ToList();
                results.AddRange(imagesUids);           
            }
            return results;
        }

        public IEnumerable<CFindImageIOD> GetImageUids(CFindSeriesIOD series)
        {
            return GetImageUids(new CFindSeriesIOD[] { series });
        }

        public IEnumerable<T> GetImageUids<T>(IEnumerable<CFindSeriesIOD> series) where T : CFindImageIOD
        {
            List<T> results = new List<T>();

            foreach (var ser in series)
            {
                var req = CFind.CreateImageQuery(ser.SeriesInstanceUID);
                var imagesUids = _scu.GetResponse(req, _scp)
                    .Where(r => r.Status == (ushort)Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<T>())
                    .ToList();
                results.AddRange(imagesUids);
            }
            return results;
        }

        public IEnumerable<T> GetImageUids<T>(CFindSeriesIOD series) where T : CFindImageIOD
        {
            return GetImageUids<T>(new CFindSeriesIOD[] { series });
        }

        /// <summary>
        /// Instructs a C-MOVE operation for an image from the SCP of the QueryBuilder to the input AETitle
        /// </summary>
        /// <param name="ir">the C Find iod of the a query (get ImageUids())</param>
        /// <param name="receivingAETitle">the AE title to send the image to (from the SCP of this query builder)</param>
        /// <param name="msgId">the message id for this image. It will be incremented for looping operations within this method</param>
        /// <returns>a C-MOVE response for this operation</returns>
        public CMoveResponse SendImage(CFindImageIOD ir, string receivingAETitle, ref ushort msgId)
        {
            var resp = _scu.SendCMoveImage(_scp, ir, receivingAETitle, ref msgId);
            return resp;
        }

        /// <summary>
        /// Instructs a C-MOVE operation for an image from the SCP of the QueryBuilder to the input AETitle
        /// </summary>
        /// <param name="ir">the C Find iod of the a query (get ImageUids())</param>
        /// <param name="receivingAETitle">the AE title to send the image to (from the SCP of this query builder)</param>
        /// <param name="msgId">the message id for this image. It will NOT be incremented for looping operations within this method</param>
        /// <returns>a C-MOVE response for this operation</returns>
        public async Task<CMoveResponse> SendImageAsync(CFindImageIOD ir, string receivingAETitle, ushort msgId)
        {
            return await Task.Run(() =>
            {
                var resp = _scu.SendCMoveImage(_scp, ir, receivingAETitle, ref msgId);
                return resp;
            });
        }
    }
}
