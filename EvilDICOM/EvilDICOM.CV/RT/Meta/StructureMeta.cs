using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;
using EvilDICOM.CV.Drawing;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Geometry;
using EvilDICOM.CV.Geometry.Isosurfaces;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT;
using EvilDICOM.RT.Data.DVH;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT.Meta
{
    public class StructureMeta
    {
        public string StructureId { get; set; }
        public Scalar Color { get; set; }
        public DVHData DVHData { get; set; } = new DVHData();
        public double VolumeCC { get; set; } = double.NaN;
        public int ROINumber { get; set; }
        public string StructureName { get; set; }
        public string DICOMType { get; set; }

        public List<SliceContourMeta> SliceContours { get; set; } = new List<SliceContourMeta>();

        private double _volume = double.NaN;
        //public double CalculateVolumeCC()
        //{
        //    var sdf = CalculateSDFMatrix();

        //    //Don't recalulate if already calculated
        //    if (double.IsNaN(_volume)) { _volume = VolumeCalculator.CalculateVolume(SliceContours); }
        //    return _volume;
        //}

        public StructureLook Look { get; set; } = StructureLooks.Default;

        private Matrix CalculateModelMatrix(Func<IEnumerable<SliceContourMeta>, Matrix, Mat> contoursToSlice, Matrix valueMatrix)
        {
            var mat = valueMatrix.EmptyClone();

            for (int z = 0; z < mat.DimensionZ; z++)
            {
                var zPos = mat.Origin.Z + (Transform.TransformOffset(new Vector3(0, 0, z), (mat.ImageOrientation)).Z) * mat.ZRes;
                var zContours = SliceContours.Where(s => Math.Abs(s.Z - zPos) < 0.01);
                var slice = contoursToSlice(zContours, mat);
                mat.SetZPlaneBySlice(slice, z);
                slice.Dispose();
            }
            return mat;
        }

        /// <summary>
        /// Calculates a 1 mm^3 matrix describing the shape of the contour. All voxels contain signed distance function
        /// to closest contour point. Zero boundry is edge
        /// </summary>
        /// <param name="xyScale">the scale to expand the slice pixels (2 halves resolution)</param>
        /// <returns>a matrix describing shape of contour</returns>
        public Matrix CalculateSDFMatrix(Matrix valueMatrix)
        {
            Point2f[] sliceVoxelXYs = valueMatrix.GetImageGridPointsPatientCoordinates();

            var zeroSlice = new float[valueMatrix.DimensionX * valueMatrix.DimensionY];
            var ctrPoint = new Point2f(sliceVoxelXYs.Average(s => s.X), sliceVoxelXYs.Average(s => s.Y));
            Parallel.For(0, sliceVoxelXYs.Length, (i) =>
            {
                var pt = sliceVoxelXYs[i];
                //Positive inside
                var dist = Cv2.PointPolygonTest(new List<Point2f>() { ctrPoint }, pt, true);
                zeroSlice[i] = (float)dist;
            });

            var contoursToSlice = new Func<IEnumerable<SliceContourMeta>, Matrix, Mat>((zContours, mat) =>
            {
                Mat toReturn = Mat.FromPixelData(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, zeroSlice, 0);

                if (zContours.Any())
                {
                    var pixels = new float[mat.DimensionX * mat.DimensionY];
                    var allPts = zContours.SelectMany(c => c.ContourPoints).ToList();
                    foreach (var contour in zContours)
                    {
                        Parallel.For(0, sliceVoxelXYs.Length, (i) =>
                        {
                            var pt = sliceVoxelXYs[i];
                            //Positive inside
                            var dist = Cv2.PointPolygonTest(allPts, pt, true);
                            pixels[i] = (float)dist;
                        });
                    }
                    toReturn = Mat.FromPixelData(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, pixels, 0);
                }
                return toReturn;
            });

            return CalculateModelMatrix(contoursToSlice, valueMatrix);
        }

        /// <summary>
        /// Calculates a 1 mm^3 matrix describing the shape of the contour. Inside is 1, Outside is -1 and zero boundry is edge
        /// </summary>
        /// <param name="xyScale">the scale to expand the slice pixels (2 halves resolution)</param>
        /// <returns>a matrix describing shape of contour</returns>
        //public Matrix CalculateImageMatrix(double xyScale = 1)
        //{
        //    var contoursToSlice = new Func<IEnumerable<SliceContourMeta>, Matrix, Mat>((zContours, mat) =>
        //    {
        //        if (zContours.Any())
        //        {
        //            var slice = new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, new Scalar(-1));
        //            var allPts = zContours.SelectMany(c => c.ContourPoints).ToList();
        //            foreach (var contour in zContours)
        //            {
        //                var mask = new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_8UC1, new Scalar(0));
        //                contour.MaskImageFast(mask, mat.PatientTransformMatrix);
        //                var inside = new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, new Scalar(1));
        //                inside.CopyTo(slice, mask);
        //                inside.Dispose();
        //                mask.Dispose();
        //            }
        //            return slice;
        //        }
        //        else
        //        {
        //            return new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, new Scalar(-1));
        //        }
        //    });
        //    return CalculateModelMatrix(contoursToSlice, value);
        //}

        //public Mesh CalcMesh()
        //{
        //    var mat = CalculateSDFMatrix();
        //    var mesh = MachingCubes.Calculate(mat, 0);
        //    mat.Dispose();
        //    return mesh;
        //}

        public override string ToString()
        {
            return $"{StructureId}";
        }
    }
}