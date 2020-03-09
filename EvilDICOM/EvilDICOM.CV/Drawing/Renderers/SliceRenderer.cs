using OpenCvSharp;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Drawing.Renderers
{
    public class SliceRenderer
    {
        public static void RenderContourOnDose(Matrix dm, params SliceContourMeta[] contours)
        {
            var z = contours.Select(c => c.Z).First();
            var slice = dm.GetZPlane(z);
            var color = FloatMat.ToSDFRender(slice);
            slice.Dispose();
            foreach (var contour in contours)
            {
                if (contour.ContourPoints.Any())
                    contour.DrawOnSlice(dm.PatientTransformMatrix, color);
            }

            Cv2.ImShow("Dose Contour", color);
            Cv2.WaitKey();
        }
    }
}
