using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.RT
{
    /// <summary>
    /// A simple container for dose values as a function of 3D space
    /// </summary>
    public struct DoseValue
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Dose { get; set; }

        public DoseValue(double x, double y, double z, double dose)
            : this()
        {
            X = x; Y = y; Z = z; Dose = dose;
        }
    }
}
