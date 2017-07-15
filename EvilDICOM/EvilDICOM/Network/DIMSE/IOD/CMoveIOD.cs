#region

using System;
using EvilDICOM.Network.Enums;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CMoveIOD : AbstractDIMSEIOD
    {
        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.Query​Retrieve​Level == null)
                    _sel.Query​Retrieve​Level.Data = QueryLevel.PATIENT.ToString();
                return (QueryLevel) Enum.Parse(typeof(QueryLevel), _sel.Query​Retrieve​Level.Data);
            }
            set { _sel.Forge(DF.Query​Retrieve​Level(value.ToString())); }
        }

        public string SpecificCharacterSet
        {
            get { return _sel.Specific​Character​Set != null ? _sel.Specific​Character​Set.Data : null; }
            set { _sel.Forge(DF.Specific​Character​Set(value)); }
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

        public string SOPInstanceUID
        {
            get { return _sel.SOP​Instance​UID != null ? _sel.SOP​Instance​UID.Data : null; }
            set { _sel.Forge(DF.SOP​Instance​UID(value)); }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.Series​Instance​UID != null ? _sel.Series​Instance​UID.Data : null; }
            set { _sel.Forge(DF.Series​Instance​UID(value)); }
        }
    }
}