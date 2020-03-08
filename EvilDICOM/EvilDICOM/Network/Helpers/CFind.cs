#region

using EvilDICOM.Core.Element;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using System;
using DF = EvilDICOM.Core.DICOMForge;
using System.Linq;

#endregion

namespace EvilDICOM.Network.Helpers
{
    public class CFind
    {
        public static CFindRequest CreateStudyQuery(string patientId)
        {
            var iod = new CFindPatientIOD();
            iod.PatientId = patientId;
            iod.CombineQuery(new CFindStudyIOD());
            return new CFindRequest(iod, Root.STUDY);
        }

        public static CFindRequest CreateSeriesQuery(string studyUid)
        {
            var iod = new CFindStudyIOD();
            iod.StudyInstanceUID = studyUid;
            iod.CombineQuery(new CFindSeriesIOD());
            return new CFindRequest(iod, Root.STUDY);
        }

        public static CFindRequest CreateQuery(CFindRequestIOD iodQuery)
        {
            if (iodQuery is CFindPatientIOD)
                return new CFindRequest(iodQuery, Root.PATIENT);
            else
                return new CFindRequest(iodQuery, Root.STUDY);
        }

        public static CFindRequest CreateImageQuery(CFindSeriesIOD ser)
        {
            var iod = new CFindSeriesIOD();
            iod.SeriesInstanceUID = ser.SeriesInstanceUID;

            switch (ser.Modality)
            {
                case "CT":
                case "MR":
                case "PT":
                case "RTIMAGE":
                    iod.CombineQuery(new CFindImageIOD());
                    break;
                case "PLAN":
                case "RTPLAN":
                    iod.CombineQuery(new CFindPlanIOD());
                    break;
                case "RTDOSE":
                    iod.CombineQuery(new CFindDoseIOD());
                    break;
                case "RTSTRUCT":
                    iod.CombineQuery(new CFindInstanceIOD());
                    break;
                case "RTRECORD":
                    iod.CombineQuery(new CFindTreatmentRecordIOD());
                    break;
                case "REG":
                    iod.CombineQuery(new CFindInstanceIOD());
                    break;
                default:
                    break;
            }

            return new CFindRequest(iod, Root.STUDY);
        }

        public static CFindRequest CreatePatientQuery(string patientId, string patientName, System.DateTime? dob)
        {
            var iod = new CFindPatientIOD();
            iod.PatientsName = DF.PatientName(patientName);
            iod.PatientBirthDate = dob;
            iod.PatientId = patientId;
            return new CFindRequest(iod, Root.PATIENT);
        }
    }
}