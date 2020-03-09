using OpenCvSharp;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Helpers
{
    public static class ContourHelper
    {
        /// <summary>
        /// Recursive method that takes the largest contour and an array of smaller contours to
        /// place smaller contours which are inside the largest contour into the child list
        /// for area calculations
        /// </summary>
        /// <param name="largerContour">the large contour possibly containing smaller contours</param>
        /// <param name="smallerContours">the list of smaller contours</param>
        public static void OrganizeIntoChildren(SliceContourMeta largerContour, IEnumerable<SliceContourMeta> smallerContours)
        {
            if (!smallerContours.Any()) { return; }

            foreach (var smaller in smallerContours)
            {
                if (largerContour.CompletelyContains(smaller))
                {
                    OrganizeIntoChildren(smaller, smallerContours.Where(s => s != smaller));
                    largerContour.Children.Add(smaller);
                }
            }
        }

        /// <summary>
        /// Gets the contours on a slice z. Interpolates if contours don't exist
        /// </summary>
        /// <param name="z">z position of contours</param>
        /// <returns></returns>.
        public static Mat GetMaskAtZ(this IEnumerable<SliceContourMeta> contours, float z, Mat zSlice, Mat imageToPatientTx, double scale)
        {
            var zSlices = contours.GroupBy(sc => sc.Z).OrderBy(grp => grp.Key).ToList();

            if (zSlices.Any(o => Math.Abs(z - o.Key) < 0.01))
            {
                var matchedContours = zSlices.First(o => Math.Abs(z - o.Key) < 0.01);
                using (var mask = new Mat(zSlice.Rows, zSlice.Cols, MatType.CV_8UC1, new Scalar(0)))
                {
                    foreach (var contour in matchedContours)
                    {
                        contour.MaskImageFast(mask, imageToPatientTx, 255, scale);
                    }
                    return mask;
                }
            }
            else
            {//Else interpolate
                var smaller = zSlices.Last(s => s.Key <= z);
                var larger = zSlices.First(s => s.Key >= z);
                var zd = (z - smaller.Key) / (larger.Key - smaller.Key);

                using (var mask1 = new Mat(zSlice.Rows, zSlice.Cols, MatType.CV_8UC1, new Scalar(0)))
                {
                    foreach (var contour in smaller)
                    {
                        contour.MaskImageFast(mask1, imageToPatientTx, 255, scale);
                    }

                    using (var mask2 = new Mat(zSlice.Rows, zSlice.Cols, MatType.CV_8UC1, new Scalar(0)))
                    {
                        foreach (var contour in larger)
                        {
                            contour.MaskImageFast(mask2, imageToPatientTx, 255, scale);
                        }

                        Mat interpolated = new Mat(zSlice.Rows, zSlice.Cols, MatType.CV_8UC1, new Scalar(0));
                        Cv2.AddWeighted(mask1, 1 - zd, mask2, zd, 0, interpolated);
                        return interpolated;
                    }
                    
                }
            }
        }
    }
}
