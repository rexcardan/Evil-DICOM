using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using EvilDICOM.Core.Extensions;

namespace EvilDICOM.CV.RT
{
    public class DRR
    {
        internal DRR() { }

        public Mat Image { get; internal set; }
        public string Label { get; internal set; }
        public Point Iso2D { get { return Image==null?new Point(): new Point(Image.Cols / 2, Image.Rows / 2); } }

        public static Point Rotate(Point pt, double collAngleDeg)
        {
            var collAngleRad = Math.PI - collAngleDeg * (Math.PI / 180);
            var xp = pt.X * Math.Cos(collAngleRad) - pt.Y * Math.Sin(collAngleRad);
            var yp = pt.X * Math.Sin(collAngleRad) + pt.Y * Math.Cos(collAngleRad);
            return new Point(xp, yp);
        }
    }
}
