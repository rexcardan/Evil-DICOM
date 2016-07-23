using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes the study id from the DICOM files and creates a new study id
    /// </summary>
    public class StudyIdAnonymizer : IAnonymizer
    {

        public StudyIdAnonymizer(){
            Studies= new List<DICOMStudy>();
            StudyDictionary = new Dictionary<string, string>();
        }
        List<DICOMStudy> Studies { get; set; } 
        /// <summary>
        /// A dictionary which stores the original study id and type of study
        /// </summary>
        public Dictionary<string, string> StudyDictionary { get; set; } 

        /// <summary>
        /// This method is to be called once all DICOM objects are added. It then remaps study ids in a private dictionary
        /// </summary>
        public void FinalizeDictionary()
        {
            GenerateNamesByType();
        }

        public void GenerateNames()
        {
            Studies = Studies.OrderBy(s => s.ID).ToList();
            int i = 1;
            foreach (DICOMStudy s in Studies)
            {
                StudyDictionary.Add(s.ID, string.Format("{0}_{1}", "Study", i));
                i++;
            }
        }

        public void GenerateNamesByType()
        {
            var types = Enum.GetValues(typeof(DICOMFileType)).Cast<DICOMFileType>();

            foreach (DICOMFileType type in types)
            {
                List<DICOMStudy> studiesOfType = Studies
                .Where(s => s.Type == type)
                .OrderBy(s => s.Date)
                .ToList();

                string abbreviation = GetTypeAbbreviation(type);
                int i = 1;
                foreach (DICOMStudy s in studiesOfType)
                {
                    StudyDictionary.Add(s.ID, string.Format("{0}_{1}", abbreviation, i));
                    i++;
                }
            }
        }

        private string GetTypeAbbreviation(DICOMFileType type)
        {
            switch (type)
            {
                case DICOMFileType.RT_IMAGE: return "RT";
                case DICOMFileType.CT_IMAGE: return "CT";
                case DICOMFileType.MRI_IMAGE: return "MR";
                case DICOMFileType.PET_IMAGE: return "RT";
                case DICOMFileType.RT_PLAN: return "RTPLAN";
                case DICOMFileType.RT_STRUCT: return "RTSTRUCT";
                case DICOMFileType.RT_DOSE: return "DOSE";
                default: return "STUDY";
            }
        }

        public void AddDICOMObject(DICOMObject d)
        {
            IDICOMElement id = d.FindFirst(TagHelper.STUDY_ID.CompleteID);
            if (id != null)
            {
                string studyID = (id as ShortString).Data;
                DICOMFileType type = GetFileType(d);
                if (!Studies.Exists(s => s.ID == studyID))
                {
                    DICOMStudy study = new DICOMStudy();
                    study.ID = studyID;
                    study.Type = type;
                    Date dt = d.FindFirst(TagHelper.STUDY_DATE.CompleteID) as Date;
                    study.Date = dt.Data;
                    Studies.Add(study);
                }
            }
        }

        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Removing study IDs and descriptions...");
            ShortString sID = d.FindFirst(TagHelper.STUDY_ID) as ShortString;
            if (sID != null)
            {
                sID.Data = StudyDictionary[sID.Data];
            }
            LongString desc = d.FindFirst(TagHelper.STUDY_DESCRIPTION) as LongString;
            if (desc != null)
            {
                desc.Data = string.Empty;
            }
        }

        public static DICOMFileType GetFileType(DICOMObject d)
        {
            IDICOMElement el = d.FindFirst(TagHelper.SOPCLASS_UID.CompleteID);
            if (el != null)
            {
                UniqueIdentifier ui = el as UniqueIdentifier;
                switch (ui.Data)
                {
                    case "1.2.840.10008.5.1.4.1.1.481.1": return DICOMFileType.RT_IMAGE;
                    case "1.2.840.10008.5.1.4.1.1.2": return DICOMFileType.CT_IMAGE;
                    case "1.2.840.10008.5.1.4.1.1.4": return DICOMFileType.MRI_IMAGE;
                    case "1.2.840.10008.5.1.4.1.1.77.1.5.2": return DICOMFileType.PET_IMAGE;
                    case "1.2.840.10008.5.1.4.1.1.481.5": return DICOMFileType.RT_PLAN;
                    case "1.2.840.10008.5.1.4.1.1.481.3": return DICOMFileType.RT_STRUCT;
                    case "1.2.840.10008.5.1.4.1.1.481.2": return DICOMFileType.RT_DOSE;
                    default: return DICOMFileType.OTHER;
                }
            }
            return DICOMFileType.OTHER;
        }

        public string Name
        {
            get { return "Study Id Anonymizer"; }
        }
    }

    public class DICOMStudy
    {
        public string ID { get; set; }
        public System.DateTime? Date { get; set; }
        public DICOMFileType Type { get; set; }
    }

    public enum DICOMFileType
    {
        RT_IMAGE,
        CT_IMAGE,
        MRI_IMAGE,
        PET_IMAGE,
        RT_DOSE,
        RT_PLAN,
        RT_STRUCT,
        REGISTRATION,
        OTHER
    }
}
