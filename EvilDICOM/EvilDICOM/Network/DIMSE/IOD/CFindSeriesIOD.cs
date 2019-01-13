#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using System.Linq;
#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindSeriesIOD : AbstractDIMSEIOD
    {
        public CFindSeriesIOD()
        {
            QueryLevel = QueryLevel.SERIES;
            StudyInstanceUID = string.Empty;
            SeriesInstanceUID = string.Empty;
            SeriesNumber = null;
            NumberOfSeriesRelatedInstances = null;
            SeriesDescription = string.Empty;
            Modality = string.Empty;
            SeriesTime = SeriesDate = null;
        }

        public CFindSeriesIOD(DICOMObject dcm) : base(dcm)
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

        public string StudyInstanceUID
        {
            get { return _sel.StudyInstanceUID != null ? _sel.StudyInstanceUID.Data : null; }
            set { _sel.Forge(DF.StudyInstanceUID(value)); }
        }

        public int? SeriesNumber
        {
            get
            {
                if (_sel.SeriesNumber != null && _sel.SeriesNumber.Data_.Any()) return _sel.SeriesNumber.Data_[0];
                return null;
            }
            set { _sel.Forge(DF.SeriesNumber()).Data_ = value != null ? new List<int> {(int) value} : new List<int>(); }
        }

        public string PatientId
        {
            get { return _sel.PatientID != null ? _sel.PatientID.Data : null; }
            set { _sel.Forge(DF.PatientID(value)); }
        }

        public int? NumberOfSeriesRelatedInstances
        {
            get
            {
                if (_sel.NumberOfSeriesRelatedInstances != null && _sel.NumberOfSeriesRelatedInstances.Data_.Any())
                    return _sel.NumberOfSeriesRelatedInstances.Data_[0];
                return null;
            }
            set
            {
                _sel.Forge(DF.NumberOfSeriesRelatedInstances()).Data_ =
                    value != null ? new List<int> {(int) value} : new List<int>();
            }
        }

        public string SeriesDescription
        {
            get { return _sel.SeriesDescription != null ? _sel.SeriesDescription.Data : null; }
            set { _sel.Forge(DF.SeriesDescription(value)); }
        }

        public string Modality
        {
            get { return _sel.Modality != null ? _sel.Modality.Data : null; }
            set { _sel.Forge(DF.Modality(value)); }
        }

        public S.DateTime? SeriesDate
        {
            get { return _sel.SeriesDate != null ? _sel.SeriesDate.Data : null; }
            set { _sel.Forge(DF.SeriesDate(value)); }
        }

        public S.DateTime? SeriesTime
        {
            get { return _sel.SeriesTime != null ? _sel.SeriesTime.Data : null; }
            set { _sel.Forge(DF.SeriesTime(value)); }
        }

        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID(value)); }
        }
    }
}