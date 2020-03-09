using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.RT.Data
{
    public static class PlanStatus
    {
        public const string UNAPPROVED = "Unapproved";
        public const string UNPLANNED_TREAT = "UnplannedTreat";
        public const string PLAN_APPROVED = "PlanApproval";
        public const string TREAT_APPROVED = "TreatApproval";
        public const string EXT_APPROVED = "ExternalApproval";
        public const string RETIRED = "Retired";
        public const string REVIEWED = "Reviewed";
        public const string REJECTED = "Rejected";
        public const string COMPLETED_EARLY = "CompletedEarly";
        public const string COMPLETED = "Completed";
    }
}
