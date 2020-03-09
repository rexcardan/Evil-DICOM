using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT
{
    public struct TargetIndices
    {
        public double Gradient { get; set; }
        public double PaddingCI { get; set; }
        public double RTOGCI { get; set; }
    }
}
