#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Replaces standard indentification profile with empty data
    /// </summary>
    public class ProfileAnonymizer : IAnonymizer
    {
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Clearing DICOM profile...");
            foreach (var el in GenerateProfile())
                d.Replace(el);
        }

        /// <summary>
        /// Returns a collection of identifiable elements
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<IDICOMElement> GenerateProfile()
        {
            var profile = new List<IDICOMElement>();

            //PATIENT SEX
            profile.Add(GenerateEmptyDICOMString(TagHelper.PatientSex));

            //OTHER PATIENT IDS
            profile.Add(GenerateEmptyDICOMString(TagHelper.OtherPatientIDs));

            //OTHER PATIENT NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.OtherPatientNames));

            //ETHNIC GROUP
            profile.Add(GenerateEmptyDICOMString(TagHelper.EthnicGroup));

            //PATIENT COMMENTS
            profile.Add(GenerateEmptyDICOMString(TagHelper.PatientComments));

            //REFERRING PHYSICIAN NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.ReferringPhysicianName));

            //ACCESSION NUMBER
            profile.Add(GenerateEmptyDICOMString(TagHelper.AccessionNumber));

            //PHYSICIANS RECORD
            profile.Add(GenerateEmptyDICOMString(TagHelper.PhysiciansOfRecord));

            //PHYSICIANS READING STUDY
            profile.Add(GenerateEmptyDICOMString(TagHelper.NameOfPhysiciansReadingStudy));

            //ADMITTING DIAGNOSIS DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.AdmittingDiagnosesDescription));

            //PATIENTS SIZE
            profile.Add(GenerateZeroDecimalString(TagHelper.PatientSize));

            //PATIENTS WEIGHT
            profile.Add(GenerateZeroDecimalString(TagHelper.PatientWeight));

            //OCCUPATION
            profile.Add(GenerateEmptyDICOMString(TagHelper.Occupation));

            //ADDITIONAL PATIENT HISTORY
            profile.Add(GenerateEmptyDICOMString(TagHelper.AdditionalPatientHistory));

            //PERFORMING PHYSICIAN NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.PerformingPhysicianName));

            //PROTOCOL NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.ProtocolName));

            //SERIES DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.SeriesDescription));

            //OPERATORS NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.OperatorsName));

            //INSTITUITION NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.InstitutionName));

            //INSTITUTION ADDRESS
            profile.Add(GenerateEmptyDICOMString(TagHelper.InstitutionAddress));

            //STATION NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.StationName));

            //INSTITUTION DEPARTMENT NAME
            profile.Add(GenerateEmptyDICOMString(TagHelper.InstitutionalDepartmentName));

            //DEVICE SERIAL NUMBER
            profile.Add(GenerateEmptyDICOMString(TagHelper.DeviceSerialNumber));

            //DERIVATION DESCRIPTION
            profile.Add(GenerateEmptyDICOMString(TagHelper.DerivationDescription));

            //IMAGE COMMENTS
            profile.Add(GenerateEmptyDICOMString(TagHelper.ImageComments));

            return profile;
        }

        private IDICOMElement GenerateZeroDecimalString(Tag tag)
        {
            var ds = new DecimalString(tag, new[] {0.0});
            return ds;
        }

        private IDICOMElement GenerateEmptyDICOMString(Tag tag)
        {
            var s = new LongString();
            s.Tag = tag;
            s.Data = string.Empty;
            return s;
        }
    }
}