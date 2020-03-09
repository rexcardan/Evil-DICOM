using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace EvilDICOM.CV.Helpers
{
    public class WindowLevelPreset
    {

        public WindowLevelPreset(double window, double level)
        {
            this.Window = window;
            this.Level = level;
        }

        public double Level { get; set; }
        public double Window { get; set; }
    }
}
