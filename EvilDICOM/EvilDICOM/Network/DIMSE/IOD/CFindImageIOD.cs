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
            PatientsName = DF.Patient​Name();
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
                if (_sel.Query​Retrieve​Level == null)
                    _sel.Query​Retrieve​Level.Data = QueryLevel.PATIENT.ToString();
                return (QueryLevel) S.Enum.Parse(typeof(QueryLevel), _sel.Query​Retrieve​Level.Data);
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

        public string StudyInstanceUID
        {
            get { return _sel.Study​Instance​UID != null ? _sel.Study​Instance​UID.Data : null; }
            set { _sel.Forge(DF.Study​Instance​UID(value)); }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.Series​Instance​UID != null ? _sel.Series​Instance​UID.Data : null; }
            set { _sel.Forge(DF.Series​Instance​UID(value)); }
        }

        public string SOPInstanceUID
        {
            get { return _sel.SOP​Instance​UID != null ? _sel.SOP​Instance​UID.Data : null; }
            set { _sel.Forge(DF.SOP​Instance​UID(value)); }
        }
    }
}