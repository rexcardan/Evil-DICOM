#region

using System.Collections.Generic;

#endregion

namespace EvilDICOM.Core.Helpers
{
    public class AbstractSyntax :SOPClassUID
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

        public static string VARIAN_MOTION_MANAGEMENT_PROTOCOL = "1.2.246.352.70.1.40";

        //ALL RADIOTHERAPY STORAGE
        public static List<string> ALL_RADIOTHERAPY_STORAGE = new List<string>
        {
            VERIFICATION,
            CTImageStorage,
            RTImageStorage,
            RTDoseStorage,
            RTStructureSetStorage,
            RTBeamsTreatmentRecordStorage,
            RTPlanStorage,
            RTBrachyTreatmentRecordStorage,
            RTBrachyApplicationSetupDeliveryInstructionStorage,
            ComputedRadiographyImageStorage,
            MRImageStorage,
            SpatialRegistrationStorage,
            DeformableSpatialRegistrationStorage,
            UltrasoundImageStorage,
            X_RayAngiographicImageStorage,
            VARIAN_MOTION_MANAGEMENT_PROTOCOL,
            RTImageStorage,
            RTIonBeamsTreatmentRecordStorage,
            RTIonPlanStorage
        };

        public static string StorageCommitment_Push = "1.2.840.10008.1.20.1";
    }
}