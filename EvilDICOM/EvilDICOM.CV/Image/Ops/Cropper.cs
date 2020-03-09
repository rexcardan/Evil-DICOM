using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image.Ops
{
    public static class Cropper
    {
        public static void CropMatrixToStructure(Matrix m, StructureMeta contour, double marginMM)
        {
            var allPoints = contour.SliceContours.SelectMany(sc => sc.ContourPoints).ToList();
            var minPointX = allPoints.Min(pt => pt.X) - marginMM;
            var maxPointX = allPoints.Max(pt => pt.X) + marginMM;
            var minPointY = allPoints.Min(pt => pt.Y) - marginMM;
            var maxPointY = allPoints.Max(pt => pt.Y) + marginMM;
            var minPointZ = contour.SliceContours.Min(sc => sc.Z) - marginMM;
            var maxPointZ = contour.SliceContours.Max(sc => sc.Z) + marginMM;

            var minCorner = new Point3d(minPointX, minPointY, minPointZ);
            var maxCorner = new Point3d(maxPointX, maxPointY, maxPointZ);

            var imageCoordMinCorner = m.PatientTransformMatrix.Inv().ToMat().TransformPoint3d(minCorner);
            var imageCoordMaxCorner = m.PatientTransformMatrix.Inv().ToMat().TransformPoint3d(maxCorner);

            var minX = (int)Math.Floor(Math.Min(imageCoordMinCorner.X, imageCoordMaxCorner.X));
            minX = minX < 0 ? 0 : minX;
            var minY = (int)Math.Floor(Math.Min(imageCoordMinCorner.Y, imageCoordMaxCorner.Y));
            minY = minY < 0 ? 0 : minY;
            var minZ = (int)Math.Floor(Math.Min(imageCoordMinCorner.Z, imageCoordMaxCorner.Z));
            minZ = minZ < 0 ? 0 : minZ;

            var maxX = (int)Math.Ceiling(Math.Max(imageCoordMinCorner.X, imageCoordMaxCorner.X));
            maxX = maxX > (m.DimensionX - 1) ? m.DimensionX : maxX;
            var maxY = (int)Math.Ceiling(Math.Max(imageCoordMinCorner.Y, imageCoordMaxCorner.Y));
            maxY = maxY > (m.DimensionY - 1) ? m.DimensionY : maxY;
            var maxZ = (int)Math.Ceiling(Math.Max(imageCoordMinCorner.Z, imageCoordMaxCorner.Z));
            maxZ = maxZ > (m.DimensionZ - 1) ? m.DimensionZ : maxZ;

            var rangeX = new OpenCvSharp.Range(minX, maxX);
            var rangeY = new OpenCvSharp.Range(minY, maxY);
            var rangeZ = new OpenCvSharp.Range(minZ, maxZ);

            using (var cropped = new Mat(m._mat, rangeZ, rangeY, rangeX))
            {
                m.DimensionX = maxX - minX;
                m.DimensionY = maxY - minY;
                m.DimensionZ = maxZ - minZ;

                var xOffset = minX * m.XRes;
                var yOffset = minY * m.YRes;
                var zOffset = minZ * m.ZRes;
                var txOffset = Transform.TransformOffset(new Vector3(xOffset, yOffset, zOffset), m.ImageOrientation);
                m.Origin = m.Origin + txOffset;

                var mat = cropped.Clone();
                m._mat.Dispose();
                m._mat = mat;
                m.CalculatePatientTransformMatrix();
                m.CalculateBounds();
            }
        }
    }
}
