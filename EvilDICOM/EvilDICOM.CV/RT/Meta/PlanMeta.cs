using EvilDICOM.CV.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class PlanMeta
    {
        public string PlanId { get; set; }
        public string CourseId { get; set; }
        public string StructureSetUID { get; set; }
        public string PatientId { get; set; }

        public DoseMatrix Dose { get; set; }
        public ImageMatrix CT { get; set; }
        public StructureSetMeta StructureSet { get; set; }
        public List<BeamMeta> Beams { get; private set; } = new List<BeamMeta>();
        public string SOPInstanceUID { get; set; }
        public string SeriesUID { get; set; }
        public string Status { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PatientName { get; set; }
        public string SoftwareVersion { get; internal set; }
        public double? PrescribedDose { get; internal set; }
        public int? NumFractions { get; internal set; }
        public string Hospital { get; set; }
        public double? PrescribedDosePercentage { get; set; }
        public List<ApprovalMeta> Approvals { get; set; } = new List<ApprovalMeta>();
        public List<ReferencePointMeta> ReferencesPoints { get; set; } = new List<ReferencePointMeta>();
        public bool IsVerification { get; set; }
        public bool IsTreated { get; set; } = true;
        public List<CalculationOptionMeta> CalculationSettings { get; set; } = new List<CalculationOptionMeta>();
        public double? Normalization { get; internal set; }
        public string TreatmentOrientationDescription { get; internal set; }
        public bool IsDataComplete
        {
            get
            {
                return Dose != null && CT != null && StructureSet != null && PrescribedDose.HasValue;
            }
        }
    }
}
