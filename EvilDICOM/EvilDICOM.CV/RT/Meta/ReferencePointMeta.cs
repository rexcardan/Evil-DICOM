using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class ReferencePointMeta
    {
        public string Id { get; set; }
        public Vector3 Location { get; set; }
        public double FractionDoseGy { get; set; }
        public string PointType { get; internal set; }
    }
}
