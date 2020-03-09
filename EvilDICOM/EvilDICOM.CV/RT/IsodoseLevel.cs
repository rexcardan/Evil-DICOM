using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT
{
    public class IsodoseLevel
    {
        public double Value { get; set; }
        public Scalar Color { get; set; } = new Scalar(0, 0, 0);
        public static List<IsodoseLevel> DefaultLevels()
        {
            return new List<IsodoseLevel>()
            {
                //BGR
                new IsodoseLevel(){Value=1.05, Color = new Scalar(0,12,140) }, //DARK RED
                new IsodoseLevel(){Value=1.00, Color = new Scalar(0,0,255) }, //RED
                new IsodoseLevel(){Value=0.95, Color = new Scalar(0,255,255) }, //YELLOW,
                new IsodoseLevel(){Value=0.90, Color = new Scalar(0,255,0) }, //GREEN
                new IsodoseLevel(){Value=0.80, Color = new Scalar(255,255,0) }, //CYAN,
                new IsodoseLevel(){Value=0.50, Color = new Scalar(255,0,0) } //BLUE
            };
        }

        public static List<IsodoseLevel> DefaultLevels(double rxDose)
        {
            return new List<IsodoseLevel>()
            {
                //BGR
                new IsodoseLevel(){Value=1.05*rxDose, Color = new Scalar(0,12,140) }, //DARK RED
                new IsodoseLevel(){Value=1.00*rxDose, Color = new Scalar(0,0,255) }, //RED
                new IsodoseLevel(){Value=0.95*rxDose, Color = new Scalar(0,255,255) }, //YELLOW,
                new IsodoseLevel(){Value=0.90*rxDose, Color = new Scalar(0,255,0) }, //GREEN
                new IsodoseLevel(){Value=0.80*rxDose, Color = new Scalar(255,255,0) }, //CYAN,
                new IsodoseLevel(){Value=0.50*rxDose, Color = new Scalar(255,0,0) } //BLUE
            };
        }
    }
}
