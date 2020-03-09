using OpenCvSharp;
using EvilDICOM.CV.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Helpers
{
    public static class FloatMat
    {
        public static void Show(Mat mat, string windowName = "CV")
        {
            double min, max;
            mat.MinMaxIdx(out min, out max);
            var windowed = mat.WindowAndLevel((max - min) / 2, (max - min));
            windowed.Show(windowName);
            windowed.Dispose();
        }

        public static Mat WindowLevelColor(Mat mat)
        {
            double min, max;
            mat.MinMaxIdx(out min, out max);
            var windowed = mat.WindowAndLevel((max - min) / 2, (max - min));
            var color = windowed.CvtColor(ColorConversionCodes.GRAY2BGR);
            windowed.Dispose();
            return color;
        }

        public static Mat ToSDFRender(Mat sdf)
        {
            var drawing = new Mat(sdf.Rows, sdf.Cols, MatType.CV_8UC3);
            double minVal, maxVal;
            sdf.MinMaxIdx(out minVal, out maxVal);
            for (int j = 0; j < sdf.Rows; j++)
            {
                for (int i = 0; i < sdf.Cols; i++)
                {
                    if (sdf.At<float>(j, i) < 0)
                    { drawing.Set(j, i, new Vec3b((byte)(255 - (int)Math.Abs((sdf.At<float>(j, i)) * 255 / minVal)), 0, 0)); }
                    else if (sdf.At<float>(j, i) > 0)
                    { drawing.Set(j, i, new Vec3b(0, 0, (byte)(255 - (int)sdf.At<float>(j, i) * 255 / maxVal))); }
                    else
                    { drawing.Set(j, i, new Vec3b(255, 255, 255)); }
                }
            }
            return drawing;
        }
    }
}
