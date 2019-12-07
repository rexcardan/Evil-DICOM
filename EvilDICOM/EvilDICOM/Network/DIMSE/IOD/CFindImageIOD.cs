#region

using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Network.Enums;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindImageIOD : CFindInstanceIOD
    {
        public CFindImageIOD() : base()
        {
            AcquisitionDate = null;
            AcquisitionTime = null;
            ContentDate = null;
            ImageType = string.Empty;
            InstanceNumber = null;
        }

        public CFindImageIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public int? InstanceNumber
        {
            get { return _sel.InstanceNumber != null ? _sel.InstanceNumber.Data : int.MinValue; }
            set
            {
                var convertedValue = value.HasValue ? new int[value.Value] : new int[0];
                _sel.Forge(DF.InstanceNumber(convertedValue));
            }
        }

        public string ImageType
        {
            get { return _sel.ImageType != null ? _sel.ImageType.Data : string.Empty; }
            set { _sel.Forge(DF.ImageType(value)); }
        }

        public S.DateTime? AcquisitionDate
        {
            get { return _sel.AcquisitionDate != null ? _sel.AcquisitionDate.Data : null; }
            set { _sel.Forge(DF.AcquisitionDate(value)); }
        }

        public S.DateTime? AcquisitionTime
        {
            get { return _sel.AcquisitionTime != null ? _sel.AcquisitionTime.Data : null; }
            set { _sel.Forge(DF.AcquisitionTime(value)); }
        }

        public S.DateTime? Content​Date
        {
            get { return _sel.Content​Date != null ? _sel.Content​Date.Data : null; }
            set { _sel.Forge(DF.Content​Date(value)); }
        }

        public S.DateTime? ContentTime
        {
            get { return _sel.ContentTime != null ? _sel.ContentTime.Data : null; }
            set { _sel.Forge(DF.ContentTime(value)); }
        }

        public string ApprovalStatus
        {
            get { return _sel.ApprovalStatus != null ? _sel.ApprovalStatus.Data : string.Empty; }
            set { _sel.Forge(DF.ApprovalStatus(value)); }
        }
    }
}