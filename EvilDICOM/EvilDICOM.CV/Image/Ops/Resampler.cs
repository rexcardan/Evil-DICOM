using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image.Ops
{
    public class Resampler
    {
        public static void Resample(Matrix m, double xRes, double yRes, double zRes)
        {
            var matrix = new Matrix();
            matrix.BytesAllocated = m.BytesAllocated;
            matrix.DimensionX = (int)((m.XMax - m.XMin) / xRes) + 1;
            matrix.DimensionY = (int)((m.YMax - m.YMin) / yRes) + 1;
            matrix.DimensionZ = (int)((m.ZMax - m.ZMin) / zRes) + 1;
            matrix.Origin = m.Origin.Copy();
            matrix.ImageOrientation =
                (m.ImageOrientation.xDir.Copy(),
                m.ImageOrientation.yDir.Copy(),
                m.ImageOrientation.zDir.Copy());
            matrix.XRes = xRes;
            matrix.YRes = yRes;
            matrix.ZRes = zRes;
            var xScale = matrix.DimensionX * 1.0 / m.DimensionX;
            var yScale = matrix.DimensionY * 1.0 / m.DimensionY;
            var values = new float[matrix.DimensionX * matrix.DimensionY * matrix.DimensionZ];
            matrix.CreateMatrix(values);


            for (int z = 0; z < matrix.DimensionZ; z++)
            {
                var zPos = matrix.Origin.Z +
                    (Transform.TransformOffset(new Vector3(0, 0, z), matrix.ImageOrientation).Z) * matrix.ZRes;
                var slice = m.GetZPlane(zPos);
                var resize = slice.Resize(Size.Zero, xScale, yScale);
                slice.Dispose();
                matrix.SetZPlaneBySlice(resize.Clone(), z);
                resize.Dispose();
            }
            //Copy resample matrix to this (replacing current data)
            m._mat.Dispose();
            m._mat = matrix._mat.Clone();
            matrix._mat.Dispose();
            m.XRes = matrix.XRes;
            m.YRes = matrix.YRes;
            m.ZRes = matrix.ZRes;
            m.DimensionX = matrix.DimensionX;
            m.DimensionY = matrix.DimensionY;
            m.DimensionZ = matrix.DimensionZ;
            m.CalculatePatientTransformMatrix();
            m.CalculateBounds();
        }
    }
}
