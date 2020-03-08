#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evil_DICOM.Core.Helpers;
using EvilDICOM.Core;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Helpers;

#endregion

namespace EvilDICOM.Network.SCUOps
{
    /// <summary>
    /// A class to help with CFind and CMove operations
    /// </summary>
    public class CFinder
    {
        private readonly Entity _scp;
        private readonly DICOMSCU _scu;

        /// <summary>
        /// A Query builder constructor which requires a SCU and SCP entity
        /// </summary>
        /// <param name="scu">The SCU client which will perform the operations and queries</param>
        /// <param name="scp">the SCP which will send the results</param>
        public CFinder(DICOMSCU scu, Entity scp)
        {
            _scu = scu;
            _scp = scp;
        }

        public IEnumerable<CFindStudyIOD> FindStudies(string patientId)
        {
            ushort msgId = 1;
            var query = CFind.CreateStudyQuery(patientId);
            var studyUids = _scu.GetResponses<CFindResponse, CFindRequest>(query, _scp, ref msgId) // Studies
                .Where(r => r.Status == (ushort)Status.PENDING)
                .Where(r => r.HasData)
                .Select(r => new CFindStudyIOD(new DICOMObject(r.Data.Elements)));
            return studyUids;
        }

        public IEnumerable<CFindSeriesIOD> FindSeries(IEnumerable<CFindStudyIOD> studies)
        {
            var results = new List<CFindSeriesIOD>();
            ushort msgId = 1;
            foreach (var study in studies)
            {
                Tryer.Try(() =>
                {
                    var req = CFind.CreateSeriesQuery(study.StudyInstanceUID);
                    var seriesUids = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                        .Where(r => r.Status == (ushort)Status.PENDING)
                        .Where(r => r.HasData)
                        .Select(r => new CFindSeriesIOD(new DICOMObject(r.Data.Elements)));
                    results.AddRange(seriesUids);
                },  $"Adding study {study.StudyInstanceUID}...");
            }
            return results;
        }

        public IEnumerable<CFindSeriesIOD> FindSeries(CFindStudyIOD study)
        {
            return FindSeries(new[] { study });
        }

        public IEnumerable<CFindInstanceIOD> FindImages(IEnumerable<CFindSeriesIOD> series)
        {
            var results = new List<CFindInstanceIOD>();

            results = results
                .Concat(FindPlans(series))
                .Concat(FindDoses(series))
                .Concat(FindStructures(series))
                .Concat(FindCTs(series))
                .Concat(FindMRs(series))
                .Concat(FindPETs(series))
                .Concat(FindRTRecords(series))
                .Concat(FindRTImages(series))
                .Concat(FindRegistrations(series))
                .Concat(FindOther(series)).ToList();

            return results;
        }

        private IEnumerable<CFindInstanceIOD> FindOther(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindInstanceIOD>(series.Where(s =>
            {
                switch (s.Modality)
                {
                    case "REG": return false;
                    case "CT": return false;
                    case "MR": return false;
                    case "PT": return false;
                    case "RTPLAN": return false;
                    case "PLAN": return false;
                    case "RTDOSE": return false;
                    case "RTSTRUCT": return false;
                    case "RTIMAGE": return false;
                    case "RTRECORD": return false;
                    default: return true;
                }
            }));
        }

        public IEnumerable<CFindInstanceIOD> FindInstances(CFindSeriesIOD ser)
        {
            return FindInstances(new CFindSeriesIOD[] { ser });
        }

        public IEnumerable<CFindInstanceIOD> FindInstances(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindInstanceIOD>(series);
        }

        private IEnumerable<CFindInstanceIOD> FindRegistrations(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindInstanceIOD>(series.Where(s => s.Modality == "REG"));
        }

        private IEnumerable<CFindImageIOD> FindCTs(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindImageIOD>(series.Where(s => s.Modality == "CT"));
        }

        private IEnumerable<CFindImageIOD> FindMRs(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindImageIOD>(series.Where(s => s.Modality == "MR"));
        }

        private IEnumerable<CFindImageIOD> FindPETs(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindImageIOD>(series.Where(s => s.Modality == "PT"));
        }

        public IEnumerable<CFindInstanceIOD> FindImages(CFindSeriesIOD series)
        {
            return FindImages(new[] { series });
        }

        public IEnumerable<CFindPatientIOD> FindPatient(string patientId = "", string patientName = "", System.DateTime? dob = null)
        {
            ushort msgId = 1;
            var query = CFind.CreatePatientQuery(patientId, patientName, dob);
            var patientResponses = _scu.GetResponses<CFindResponse, CFindRequest>(query, _scp, ref msgId); // Studies 
            var responses = patientResponses.Where(r => r.HasData)
                .Select(r => new CFindPatientIOD(new DICOMObject(r.Data.Elements)));
            return responses;
        }

        public IEnumerable<T> FindImages<T>(IEnumerable<CFindSeriesIOD> series) where T : CFindInstanceIOD
        {
            var results = new List<T>();
            ushort msgId = 1;
            foreach (var ser in series)
            {
                Tryer.Try(() =>
                {
                    var req = CFind.CreateImageQuery(ser);
                    var imagesUids = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                        .Where(r => r.Status == (ushort)Status.PENDING)
                        .Where(r => r.HasData)
                        .Select(r => r.GetIOD<T>())
                        .ToList();
                    results.AddRange(imagesUids);
                }, $"Adding series {ser.Modality} : {ser.SeriesInstanceUID}");

            }
            return results;
        }


        public IEnumerable<T> FindImages<T>(CFindSeriesIOD series) where T : CFindImageIOD
        {
            return FindImages<T>(new[] { series });
        }

        public IEnumerable<CFindPlanIOD> FindPlans(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindPlanIOD>(series.Where(s => s.Modality == "RTPLAN" || s.Modality == "PLAN"));
        }

        public IEnumerable<CFindDoseIOD> FindDoses(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindDoseIOD>(series.Where(s => s.Modality == "RTDOSE"));
        }

        public IEnumerable<CFindInstanceIOD> FindStructures(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindInstanceIOD>(series.Where(s => s.Modality == "RTSTRUCT"));
        }

        public IEnumerable<CFindRTImageIOD> FindRTImages(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindRTImageIOD>(series.Where(s => s.Modality == "RTIMAGE"));
        }

        public IEnumerable<CFindTreatmentRecordIOD> FindRTRecords(IEnumerable<CFindSeriesIOD> series)
        {
            return FindImages<CFindTreatmentRecordIOD>(series.Where(s => s.Modality == "RTRECORD"));
        }

        public IEnumerable<T> Find<T>(T iodQuery) where T : CFindInstanceIOD
        {
            var results = new List<T>();
            ushort msgId = 1;
            var req = CFind.CreateQuery(iodQuery);
            var responses = _scu.GetResponses<CFindResponse, CFindRequest>(req, _scp, ref msgId)
                   .Where(r => r.Status == (ushort)Status.PENDING)
                   .Where(r => r.HasData)
                   .Select(r => r.GetIOD<T>())
                   .ToList();
            results.AddRange(responses);
            return results;
        }
    }
}