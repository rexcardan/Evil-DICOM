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
    public class CFindImageIOD :  AbstractDIMSEIOD
    {
        public CFindImageIOD()
        {
            QueryLevel = QueryLevel.IMAGE;
            SOPInstanceUID = string.Empty;
            PatientId = string.Empty;
            PatientsName = new PersonName() { Tag = TagHelper.PATIENT_NAME, Data = string.Empty };
            StudyInstanceUID = string.Empty;
            SeriesInstanceUID = string.Empty;
        }

        public CFindImageIOD(DICOMObject dcm) : base(dcm) { }

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