using OpenCvSharp;
using EvilDICOM.Core.Selection;
using EvilDICOM.CV.RT;
using System.Collections.Generic;
using System.Linq;

namespace EvilDICOM.CV.Drawing.Renderers
{
    public class FieldRenderer
    {
        public static void Render(DICOMSelector sel, DRR drr)
        {
            //Field rendering
            var exposure = sel.ExposureSequence;
            var beamLimits = exposure?.Select(s => s.BeamLimitingDeviceSequence);
            if (beamLimits != null)
            {

                var collAngle = sel.BeamLimitingDeviceAngle.Data;
                var mmPerPixel = sel.ImagePlanePixelSpacing.Data_;

                var points = new List<List<Point>>();
                var xLimits = beamLimits.Items.FirstOrDefault(b => b.GetSelector().RTBeamLimitingDeviceType.Data == "X" || b.GetSelector().RTBeamLimitingDeviceType.Data == "ASYMX");
                var yLimits = beamLimits.Items.FirstOrDefault(b => b.GetSelector().RTBeamLimitingDeviceType.Data == "Y" || b.GetSelector().RTBeamLimitingDeviceType.Data == "ASYMY");
                if (xLimits != null && yLimits != null)
                {
                    var xjaws = xLimits.GetSelector().LeafJawPositions.Data_;
                    var yjaws = yLimits.GetSelector().LeafJawPositions.Data_;

                    var x1y1 = new Point(xjaws[0] / mmPerPixel[0], yjaws[0] / mmPerPixel[0]);
                    var x1y2 = new Point(xjaws[0] / mmPerPixel[0], yjaws[1] / mmPerPixel[0]);
                    var x2y2 = new Point(xjaws[1] / mmPerPixel[0], yjaws[1] / mmPerPixel[0]);
                    var x2y1 = new Point(xjaws[1] / mmPerPixel[0], yjaws[0] / mmPerPixel[0]);


                    x1y1 = DRR.Rotate(x1y1, collAngle) + drr.Iso2D;
                    x1y2 = DRR.Rotate(x1y2, collAngle) + drr.Iso2D;
                    x2y2 = DRR.Rotate(x2y2, collAngle) + drr.Iso2D;
                    x2y1 = DRR.Rotate(x2y1, collAngle) + drr.Iso2D;

                    points.Add(new List<Point>() { x1y1, x1y2, x2y2, x2y1 });
                }

                //points.Add(field.Points().Select(p => new Point(p.X, p.Y)).ToList());
                drr.Image.DrawContours(points, 0, new Scalar(255, 0, 0));
            }
        }
    }
}
