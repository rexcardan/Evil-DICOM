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
    }
}
