using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Geometry
{
    public struct Triangle
    {
        public Vector3 P1 { get; set; }
        public Vector3 P2 { get; set; }
        public Vector3 P3 { get; set; }

        public double SignedVolume()
        {
            return P1*(P2.CrossMultiply(P3)) / 6.0f;
        }
    }
}
