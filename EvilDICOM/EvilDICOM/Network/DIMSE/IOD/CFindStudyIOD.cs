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
            PatientsName = new PersonName() { Tag = TagHelper.Patient​Name, Data = string.Empty };
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
                if (_sel.Query​Retrieve​Level == null)
                {
                    _sel.Query​Retrieve​Level.Data = QueryLevel.PATIENT.ToString();
                }
                return (QueryLevel) S.Enum.Parse(typeof (QueryLevel), _sel.Query​Retrieve​Level.Data);
            }
            set { _sel.Forge(DF.Query​Retrieve​Level(value.ToString())); }
        }

        public PersonName PatientsName
        {
            get { return _sel.Patient​Name != null ? _sel.Patient​Name : null; }
            set { _sel.Patient​Name = value; }
        }

        public string PatientId
        {
            get { return _sel.Patient​ID != null ? _sel.Patient​ID.Data : null; }
            set { _sel.Forge(DF.Patient​ID(value)); }
        }

        public S.DateTime? StudyDate
        {
            get { return _sel.StudyDate != null ? _sel.StudyDate.Data : null; }
            set
            {
                _sel.Forge(DF.StudyDate(value));
            }
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
