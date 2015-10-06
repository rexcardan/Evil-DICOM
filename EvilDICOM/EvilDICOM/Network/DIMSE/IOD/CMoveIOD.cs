using System;
using EvilDICOM.Network.Enums;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CMoveIOD : AbstractDIMSEIOD
    {
        public QueryLevel QueryLevel
        {
            get
            {
                if (_sel.QueryRetrieveLevel == null)
                {
                    _sel.QueryRetrieveLevel.Data = QueryLevel.PATIENT.ToString();
                }
                return (QueryLevel) Enum.Parse(typeof (QueryLevel), _sel.QueryRetrieveLevel.Data);
            }
            set { _sel.Forge(DF.QueryRetrieveLevel).Data = value.ToString(); }
        }

        public string SpecificCharacterSeet
        {
            get { return _sel.SpecificCharacterSet != null ? _sel.SpecificCharacterSet.Data : null; }
            set { _sel.Forge(DF.SpecificCharacterSet).Data = value; }
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

        public string SOPInstanceUID
        {
            get { return _sel.SOPInstanceUID != null ? _sel.SOPInstanceUID.Data : null; }
            set { _sel.Forge(DF.SOPInstanceUID).Data = value; }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID).Data = value; }
        }
    }
}