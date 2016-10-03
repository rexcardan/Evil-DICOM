using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DF = EvilDICOM.Core.DICOMForge;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindPlanIOD : CFindImageIOD
    {

        public CFindPlanIOD(DICOMObject dcm) : base(dcm) { }

        public string PlanLabel
        {
            get { return _sel.RTPlanLabel != null ? _sel.RTPlanLabel.Data : null; }
            set { _sel.Forge(DF.RTPlanLabel).Data = value; }
        }

        public System.DateTime? PlanDate
        {
            get { return _sel.RTPlanDate != null ? _sel.RTPlanDate.Data : null; }
            set { _sel.Forge(DF.RTPlanDate).Data = value; }
        }

        public System.DateTime? PlanTime
        {
            get { return _sel.RTPlanTime != null ? _sel.RTPlanTime.Data : null; }
            set { _sel.Forge(DF.RTPlanTime).Data = value; }
        }

        public int NumberOfBeams
        {
            get { return _sel.NumberOfBeams != null ? _sel.NumberOfBeams.Data : -1; }
            set { _sel.Forge(DF.NumberOfBeams).Data = value; }
        }

        public string ApprovalStatus
        {
            get { return _sel.ApprovalStatus != null ? _sel.ApprovalStatus.Data : null; }
            set { _sel.Forge(DF.ApprovalStatus).Data = value; }
        }
    }
}
