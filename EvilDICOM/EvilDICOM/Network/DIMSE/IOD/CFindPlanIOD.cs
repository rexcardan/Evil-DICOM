#region

using System;
using EvilDICOM.Core;
using DF = EvilDICOM.Core.DICOMForge;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindPlanIOD : CFindInstanceIOD
    {
        public CFindPlanIOD() : base()
        {
            PlanLabel = string.Empty;
            PlanDescription = string.Empty;
            PlanDate = null;
            PlanTime = null;
            NumberOfBeams = 0;
            ApprovalStatus = string.Empty;
            ReferencedPlan = null;
        }

        public CFindPlanIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public string PlanLabel
        {
            get { return _sel.RTPlanLabel != null ? _sel.RTPlanLabel.Data : null; }
            set { _sel.Forge(DF.RTPlanLabel(value)); }
        }

        public string PlanDescription
        {
            get { return _sel.RTPlanDescription != null ? _sel.RTPlanDescription.Data : null; }
            set { _sel.Forge(DF.RTPlanDescription(value)); }
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

        public (string sopClassUID, string sopInstanceUID, string planRelationship)? ReferencedPlan
        {
            get
            {
                return _sel.ReferencedRTPlanSequence != null ?
                  (_sel.ReferencedRTPlanSequence.Select(seq => seq.ReferencedSOPClassUID.Data),
                  _sel.ReferencedRTPlanSequence.Select(seq => seq.ReferencedSOPInstanceUID.Data),
                  _sel.ReferencedRTPlanSequence.Select(seq => seq.RTPlanRelationship.Data))
                  : (string.Empty, string.Empty, string.Empty);
            }
            set
            {
                if (value.HasValue)
                {
                    _sel.Forge(DF.ReferencedRTPlanSequence(new DICOMObject(
                         DF.ReferencedSOPClassUID(value.Value.sopClassUID),
                         DF.ReferencedSOPInstanceUID(value.Value.sopInstanceUID),
                         DF.RTPlanRelationship(value.Value.planRelationship))));
                }
                else
                {
                    _sel.Forge(DF.ReferencedRTPlanSequence(new DICOMObject[0]));
                }
            }
        }
    }
}