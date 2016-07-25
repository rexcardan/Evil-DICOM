using System.Runtime.Serialization;
using EvilDICOM.Core.Element;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using EvilDICOM.Core.Interfaces;
using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindStudyIOD : AbstractDIMSEIOD
    {
         public CFindStudyIOD()
        {
            QueryLevel = QueryLevel.STUDY;
            SOPInstanceUID = string.Empty;
            PatientId = string.Empty;
            PatientsName = new PersonName() { Tag = TagHelper.PATIENT_NAME, Data = string.Empty };
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

         public CFindStudyIOD(DICOMObject dcm) : base(dcm) { }

        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.QueryRetrieveLevel == null)
                {
                    _sel.QueryRetrieveLevel.Data = QueryLevel.PATIENT.ToString();
                }
                return (QueryLevel) S.Enum.Parse(typeof (QueryLevel), _sel.QueryRetrieveLevel.Data);
            }
            set { _sel.Forge(DF.QueryRetrieveLevel).Data = value.ToString(); }
        }

        public PersonName PatientsName
        {
            get { return _sel.PatientName != null ? _sel.PatientName : null; }
            set { _sel.PatientName = value; }
        }

        public string PatientId
        {
            get { return _sel.PatientID != null ? _sel.PatientID.Data : null; }
            set { _sel.Forge(DF.PatientID).Data = value; }
        }

        public S.DateTime? StudyDate
        {
            get { return _sel.StudyDate != null ? _sel.StudyDate.Data : null; }
            set
            {
                _sel.Forge(DF.StudyDate).Data = value;
            }
        }

        public S.DateTime? StudyTime
        {
            get { return _sel.StudyTime != null ? _sel.StudyTime.Data : null; }
            set { _sel.Forge(DF.StudyTime).Data = value; }
        }

        public string AccessionNumber
        {
            get { return _sel.AccessionNumber != null ? _sel.AccessionNumber.Data : null; }
            set { _sel.Forge(DF.AccessionNumber).Data = value; }
        }

        public string StudyId
        {
            get { return _sel.StudyID != null ? _sel.StudyID.Data : null; }
            set { _sel.Forge(DF.StudyID).Data = value; }
        }

        public string StudyDescription
        {
            get { return _sel.StudyDescription != null ? _sel.StudyDescription.Data : null; }
            set { _sel.Forge(DF.StudyDescription).Data = value; }
        }

        public string ReferringPhysicianName
        {
            get { return _sel.ReferringPhysicianName != null ? _sel.ReferringPhysicianName.Data : null; }
            set { _sel.Forge(DF.ReferringPhysicianName).Data = value; }
        }

        public string NameOfPhysiciansReadingStudy
        {
            get { return _sel.NameOfPhysiciansReadingStudy != null ? _sel.NameOfPhysiciansReadingStudy.Data : null; }
            set { _sel.Forge(DF.NameOfPhysiciansReadingStudy).Data = value; }
        }

        public string ModalitiesInStudy
        {
            get { return _sel.ModalitiesInStudy != null ? _sel.ModalitiesInStudy.Data : null; }
            set { _sel.Forge(DF.ModalitiesInStudy).Data = value; }
        }

        public S.DateTime? PatientBirthDate
        {
            get { return _sel.PatientBirthDate != null ? _sel.PatientBirthDate.Data : null; }
            set { _sel.Forge(DF.PatientBirthDate).Data = value; }
        }

        public string StudyInstanceUID
        {
            get { return _sel.StudyInstanceUID != null ? _sel.StudyInstanceUID.Data : null; }
            set { _sel.Forge(DF.StudyInstanceUID).Data = value; }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID).Data = value; }
        }

        public string SOPInstanceUID
        {
            get { return _sel.SOPInstanceUID != null ? _sel.SOPInstanceUID.Data : null; }
            set { _sel.Forge(DF.SOPInstanceUID).Data = value; }
        }
    }
}
