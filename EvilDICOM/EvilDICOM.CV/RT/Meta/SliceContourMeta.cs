using OpenCvSharp;
using EvilDICOM.CV.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    /// <summary>
    /// Represents a closed contour on a given slice 
    /// (can be multiple for the same stucture on the same slice)
    /// </summary>
    public class SliceContourMeta
    {
        public void AddPoint(OpenCvSharp.Point3f pt)
        {
            var z = pt.Z;
            if (float.IsNaN(Z)) { Z = z; }

            if (z != Z) { throw new ArgumentException("Point not in same plane as other points in contour!"); }
            ContourPoints.Add(new Point2f(pt.X, pt.Y));
        }

        public bool CompletelyContains(SliceContourMeta smaller)
        {
            if (smaller.Z != Z) { throw new ArgumentException("Cannot compare contours on different slices!"); }
            var insidePts = smaller.ContourPoints;
            var allInside = insidePts.All(pt => Cv2.PointPolygonTest(ContourPoints, pt, false) == 1);
            return allInside;
        }

        public List<OpenCvSharp.Point2f> ContourPoints { get; internal set; } = new List<Point2f>();
        public float Z { get; private set; } = float.NaN;

        public double CalculateArea()
        {
            var area = Cv2.ContourArea(ContourPoints);
            var grandchildren = Children.SelectMany(c => c.Children);
            var outermostChildren = Children.Where(c => !grandchildren.Any(gc => gc == c));

            foreach (var child in outermostChildren)
            {
                area -= child.CalculateArea();
            }
            return area;
        }


        /// <summary>
        /// A container for contours within this contour
        /// </summary>
        public List<SliceContourMeta> Children { get; set; } = new List<SliceContourMeta>();

        public Mat DrawOnSlice(Mat txMatrix, Mat slice, double scale = 1)
        {
            var allPoints = new Vec4f[ContourPoints.Count];
            var points = ContourPoints.Select(c => new Vec4f(c.X, c.Y, Z, 1));
            using (var pointMat = new Mat<float>(new[] { ContourPoints.Count }, points.ToArray()))
            {
                var txPointMat = pointMat.Transform(txMatrix.Inv());
                txPointMat.GetArray(out allPoints);
            }
            var points2D = allPoints.Select(a => new Point(a.Item0 * scale, a.Item1 * scale)).ToList();
            double min, max;
            slice.MinMaxIdx(out min, out max);
            var color = slice.Type()==MatType.CV_8UC3? slice: slice.WindowAndLevel((max - min) / 2, max - min);
            Cv2.DrawContours(color, new[] { points2D }, 0, new Scalar(255,255,0));

            var grandchildren = Children.SelectMany(c => c.Children);
            var outermostChildren = Children.Where(c => !grandchildren.Any(gc => gc == c));
            return color;
        }

        /// <summary>
        /// Masks an image of the same voxel count as the input doseGrid. Voxels inside the
        /// contour are filled with the specified color. Intent is white (0x255) on inside and outside
        /// areas/holes are left black (0x00)
        /// </summary>
        /// <param name="mask">the mask to fill or modify (recursively)</param>
        /// <param name="txMatrix">the patient transform matrix from the imagematrix/dosematrix</param>
        /// <param name="color">the color to fill the contour</param>
        /// <param name="scale">the scale of the image (in case the image has been resampled)</param>
        public void MaskImageFast(Mat mask, Mat txMatrix, byte color = 255, double scale = 1)
        {
            if (ContourPoints.Count == 0) { mask.SetArray(new byte[mask.Cols * mask.Rows]); return; }


            var allPoints = new Vec4f[ContourPoints.Count];
            var points = ContourPoints.Select(c => new Vec4f(c.X, c.Y, Z, 1));
            using (var pointMat = new Mat<float>(new[] { ContourPoints.Count }, points.ToArray()))
            {
                var txPointMat = pointMat.Transform(txMatrix.Inv());
                txPointMat.GetArray(out allPoints);
            }
            var points2D = allPoints.Select(a => new Point(a.Item0* scale, a.Item1 * scale)).ToList();

            Cv2.FillPoly(mask, new[] { points2D }, new Scalar(color), LineTypes.Link8);

            var grandchildren = Children.SelectMany(c => c.Children);
            var outermostChildren = Children.Where(c => !grandchildren.Any(gc => gc == c));

            foreach (var child in outermostChildren)
            {
                //Recursively alternates black white to account for holes and fills of children contours
                child.MaskImageFast(mask, txMatrix, color == 255 ? (byte)0 : (byte)255);
            }
        }

        /// <summary>
        /// Masks an image of the same voxel count as the input doseGrid. Voxels inside the
        /// contour are filled with the specified color. Intent is white (0x255) on inside and outside
        /// areas/holes are left black (0x00)
        /// </summary>
        /// <param name="mask">the mask to fill or modify (recursively)</param>
        /// <param name="doseGridPts">the 2d (x,y) patient coordinates of the dose matrix slices</param>
        /// <param name="color">the color to fill the contour</param>
        public void MaskImage(Mat mask, Vec2f[] doseGridPts, byte color = 255)
        {
            var maskVals = new byte[doseGridPts.Length];
            var contourPoints = new Mat<Point3f>();
            var oppColor = color == 255 ? (byte)0 : (byte)255;

            for (int i = 0; i < doseGridPts.Length; i++)
            {
                var dg = doseGridPts[i];
                maskVals[i] = Cv2.PointPolygonTest(ContourPoints,
                    new Point2f(dg.Item0, dg.Item1), false) == 1 ? color : oppColor;
            }

            mask.SetArray(maskVals);

            var grandchildren = Children.SelectMany(c => c.Children);
            var outermostChildren = Children.Where(c => !grandchildren.Any(gc => gc == c));

            foreach (var child in outermostChildren)
            {
                //Recursively alternates black white to account for holes and fills of children contours
                child.MaskImage(mask, doseGridPts, color == 255 ? (byte)0 : (byte)255);
            }

            contourPoints.Dispose();
        }
    }
}
