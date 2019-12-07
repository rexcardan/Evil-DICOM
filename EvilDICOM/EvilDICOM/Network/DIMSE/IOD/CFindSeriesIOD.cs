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
    public class CFindSeriesIOD : CFindRequestIOD
    {
        public CFindSeriesIOD()
        {
            QueryLevel = QueryLevel.SERIES;
            SeriesInstanceUID = string.Empty;
            SeriesNumber = null;
            SeriesDescription = string.Empty;
            Modality = string.Empty;
        }

        public CFindSeriesIOD(DICOMObject dcm) : base(dcm)
        {
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

        public string SeriesInstanceUID
        {
            get { return _sel.SeriesInstanceUID != null ? _sel.SeriesInstanceUID.Data : null; }
            set { _sel.Forge(DF.SeriesInstanceUID(value)); }
        }
    }
}