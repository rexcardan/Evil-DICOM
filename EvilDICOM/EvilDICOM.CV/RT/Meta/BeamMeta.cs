using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class BeamMeta
    {
        public double Energy { get; set; }
        public string ModalityAbbr { get; set; }
        public string Id { get; set; }
        public string MachineId { get; set; }
        public string Modality { get { return $"{(int)Energy}{ModalityAbbr}"; } }
        public double Weight { get; set; }
        public double StartingGantryAngle { get; set; }
        public double EndingGantryAngle { get; set; }
        public string Wedge { get; set; }
        public double CollimatorAngle { get; set; }
        public double TableAngle { get; set; }
        public double SSD { get; set; }
        public double? MU { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
        public string MLCType { get; set; }
        public bool HasBlock { get; set; }
        public bool HasBolus { get; set; }
        public bool IsSetup { get; set; }
        public bool HasWedge { get; set; }
        public Vector3 Isocenter { get; set; }
        public double? Dose { get; set; }
    }
}
