using OpenCvSharp;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvilDICOM.Core.Enums.Orientation;

namespace EvilDICOM.CV.Helpers
{
    public class Transform
    {
        /// <summary>
        /// Provides the transformation from patient DICOM coordinates to IEC coordinates. Useful for rendering
        /// structures.
        /// </summary>
        /// <param name="orient"></param>
        /// <returns>4x4 transformation matrix</returns>
        public static Mat<float> DICOM2IEC(Orientation orient)
        {
            switch (orient)
            {
                case HFS:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                  new float[] {
                        1, 0, 0, 0,
                        0, 0, -1, 0,
                        0, 1, 0, 0,
                        0, 0, 0, 1 }));
                case HFP:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                        new float[] {
                       -1, 0, 0, 0,
                       0, 0, 1, 0,
                       0, 1, 0, 0,
                       0, 0, 0, 1 }));
                case FFS:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                        new float[] {
                       -1, 0, 0, 0,
                       0, 0, -1, 0,
                       0, -1, 0, 0,
                       0, 0, 0, 1 }));
                case FFP:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                         new float[] {
                       1, 0, 0, 0,
                       0, 0, 1, 0,
                       0, -1, 0, 0,
                       0, 0, 0, 1 }));
                case HFDL:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                          new float[] {
                       0, 0, -1, 0,
                       -1, 0, 0, 0,
                       0, 1, 0, 0,
                       0, 0, 0, 1 }));
                case HFDR:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                           new float[] {
                       0, 0, 1, 0,
                       1, 0, 0, 0,
                       0, 1, 0, 0,
                       0, 0, 0, 1 }));
                case FFDL:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                          new float[] {
                       0, 0, -1, 0,
                       1, 0, 0, 0,
                       0, -1, 0, 0,
                       0, 0, 0, 1 }));
                case FFDR:
                    return new Mat<float>(new Mat(4, 4, MatType.CV_32FC1,
                         new float[] {
                       0, 0, 1, 0,
                       -1, 0, 0, 0,
                       0, -1, 0, 0,
                       0, 0, 0, 1 }));
                default: throw new Exception("Don't have transform for this orientation!");
            }
        }

        public static Mat<float> Iso2DICOM(Orientation orient, Point3f iso)
        {
            using (var tx = DICOM2Isocenter(orient, iso))
            {
                return new Mat<float>(tx.Inv());
            }
        }

        /// <summary>
        /// Provides the transformation from IEC coordinates to patient DICOM coordinates. Useful for rendering
        /// structures.
        /// </summary>
        /// <param name="orient"></param>
        /// <returns>4x4 transformation matrix</returns>
        public static Mat<float> IECToDICOM(Orientation orient)
        {
            using (var tx = DICOM2IEC(orient))
            {
                return new Mat<float>(tx.Inv());
            }
        }

        public static Mat GantryTransform(double gantryAngle)
        {
            var gantryTx = MatMaker.Identity(4, 4);
            double gAngle = gantryAngle * Math.PI / 180;
            gantryTx.Set(0, 0, (float)Math.Cos(gAngle));
            gantryTx.Set(2, 0, (float)Math.Sin(gAngle));
            gantryTx.Set(0, 2, (float)-Math.Sin(gAngle));
            gantryTx.Set(2, 2, (float)Math.Cos(gAngle));
            return gantryTx;
        }
        /// <summary>
        /// Provides the transform from 
        /// </summary>
        /// <param name="gantryAngle">gantry angle in degrees (IEC)</param>
        /// <param name="collimatorAngle">collimator angle in degrees (IEC)</param>
        /// <param name="patientSupportAngle">support anlge in degrees (IEC)</param>
        /// <returns></returns>
        public static Mat BEVTransform(double gantryAngle, double collimatorAngle, double patientSupportAngle, Mat dicomToIEC)
        {
            var gantryTx = GantryTransform(gantryAngle);

            var collTx = MatMaker.Identity(4, 4);
            double cAngle = collimatorAngle * Math.PI / 180;
            collTx.Set(0, 0, (float)Math.Cos(cAngle));
            collTx.Set(1, 0, (float)Math.Sin(cAngle));
            collTx.Set(0, 1, (float)-Math.Sin(cAngle));
            collTx.Set(1, 1, (float)Math.Cos(cAngle));

            var supportTx = MatMaker.Identity(4, 4);
            double sAngle = -patientSupportAngle * Math.PI / 180;
            supportTx.Set(0, 0, (float)Math.Cos(sAngle));
            supportTx.Set(1, 0, (float)Math.Sin(sAngle));
            supportTx.Set(0, 1, (float)-Math.Sin(sAngle));
            supportTx.Set(1, 1, (float)Math.Cos(sAngle));
            var step1 = (dicomToIEC * supportTx).ToMat();
            var step2 = (step1 * gantryTx).ToMat();
            var result = (step2 * collTx).ToMat();
            return result;
        }

        /// <summary>
        /// Provides the transformation from patient DICOM coordinates to IEC coordinates, adjusted for the isocenter position.
        /// Useful for rendering structures.
        /// </summary>
        /// <param name="orient">patient orienation</param>
        /// /// <param name="iso">isocenter position</param>
        /// <returns>4x4 transformation matrix</returns>
        public static Mat<float> DICOM2Isocenter(Orientation orient, Point3f iso)
        {
            var basic = DICOM2IEC(orient);
            var offset = Mat.Eye(4, 4, MatType.CV_32FC1).ToMat();
            offset.Set(0, 3, -iso.X);
            offset.Set(1, 3, -iso.Y);
            offset.Set(2, 3, -iso.Z);

            return new Mat<float>(offset * basic);
        }

        /// <summary>
        /// Transforms a vector offset into a new coordinate system defined by xDir, yDir, and zDir
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="xDir"></param>
        /// <param name="yDir"></param>
        /// <param name="zDir"></param>
        /// <returns></returns>
        public static Vector3 TransformOffset(Vector3 offset, (Vector3 xDir, Vector3 yDir, Vector3 zDir) imageOrientation)
        {
            var xOffset = offset.X;
            var yOffset = offset.Y;
            var zOffset = offset.Z;
            var xDir = imageOrientation.xDir;
            var yDir = imageOrientation.yDir;
            var zDir = imageOrientation.zDir;
            var dx = (xOffset) * xDir.X + (yOffset) * yDir.X + (zOffset) * zDir.X;
            var dy = (xOffset) * xDir.Y + (yOffset) * yDir.Y + (zOffset) * zDir.Y;
            var dz = (xOffset) * xDir.Z + (yOffset) * yDir.Z + (zOffset) * zDir.Z;

            return new Vector3(dx, dy, dz);
        }
    }
}
