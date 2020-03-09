using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Drawing;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.RT;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image
{
    /// <summary>
    /// Class that holds image contours on a given 3D image
    /// </summary>
    public class ImageContours
    {
        private Dictionary<int, List<OpenCvSharp.Point>> sliceZContours = new Dictionary<int, List<OpenCvSharp.Point>>();
        private Dictionary<int, List<OpenCvSharp.Point>> sliceYContours = new Dictionary<int, List<OpenCvSharp.Point>>();
        private Dictionary<int, List<OpenCvSharp.Point>> sliceXContours = new Dictionary<int, List<OpenCvSharp.Point>>();
        private List<SliceContourMeta> _contours;
        private Matrix _im;

        private ImageContours(StructureMeta sm, Matrix im)
        {
            _contours = sm.SliceContours;
            _im = im;
        }

        /// <summary>
        /// Creates a rough approximation of the structure inside a given 3d matrix. Useful for calculating bounds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sm"></param>
        /// <param name="im"></param>
        /// <returns></returns>
        public static ImageContours BuildFromImage(StructureMeta sm, Matrix im)
        {
            var ic = new ImageContours(sm, im);
            ic.ConstructStructureSlicesOnImage(im);
            return ic;
        }

        public int MinX { get { return sliceXContours.Min(s => s.Key); } }
        public int MaxX { get { return sliceXContours.Max(s => s.Key); } }

        public int MinY { get { return sliceYContours.Min(s => s.Key); } }
        public int MaxY { get { return sliceYContours.Max(s => s.Key); } }

        public int MinZ { get { return sliceZContours.Min(s => s.Key); } }
        public int MaxZ { get { return sliceZContours.Max(s => s.Key); } }


        public OpenCvSharp.Point[][] GetContoursOnSliceZ(int zIdx)
        {
            if (sliceZContours.ContainsKey(zIdx))
            {
                return new OpenCvSharp.Point[][] { sliceZContours[zIdx].ToArray() };
            }
            else
            {
                return new OpenCvSharp.Point[0][];
            }
        }

        public OpenCvSharp.Point[][] GetContoursOnSliceY(int yIdx)
        {
            if (sliceYContours.ContainsKey(yIdx))
            {
                return new OpenCvSharp.Point[][] { sliceYContours[yIdx].ToArray() };
            }
            else
            {
                return new OpenCvSharp.Point[0][];
            }
        }

        public OpenCvSharp.Point[][] GetContoursOnSliceX(int xIdx)
        {
            if (sliceXContours.ContainsKey(xIdx))
            {
                return new OpenCvSharp.Point[][] { sliceXContours[xIdx].ToArray() };
            }
            else
            {
                return new OpenCvSharp.Point[0][];
            }
        }

        private void ConstructStructureSlicesOnImage(Matrix im)
        {
            foreach (var sliceContour in _contours)
            {
                var zOffset = sliceContour.Z - im.Origin.Z;
                var contourPoints = sliceContour.ContourPoints;

                foreach (var pt in contourPoints)
                {
                    var xOffset = pt.X - im.Origin.X;
                    var yOffset = pt.Y - im.Origin.Y;

                    var dOffset = Transform.TransformOffset(new Vector3(xOffset, yOffset, zOffset), im.ImageOrientation);

                    var xIdx = (int)(Math.Round(dOffset.X / im.XRes));
                    var yIdx = (int)(Math.Round(dOffset.Y / im.YRes));
                    var zIdx = (int)(Math.Round(dOffset.Z / im.ZRes));

                    var point = new OpenCvSharp.Point(xIdx, yIdx);

                    //Z Contours
                    if (sliceZContours.ContainsKey(zIdx))
                    {
                        sliceZContours[zIdx].Add(point);
                    }
                    else
                    {
                        sliceZContours.Add(zIdx, new List<OpenCvSharp.Point>() { point });
                    }

                    //Y Contours
                    if (sliceYContours.ContainsKey(point.Y))
                    {
                        sliceYContours[point.Y].Add(new OpenCvSharp.Point(point.X, zIdx));
                    }
                    else
                    {
                        sliceYContours.Add(point.Y, new List<OpenCvSharp.Point>() { new OpenCvSharp.Point(point.X, zIdx) });
                    }

                    //X Contours
                    if (sliceXContours.ContainsKey(point.X))
                    {
                        sliceXContours[point.X].Add(new OpenCvSharp.Point(point.Y, zIdx));
                    }
                    else
                    {
                        sliceXContours.Add(point.X, new List<OpenCvSharp.Point>() { new OpenCvSharp.Point(point.Y, zIdx) });
                    }
                }
            }
        }

        /// <summary>
        /// Draws the contour in patient coordinates on the slice voxels (which are voxel coordinates)
        /// </summary>
        /// <param name="structure">the structure to draw</param>
        /// <param name="z">the z slice to draw the structure</param>
        /// <returns></returns>
        public Mat DrawContourOnSlice(int z, StructureLook look)
        {
            if (!sliceZContours.Any()) { ConstructStructureSlicesOnImage(_im); }

            var slice = _im.GetZPlaneBySlice(z);
            if (slice.Type() == MatType.CV_32FC1)
            {
                //Dose file
                double min, max;
                slice.MinMaxIdx(out min, out max);
                var wL = slice.WindowAndLevel(max * 0.95, max * 0.10);
                slice.Dispose();
                var color = wL.CvtColor(ColorConversionCodes.GRAY2RGB);
                wL.Dispose();
                slice = color;
            }

            var contours = GetContoursOnSliceZ(z);
            for (int i = 0; i < contours.Length; i++)
            {
                slice.DrawContours(contours, i, look.OutlineColor, look.OutlineThickness);
            }

            return slice;
        }

        /// <summary>
        /// Draws the contour in patient coordinates on the slice voxels (which are voxel coordinates)
        /// </summary>
        /// <param name="structure">the structure to draw</param>
        /// <param name="z">the z slice to draw the structure</param>
        /// <returns></returns>
        public Mat DrawContourOnSlice(Mat imSlice, int z, StructureLook look)
        {
            var contours = GetContoursOnSliceZ(z);
            for (int i = 0; i < contours.Length; i++)
            {
                imSlice.DrawContours(contours, i, look.OutlineColor, look.OutlineThickness);
            }

            return imSlice;
        }
    }
}
