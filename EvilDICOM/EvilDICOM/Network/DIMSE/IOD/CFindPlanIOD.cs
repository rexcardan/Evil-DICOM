#region

using System;
using EvilDICOM.Core;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindPlanIOD : CFindImageIOD
    {
        public CFindPlanIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public string PlanLabel
        {
            get { return _sel.RTPlanLabel != null ? _sel.RTPlanLabel.Data : null; }
            set { _sel.Forge(DF.RTPlanLabel(value)); }
        }

        public DateTime? PlanDate
        {
            get { return _sel.RTPlanDate != null ? _sel.RTPlanDate.Data : null; }
            set { _sel.Forge(DF.RTPlanDate(value)); }
        }

        public DateTime? PlanTime
        {
            get { return _sel.RTPlanTime != null ? _sel.RTPlanTime.Data : null; }
            set { _sel.Forge(DF.RTPlanTime(value)); }
        }

        public int NumberOfBeams
        {
            get { return _sel.NumberOfBeams != null ? _sel.NumberOfBeams.Data : -1; }
            set { _sel.Forge(DF.NumberOfBeams(value)); }
        }

        public string ApprovalStatus
        {
            get { return _sel.ApprovalStatus != null ? _sel.ApprovalStatus.Data : null; }
            set { _sel.Forge(DF.ApprovalStatus(value)); }
        }
    }
}