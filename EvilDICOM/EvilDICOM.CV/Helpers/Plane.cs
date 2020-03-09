using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Helpers
{
    public class Plane
    {
        public Point3f Origin { get; set; }
        public Point3f XDir { get; set; }
        public Point3f YDir { get; set; }
    }
}
