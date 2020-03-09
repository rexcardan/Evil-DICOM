using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT;
using EvilDICOM.RT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    public static class DoseMatrixExtensions
    {
        private static Mat ProjectDoseToZPlaneMM(DoseMatrix d1, ImageMatrix im, double imagePlaneZ)
        {
            var projected = new float[im.DimensionX * im.DimensionY];

            var planeOrigin = im.Origin.Copy();
            planeOrigin.Z = imagePlaneZ;

            if (planeOrigin.Z >= d1.Origin.Z && planeOrigin.Z <= d1.ZMax)//Within dose Z bounds
            {
                //Extract dose at that plane
                var zPlane = d1.GetZPlane(planeOrigin.Z);

                //Pad image
                var x1Padmm = d1.Origin.X - im.Origin.X;
                var x2Padmm = im.XMax - d1.XMax;
                var y1Padmm = d1.Origin.Y - im.Origin.Y;
                var y2Padmm = im.YMax - d1.YMax;

                var x1Pad = (int)Math.Ceiling(x1Padmm / d1.XRes);
                var x2Pad = (int)Math.Ceiling(x2Padmm / d1.XRes);
                var y1Pad = (int)Math.Ceiling(y1Padmm / d1.YRes);
                var y2Pad = (int)Math.Ceiling(y2Padmm / d1.YRes);

                try
                {
                    zPlane = zPlane.CopyMakeBorder(y1Pad, y2Pad, x1Pad, x2Pad, BorderTypes.Constant, new Scalar(0));
                    //Calculate origin of new image
                    var x0 = d1.Origin.X - x1Pad * d1.XRes;
                    var y0 = d1.Origin.Y - y1Pad * d1.YRes;
                    var xCenter = ((im.XMax - im.Origin.X) / 2) + im.Origin.X;
                    var yCenter = ((im.YMax - im.Origin.Y) / 2) + im.Origin.Y;
                    var xCenterPix = (float)((xCenter - x0) / d1.XRes * d1.XRes / im.XRes);
                    var yCenterPix = (float)((yCenter - y0) / d1.YRes * d1.YRes / im.YRes);

                    //Scale to same resolution
                    zPlane = zPlane.Resize(Size.Zero, d1.XRes / im.XRes, d1.YRes / im.YRes);

                    Mat projectedMat = new Mat();
                    Cv2.GetRectSubPix(zPlane, new Size(im.DimensionX, im.DimensionY), new Point2f(xCenterPix, yCenterPix), projectedMat);
                    return projectedMat;
                }
                catch (Exception e)
                {
                    throw new NotFiniteNumberException();
                }



            }

            //Return empty plane
            return new Mat(im.DimensionY, im.DimensionX, MatType.CV_32FC1, projected);
        }

        public static DoseMatrix ResampleToImage(this DoseMatrix d1, ImageMatrix im)
        {
            var dm = new DoseMatrix();
            dm.Origin = im.Origin;
            dm.PlanUID = d1.PlanUID;
            dm.BytesAllocated = d1.BytesAllocated;
            dm.DimensionX = im.DimensionX;
            dm.DimensionY = im.DimensionY;
            dm.DimensionZ = im.DimensionZ;
            dm.XRes = im.XRes;
            dm.YRes = im.YRes;
            dm.ZRes = im.ZRes;
            dm.PrescriptionDoseGy = d1.PrescriptionDoseGy;
            dm.DoseUnit = d1.DoseUnit;
            var values = new float[dm.DimensionX * dm.DimensionY * dm.DimensionZ];

            var imageSlices = Enumerable.Range(0, im.DimensionZ).Select(i =>
            {
                return new { Z = i * im.ZRes + im.Origin.Z, NSlice = i };
            }).ToList();

            foreach (var slice in imageSlices)
            {
                using (var mat = ProjectDoseToZPlaneMM(d1, im, slice.Z))
                {
                    var projected = new float[im.DimensionX * im.DimensionY];
                    mat.GetArray(out projected);

                    var indexStart = IndexHelper.LatticeXYZToIndex(0, 0, slice.NSlice, dm.DimensionX, dm.DimensionY);
                    var indexEnd = projected.Length;
                    for (int i = 0; i < projected.Length; i++)
                    {
                        values[i + indexStart] = projected[i];
                    }
                }
            }

            dm.MaxDose = values.Max();
            var indexOfMax = values.ToList().IndexOf(dm.MaxDose);
            var (_, _, z) = IndexHelper.IndexToLatticeXYZ(indexOfMax, dm.DimensionX, dm.DimensionY);
            dm.MaxDoseSlice = z;
            dm.ImageOrientation = (d1.ImageOrientation.xDir.Copy(), d1.ImageOrientation.yDir.Copy(), d1.ImageOrientation.zDir.Copy());
            dm.CreateMatrix(values);
            return dm;

        }
    }
}
