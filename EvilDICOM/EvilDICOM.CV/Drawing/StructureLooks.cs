using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Drawing
{
    public class StructureLooks
    {
        public static StructureLook GTV { get; } = new StructureLook() { OutlineThickness = 1, OutlineColor = new Scalar(0, 0, 255) };

        public static StructureLook Purple { get; } = new StructureLook() { OutlineThickness = 1, OutlineColor = new Scalar(255, 0, 255) };
        public static StructureLook Default { get; } = new StructureLook() { OutlineThickness = 1, OutlineColor = new Scalar(0, 255, 0) };
    }
}
