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

        public Scalar OutlineColor { get; set; } = new Scalar(0, 255, 0);

        public Scalar FillColor { get; set; } = new Scalar(0, 255, 0);
    }
}
