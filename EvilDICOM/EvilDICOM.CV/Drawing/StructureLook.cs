using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Drawing
{
    public class StructureLook
    {
        public int OutlineThickness { get; set; } = 1;

        public CvScalar OutlineColor { get; set; } = new CvScalar(0, 255, 0);

        public CvScalar FillColor { get; set; } = new CvScalar(0, 255, 0);
    }
}
