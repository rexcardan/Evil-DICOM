#region

using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindImageIOD : AbstractDIMSEIOD
    {
        public CFindImageIOD()
        {
            QueryLevel = QueryLevel.IMAGE;
            SOPInstanceUID = string.Empty;
            PatientId = string.Empty;
            PatientsName = DF.PatientName();
            StudyInstanceUID = string.Empty;
            SeriesInstanceUID = string.Empty;
        }

        public CFindImageIOD(DICOMObject dcm) : base(dcm)
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