using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    public static class MatExtensions
    {
        public static Point3f TransformPoint3f(this Mat pose, Point3f point)
        {
            if (pose.Cols != 4 || pose.Rows != 4)
            {
                throw new ArgumentException("Matrix must be a 4 x 4 matrix");
            }

            if (pose.Type() != MatType.CV_32FC1)
            {
                throw new ArgumentException("Matrix must be of float 32 bit type");
            }

            var m1 = new float[4, 4];
            var data = new float[16];
            pose.GetArray(out data);
            Array.Copy(data, m1, data.Length);

            var v1 = new float[4] { point.X, point.Y, point.Z, 1 };

            var homogenous = new Vec4f(
               (m1[0, 0] * v1[0] + m1[0, 1] * v1[1] + m1[0, 2] * v1[2] + m1[0, 3] * v1[3]),
               (m1[1, 0] * v1[0] + m1[1, 1] * v1[1] + m1[1, 2] * v1[2] + m1[1, 3] * v1[3]),
               (m1[2, 0] * v1[0] + m1[2, 1] * v1[1] + m1[2, 2] * v1[2] + m1[2, 3] * v1[3]),
               (m1[3, 0] * v1[0] + m1[3, 1] * v1[1] + m1[3, 2] * v1[2] + m1[3, 3] * v1[3])
               );

            return new Point3f(homogenous.Item0, homogenous.Item1, homogenous.Item2);
        }

        public static Point3d TransformPoint3d(this Mat pose, Point3d point)
        {
            if (pose.Cols != 4 || pose.Rows != 4)
            {
                throw new ArgumentException("Matrix must be a 4 x 4 matrix");
            }

            if (pose.Type() != MatType.CV_64FC1)
            {
                throw new ArgumentException("Matrix must be of float 32 bit type");
            }

            var m1 = new float[4, 4];
            var data = new float[16];
            pose.GetArray(out data);
            Array.Copy(data, m1, data.Length);

            var v1 = new double[4] { point.X, point.Y, point.Z, 1 };

            var homogenous = new Vec4d(
               (m1[0, 0] * v1[0] + m1[0, 1] * v1[1] + m1[0, 2] * v1[2] + m1[0, 3] * v1[3]),
               (m1[1, 0] * v1[0] + m1[1, 1] * v1[1] + m1[1, 2] * v1[2] + m1[1, 3] * v1[3]),
               (m1[2, 0] * v1[0] + m1[2, 1] * v1[1] + m1[2, 2] * v1[2] + m1[2, 3] * v1[3]),
               (m1[3, 0] * v1[0] + m1[3, 1] * v1[1] + m1[3, 2] * v1[2] + m1[3, 3] * v1[3])
               );

            return new Point3d(homogenous.Item0, homogenous.Item1, homogenous.Item2);
        }

        public static void Show(this Mat mat, string windowName="CV Window")
        {
            Cv2.ImShow(windowName, mat);
            Cv2.WaitKey(0);
        }
    }
}
