#region

using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindStudyIOD : AbstractDIMSEIOD
    {
        public CFindStudyIOD()
        {
            QueryLevel = QueryLevel.STUDY;
            SOPInstanceUID = string.Empty;
            PatientId = string.Empty;
            PatientsName = new PersonName {Tag = TagHelper.PatientName, Data = string.Empty};
            StudyDate = null;
            AccessionNumber = string.Empty;
            StudyId = string.Empty;
            StudyDescription = string.Empty;
            ReferringPhysicianName = string.Empty;
            NameOfPhysiciansReadingStudy = string.Empty;
            ModalitiesInStudy = string.Empty;
            PatientBirthDate = null;
            StudyInstanceUID = string.Empty;
            SeriesInstanceUID = string.Empty;
        }

        public CFindStudyIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.QueryRetrieveLevel == null)
                    _sel.QueryRetrieveLevel.Data = QueryLevel.PATIENT.ToString();
                return (QueryLevel) S.Enum.Parse(typeof(QueryLevel), _sel.QueryRetrieveLevel.Data);
            }
            set { _sel.Forge(DF.QueryRetrieveLevel(value.ToString())); }
        }

        public PersonName PatientsName
        {
            get { return _sel.PatientName != null ? _sel.PatientName : null; }
            set { _sel.PatientName = value; }
        }

        public string PatientId
        {
            get { return _sel.PatientID != null ? _sel.PatientID.Data : null; }
            set { _sel.Forge(DF.PatientID(value)); }
        }

        public S.DateTime? StudyDate
        {
            get { return _sel.StudyDate != null ? _sel.StudyDate.Data : null; }
            set { _sel.Forge(DF.StudyDate(value)); }
        }

        public S.DateTime? StudyTime
        {
            get { return _sel.StudyTime != null ? _sel.StudyTime.Data : null; }
            set { _sel.Forge(DF.StudyTime(value)); }
        }

        public string AccessionNumber
        {
            get { return _sel.AccessionNumber != null ? _sel.AccessionNumber.Data : null; }
            set { _sel.Forge(DF.AccessionNumber(value)); }
        }

        public string StudyId
        {
            get { return _sel.StudyID != null ? _sel.StudyID.Data : null; }
            set { _sel.Forge(DF.StudyID(value)); }
        }

        public string StudyDescription
        {
            get { return _sel.StudyDescription != null ? _sel.StudyDescription.Data : null; }
            set { _sel.Forge(DF.StudyDescription(value)); }
        }

        public string ReferringPhysicianName
        {
            get { return _sel.ReferringPhysicianName != null ? _sel.ReferringPhysicianName.Data : null; }
            set { _sel.Forge(DF.ReferringPhysicianName(value)); }
        }

        public string NameOfPhysiciansReadingStudy
        {
            get { return _sel.NameOfPhysiciansReadingStudy != null ? _sel.NameOfPhysiciansReadingStudy.Data : null; }
            set { _sel.Forge(DF.NameOfPhysiciansReadingStudy(value)); }
        }

        public string ModalitiesInStudy
        {
            get { return _sel.ModalitiesInStudy != null ? _sel.ModalitiesInStudy.Data : null; }
            set { _sel.Forge(DF.ModalitiesInStudy(value)); }
        }

        public S.DateTime? PatientBirthDate
        {
            get { return _sel.PatientBirthDate != null ? _sel.PatientBirthDate.Data : null; }
            set { _sel.Forge(DF.PatientBirthDate(value)); }
        }

        public string StudyInstanceUID
        {
            get { return _sel.StudyInstanceUID != null ? _sel.StudyInstanceUID.Data : null; }
            set { _sel.Forge(DF.StudyInstanceUID(value)); }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID(value)); }
        }

        public string SOPInstanceUID
        {
            get { return _sel.SOPInstanceUID != null ? _sel.SOPInstanceUID.Data : null; }
            set { _sel.Forge(DF.SOPInstanceUID(value)); }
        }
    }
}