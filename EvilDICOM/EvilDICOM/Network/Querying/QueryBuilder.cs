#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Helpers;

#endregion

namespace EvilDICOM.Network.Querying
{
    /// <summary>
    /// A class to help with CFind and CMove operations
    /// </summary>
    public class QueryBuilder
    {
        private readonly Entity _scp;
        private readonly DICOMSCU _scu;

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
            ushort msgId = 1;
            var query = CFind.CreateStudyQuery(patientId);
            var studyUids = _scu.GetResponses<CFindResponse, CFindRequest>(query, _scp, ref msgId) // Studies
                .Where(r => r.Status == (ushort) Status.PENDING)
                .Where(r => r.HasData);
            return studyUids.Select(r => r.GetIOD<CFindStudyIOD>());
        }

        public IEnumerable<CFindSeriesIOD> GetSeriesUids(IEnumerable<CFindStudyIOD> studies)
        {
            var results = new List<CFindSeriesIOD>();
            ushort msgId = 1;
            foreach (var study in studies)
            {
                var req = CFind.CreateSeriesQuery(study.StudyInstanceUID);
                var seriesUids = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                    .Where(r => r.Status == (ushort) Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindSeriesIOD>());
                results.AddRange(seriesUids);
            }
            return results;
        }

        public IEnumerable<CFindSeriesIOD> GetSeriesUids(CFindStudyIOD study)
        {
            return GetSeriesUids(new[] {study});
        }

        public IEnumerable<CFindImageIOD> GetImageUids(IEnumerable<CFindSeriesIOD> series)
        {
            var results = new List<CFindImageIOD>();
            ushort msgId = 1;
            foreach (var ser in series)
            {
                var req = CFind.CreateImageQuery(ser.SeriesInstanceUID);
                var imagesUids = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                    .Where(r => r.Status == (ushort) Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<CFindImageIOD>())
                    .ToList();
                results.AddRange(imagesUids);
            }
            return results;
        }

        public IEnumerable<CFindImageIOD> GetImageUids(CFindSeriesIOD series)
        {
            return GetImageUids(new[] {series});
        }

        public IEnumerable<T> GetImageUids<T>(IEnumerable<CFindSeriesIOD> series) where T : CFindImageIOD
        {
            var results = new List<T>();
            ushort msgId = 1;
            foreach (var ser in series)
            {
                var req = CFind.CreateImageQuery(ser.SeriesInstanceUID);
                var imagesUids = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                    .Where(r => r.Status == (ushort) Status.PENDING)
                    .Where(r => r.HasData)
                    .Select(r => r.GetIOD<T>())
                    .ToList();
                results.AddRange(imagesUids);
            }
            return results;
        }

        public IEnumerable<T> GetImageUids<T>(CFindSeriesIOD series) where T : CFindImageIOD
        {
            return GetImageUids<T>(new[] {series});
        }
    }
}