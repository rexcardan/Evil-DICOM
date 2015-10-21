using System.Collections.Generic;

namespace EvilDICOM.Core.Helpers
{
    public class AbstractSyntax
    {
        //VERIFICATION
        public static string VERIFICATION = "1.2.840.10008.1.1";

        //PATIENT
        public static string PATIENT_FIND = "1.2.840.10008.5.1.4.1.2.1.1";
        public static string PATIENT_MOVE = "1.2.840.10008.5.1.4.1.2.1.2";
        public static string PATIENT_GET = "1.2.840.10008.5.1.4.1.2.1.3";

        //STUDY
        public static string STUDY_FIND = "1.2.840.10008.5.1.4.1.2.2.1";
        public static string STUDY_MOVE = "1.2.840.10008.5.1.4.1.2.2.2";
        public static string STUDY_GET = "1.2.840.10008.5.1.4.1.2.2.3";

        //PATIENT-STUDY
        public static string PATIENT_STUDY_FIND = "1.2.840.10008.5.1.4.1.2.3.1";
        public static string PATIENT_STUDY_MOVE = "1.2.840.10008.5.1.4.1.2.3.2";

        //MODALITY WORK LIST
        public static string MODALITY_WORKLIST_FIND = "1.2.840.10008.5.1.4.31";

        //DICOM STORAGE
        public static string CR_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.1";
        public static string CT_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.2";
        public static string ENHANCED_CT_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.2.1";
        public static string MR_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.4";
        public static string NMR_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.20";
        public static string ENHANCED_MR_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.4.1";
        public static string ULTRASOUND_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.6.1";

        public static string RT_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.481.1";
        public static string RT_DOSE_STORAGE = "1.2.840.10008.5.1.4.1.1.481.2";
        public static string RT_STRUCTURE_STORAGE = "1.2.840.10008.5.1.4.1.1.481.3";
        public static string RT_BEAMS_TREAT_RECORD_STORAGE = "1.2.840.10008.5.1.4.1.1.481.4";
        public static string RT_PLAN_STORAGE = "1.2.840.10008.5.1.4.1.1.481.5";
        public static string RT_BRACHY_TREATMENT_RECORD_STORAGE = "1.2.840.10008.5.1.4.1.1.481.6";
        public static string RT_BRACHY_TREATMENT_SUMMARY_STORAGE = "1.2.840.10008.5.1.4.1.1.481.7";
        public static string RT_ION_PLAN_STORAGE = "1.2.840.10008.5.1.4.1.1.481.8";
        public static string RT_ION_BEAM_RECORD_STORAGE = "1.2.840.10008.5.1.4.1.1.481.9";

        public static string PET_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.128";
      

        public static string SECONDARY_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.7";
        public static string SPATIAL_IMAGE_STORAGE = "1.2.840.10008.5.1.4.1.1.66.1";
        public static string XRAY_ANGIOGRAPHIC_STORAGE = "1.2.840.10008.5.1.4.1.1.12.1";
        public static string VARIAN_MOTION_MANAGEMENT_PROTOCOL = "1.2.246.352.70.1.40";
        //ALL RADIOTHERAPY STORAGE
        public static List<string> ALL_RADIOTHERAPY_STORAGE = new List<string>
        {
            VERIFICATION,
            CT_IMAGE_STORAGE,
            RT_IMAGE_STORAGE,
            RT_DOSE_STORAGE,
            RT_STRUCTURE_STORAGE,
            RT_BEAMS_TREAT_RECORD_STORAGE,
            RT_PLAN_STORAGE,
            RT_BRACHY_TREATMENT_RECORD_STORAGE,
            RT_BRACHY_TREATMENT_SUMMARY_STORAGE,
            CR_IMAGE_STORAGE,
            MR_IMAGE_STORAGE,
            SPATIAL_IMAGE_STORAGE,
            ULTRASOUND_IMAGE_STORAGE,
            XRAY_ANGIOGRAPHIC_STORAGE,
            VARIAN_MOTION_MANAGEMENT_PROTOCOL,
            RT_ION_PLAN_STORAGE,
            RT_ION_BEAM_RECORD_STORAGE
        };
    }
}