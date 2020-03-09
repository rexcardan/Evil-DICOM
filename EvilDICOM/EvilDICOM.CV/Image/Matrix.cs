using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Drawing;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Geometry;
using EvilDICOM.CV.Geometry.Isosurfaces;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.Image.Ops;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EvilDICOM.CV.Image
{
    public class Matrix : IDisposable
    {
        internal Mat _mat;

        public double XRes { get; set; }
        public double YRes { get; set; }
        public double ZRes { get; set; }

        public Mat GetZPlaneBySlice(int z)
        {
            var rangeX = OpenCvSharp.Range.All;
            var rangeY = OpenCvSharp.Range.All;
            var rangeZ = new OpenCvSharp.Range(z, z + 1);

            var clone = new Mat();
            using (var slice = new Mat(_mat, rangeZ, rangeY, rangeX).Clone())
            {
                using (var slice2D = slice.Reshape(_mat.Channels(), new int[] { DimensionY, DimensionX }))
                {
                    clone = slice2D.Clone();
                }
            }
            return clone;
        }

        public void SetZPlaneBySlice(Mat insert, int z)
        {
            var rangeX = OpenCvSharp.Range.All;
            var rangeY = OpenCvSharp.Range.All;
            var rangeZ = new OpenCvSharp.Range(z, z + 1);

            using (var slice = new Mat(_mat, rangeZ, rangeY, rangeX))
            {
                using (var slice2D = slice.Reshape(_mat.Channels(), new int[] { DimensionY, DimensionX }))
                {
                    insert.CopyTo(slice2D);
                }
            }
        }

        public double CalculateZeroBoundaryVolumeCC()
        {
            //double vol = 0;
            var contours = this.Find2DIsodoseLines(new RT.IsodoseLevel() { Value = 0 }).FirstOrDefault().SliceContours;
            //for (int z = 0; z < DimensionZ; z++)
            //{
            //    var zPos = ImageToPatientTx(new Vector3(0, 0, z)).Z;
            //    var slice = FloatMat.WindowLevelColor(GetZPlaneBySlice(z));
            //    foreach(var contour in contours.Where(c=>c.Z == zPos))
            //    {
            //        contour.DrawOnSlice(z, PatientTransformMatrix, slice);
            //    }
            //    Cv2.ImShow("Color", slice);
            //    Cv2.WaitKey();
            //}
            //var voxelLocations = this.GetImageGridPointsPatientCoordinates();
            //for (int i = 0; i < contours.Count; i++)
            //{
            //    var contour = contours[i];
            //    contour.ContourPoints = contour.ContourPoints.Select(c =>
            //    {
            //        var index = IndexHelper.LatticeXYToIndex((int)c.X, (int)c.Y, DimensionX);
            //        return voxelLocations[index];
            //    }).ToList();
            //}
            return VolumeCalculator.CalculateVolume(contours);
            //for (int z = 0; z < DimensionZ; z++)
            //{
            //    using (var slice = GetZPlaneBySlice(z))
            //    {
            //        var iso = slice.Find2DIsodoseLine(new RT.IsodoseLevel() { Value = 0 });
            //    }
            //}
            //return vol;
            var histogram = this.CalcHistogram(5000);
            var negative = histogram.Count(float.MinValue, -0.001f);
            histogram.Dispose();
            return negative * this.ZRes * this.YRes * this.XRes / 1000;
        }

        public Matrix ExcludeOutsideStructure(StructureMeta str)
        {
            var mat = EmptyClone();

            for (int z = 0; z < DimensionZ; z++)
            {
                var zPos = ImageToPatientTx(new Vector3(0, 0, z)).Z;
                var contour = str.SliceContours.FirstOrDefault(sc => sc.Z == zPos);
                var slice = GetZPlaneBySlice(z);

                var masked = new Mat(slice.Rows, slice.Cols, MatType.CV_32FC1, new Scalar(0));

                using (var mask = new Mat(slice.Rows, slice.Cols, MatType.CV_8UC1, new Scalar(0)).EmptyClone())
                {
                    if (contour != null)
                    {
                        contour.MaskImageFast(mask, PatientTransformMatrix, 255);
                        slice.CopyTo(masked, mask);
                    }
                    mat.SetZPlaneBySlice(masked, z);
                }
            }
            return mat;
        }

        public Matrix MaskToStructure(StructureMeta str)
        {
            Matrix mat = EmptyClone();

            for (int z = 0; z < DimensionZ; z++)
            {
                var zPos = ImageToPatientTx(new Vector3(0, 0, z)).Z;
                var contour = str.SliceContours.FirstOrDefault(sc => sc.Z == zPos);
                using (var slice = new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, new Scalar(1)))
                {
                    using (var masked = new Mat(mat.DimensionY, mat.DimensionX, MatType.CV_32FC1, new Scalar(-1)))
                    {
                        using (var mask = new Mat(slice.Rows, slice.Cols, MatType.CV_8UC1, new Scalar(0)))
                        {
                            if (contour != null)
                            {
                                contour.MaskImageFast(mask, PatientTransformMatrix, 255);
                                slice.CopyTo(masked, mask);
                            }
                            mat.SetZPlaneBySlice(masked, z);
                        }
                    }
                }


            }
            return mat;
        }

        public Mat GetXPlaneBySlice(int x)
        {
            var rangeX = new OpenCvSharp.Range(x, x + 1);
            var rangeY = OpenCvSharp.Range.All;
            var rangeZ = OpenCvSharp.Range.All;

            using (var slice = new Mat(_mat, rangeZ, rangeY, rangeX).Clone())
            {
                using (var slice2D = slice.Reshape(_mat.Channels(), new int[] { DimensionZ, DimensionY }))
                {
                    return slice2D.Clone();
                }
            }
        }

        public Mat GetYPlaneBySlice(int y)
        {
            var rangeX = OpenCvSharp.Range.All;
            var rangeY = new OpenCvSharp.Range(y, y + 1);
            var rangeZ = OpenCvSharp.Range.All;

            using (var slice = new Mat(_mat, rangeZ, rangeY, rangeX).Clone())
            {
                using (var slice2D = slice.Reshape(_mat.Channels(), new int[] { DimensionZ, DimensionX }))
                {
                    return slice2D.Clone();
                }
            }
        }

        public Mat GetZPlane(double z)
        {
            //Check if in bounds
            if (z >= Origin.Z && z <= ZMax)
            {
                var zSteps = (int)((z - Origin.Z) * ImageOrientation.zDir.Z / ZRes);
                var zSteps_Pls1 = (int)((z - Origin.Z + ZRes) * ImageOrientation.zDir.Z / ZRes);

                var lowZ = Origin.Z + zSteps * ZRes;
                var highZ = Origin.Z + (zSteps_Pls1) * ZRes;
                var interpZ = (z - Origin.Z) / ZRes % 1 != 0;

                if (!interpZ) { return GetZPlaneBySlice((int)(zSteps)); }
                else
                {
                    //Otherwise interpolate
                    var zd = interpZ ? (z - lowZ) / (highZ - lowZ) : 0;
                    var lowPlane = GetZPlaneBySlice((int)(zSteps));
                    var highPlane = GetZPlaneBySlice((int)(zSteps_Pls1));
                    Mat interpolated = lowPlane.EmptyClone();
                    Cv2.AddWeighted(lowPlane, 1 - zd, highPlane, zd, 0, interpolated);
                    return interpolated;
                }
            }
            else
            {
                //Return empty
                return new Mat(DimensionY, DimensionX, _mat.Type(), new float[DimensionX * DimensionY]);
            }
        }

        public Mat GetXPlane(double xPositionMM)
        {
            //XDIR=> Y, YDIR => Z
            //Scale
            var xScale = ZRes > YRes ? (YRes / ZRes) : 1.0;
            var yScale = YRes > ZRes ? (ZRes / YRes) : 1.0;

            //Check if in bounds
            if (xPositionMM >= Origin.X && xPositionMM <= XMax)
            {
                var ordered = Enumerable.Range(1, DimensionX).Select(s =>
                {
                    return new { Key = s, Value = Origin.X + XRes * s };
                });
                var firstLessThan = ordered.LastOrDefault(o => o.Value <= xPositionMM);
                //If close enough, send this plane
                if (Math.Abs(firstLessThan.Value - xPositionMM) < 0.001) { return GetXPlaneBySlice(firstLessThan.Key - 1).Resize(Size.Zero, xScale, yScale); }
                var firstMoreThan = ordered.FirstOrDefault(o => o.Value >= xPositionMM);
                if (Math.Abs(firstMoreThan.Value - xPositionMM) < 0.001) { return GetXPlaneBySlice(firstMoreThan.Key - 1).Resize(Size.Zero, xScale, yScale); }

                //Otherwise interpolate
                var zd = (xPositionMM - firstLessThan.Value) / (firstMoreThan.Value - firstLessThan.Value);
                var lowPlane = GetXPlaneBySlice(firstLessThan.Key - 1);
                var highPlane = GetXPlaneBySlice(firstMoreThan.Key - 1);
                Mat interpolated = lowPlane.EmptyClone();
                Cv2.AddWeighted(lowPlane, zd, highPlane, 1 - zd, 0, interpolated);

                interpolated = interpolated.Resize(Size.Zero, xScale, yScale);
                return interpolated;
            }
            else
            {
                //Return empty
                return new Mat(DimensionZ, DimensionY, _mat.Type()).Resize(Size.Zero, xScale, yScale);
            }
        }

        public Mat GetYPlane(double yPositionMM)
        {
            //Scale
            var xScale = ZRes > XRes ? (XRes / ZRes) : 1.0;
            var yScale = XRes > ZRes ? (ZRes / XRes) : 1.0;

            //Check if in bounds
            if (yPositionMM >= Origin.Y && yPositionMM <= YMax)
            {
                var ordered = Enumerable.Range(1, DimensionY).Select(s =>
                {
                    return new { Key = s, Value = Origin.Y + YRes * s };
                });
                var firstLessThan = ordered.LastOrDefault(o => o.Value <= yPositionMM);
                //If close enough, send this plane
                if (Math.Abs(firstLessThan.Value - yPositionMM) < 0.001) { return GetYPlaneBySlice(firstLessThan.Key - 1).Resize(Size.Zero, xScale, yScale); }
                var firstMoreThan = ordered.FirstOrDefault(o => o.Value >= yPositionMM);
                if (Math.Abs(firstMoreThan.Value - yPositionMM) < 0.001) { return GetYPlaneBySlice(firstMoreThan.Key - 1).Resize(Size.Zero, xScale, yScale); }

                //Otherwise interpolate
                var zd = (yPositionMM - firstLessThan.Value) / (firstMoreThan.Value - firstLessThan.Value);
                var lowPlane = GetYPlaneBySlice(firstLessThan.Key - 1);
                var highPlane = GetYPlaneBySlice(firstMoreThan.Key - 1);
                Mat interpolated = lowPlane.EmptyClone();
                Cv2.AddWeighted(lowPlane, zd, highPlane, 1 - zd, 0, interpolated);
                interpolated = interpolated.Resize(Size.Zero, xScale, yScale);
                return interpolated;
            }
            else
            {
                //Return empty
                return new Mat(DimensionZ, DimensionY, _mat.Type()).Resize(Size.Zero, xScale, yScale);
            }
        }

        public int BytesAllocated { get; set; }

        public (Vector3 xDir, Vector3 yDir, Vector3 zDir) ImageOrientation { get; set; }

        internal void CalculateBounds()
        {
            var xbound = Origin.X + (XRes * (DimensionX - 1)) * ImageOrientation.xDir.X +
                 (YRes * (DimensionY - 1)) * ImageOrientation.yDir.X +
                 (ZRes * (DimensionZ - 1)) * ImageOrientation.zDir.X;

            var ybound = Origin.Y + (XRes * (DimensionX - 1)) * ImageOrientation.xDir.Y +
                (YRes * (DimensionY - 1)) * ImageOrientation.yDir.Y +
                (ZRes * (DimensionZ - 1)) * ImageOrientation.zDir.Y;

            var zbound = Origin.Z + (XRes * (DimensionX - 1)) * ImageOrientation.xDir.Z +
                (YRes * (DimensionY - 1)) * ImageOrientation.yDir.Z +
                (ZRes * (DimensionZ - 1)) * ImageOrientation.zDir.Z;

            XMax = Math.Max(xbound, Origin.X);
            XMin = Math.Min(xbound, Origin.X);
            YMax = Math.Max(ybound, Origin.Y);
            YMin = Math.Min(ybound, Origin.Y);
            ZMax = Math.Max(zbound, Origin.Z);
            ZMin = Math.Min(zbound, Origin.Z);
        }

        public double XMax { get; private set; }
        public double YMax { get; private set; }
        public double ZMax { get; private set; }
        public double XMin { get; private set; }
        public double YMin { get; private set; }
        public double ZMin { get; private set; }


        public int DimensionX { get; set; }

        public int DimensionY { get; set; }

        public int DimensionZ { get; set; }

        public Vector3 Origin { get; set; }

        public Mat PatientTransformMatrix { get; private set; }

        public bool IsInBounds(Vector3 pt)
        {
            return pt.X >= XMin && pt.X <= XMax && pt.Y >= YMin && pt.Y <= YMax && pt.Z >= ZMin && pt.Z < ZMax;
        }

        public float GetPointValue(double x, double y, double z)
        {
            return GetPointValue(new Vector3(x, y, z));
        }

        public float GetValueByIndex(int xIndex, int yIndex, int zIndex)
        {
            return _mat.At<float>(zIndex, yIndex, xIndex);
        }

        public void CropMatrixToStructure(StructureMeta contour, double marginMM)
        {
            Cropper.CropMatrixToStructure(this, contour, marginMM);
        }

        //TODO
        public Matrix ResampleMatrixToStructure(StructureMeta contour)
        {
            var minZ = contour.SliceContours.Min(sc => sc.Z);
            var maxZ = contour.SliceContours.Max(sc => sc.Z);
            var dz = contour.GetContourSliceThickness();
            var nZ = (int)Math.Floor((maxZ - minZ) / dz) + 1;

            var mat = new Matrix();
            mat.DimensionZ = nZ;
            mat.DimensionX = this.DimensionX;
            mat.DimensionY = this.DimensionY;
            mat.ZRes = dz;
            mat.XRes = this.XRes;
            mat.YRes = this.YRes;
            mat.ImageOrientation = this.ImageOrientation;
            mat.Origin = new Vector3(this.Origin.X, this.Origin.Y, ImageOrientation.zDir.Z == 1 ? minZ : maxZ);
            var values = new float[DimensionX * DimensionY * DimensionZ];
            mat.CreateMatrix(values);
            for (int z = 0; z < mat.DimensionZ; z++)
            {
                var zPos = mat.ImageToPatientTx(new Vector3(0, 0, z)).Z;
                var slice = GetZPlane(zPos);
                mat.SetZPlaneBySlice(slice, z);
            }
            return mat;
        }

        public Matrix PadZ(int zPad)
        {
            var mat = new Matrix();
            mat.DimensionZ = this.DimensionZ + (2 * zPad);
            mat.DimensionX = this.DimensionX;
            mat.DimensionY = this.DimensionY;
            mat.ZRes = this.ZRes;
            mat.XRes = this.XRes;
            mat.YRes = this.YRes;
            mat.ImageOrientation = this.ImageOrientation;
            var zOffset = Transform.TransformOffset(new Vector3(0, 0, zPad * ZRes), ImageOrientation).Z;
            mat.Origin = new Vector3(this.Origin.X, this.Origin.Y, this.Origin.Z - zOffset);
            var values = new float[DimensionX * DimensionY * DimensionZ];
            mat.CreateMatrix(values);
            var empty = new Mat(DimensionY, DimensionX, MatType.CV_32FC1, new Scalar(0));
            for (int z = 0; z < zPad; z++)
            {
                mat.SetZPlaneBySlice(empty, z);
            }
            for (int z = zPad; z < mat.DimensionZ - zPad; z++)
            {
                var zPos = mat.ImageToPatientTx(new Vector3(0, 0, z)).Z;
                var slice = GetZPlane(zPos + zOffset);
                mat.SetZPlaneBySlice(slice, z);
            }
            for (int z = mat.DimensionZ - zPad; z < mat.DimensionZ; z++)
            {
                mat.SetZPlaneBySlice(empty, z);
            }
            return mat;
        }

        /// <summary>
        /// Retrieves the point value in the matrix. Inefficient but useful for single point queries
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public float GetPointValue(Vector3 pt)
        {
            if (!IsInBounds(pt)) { return float.NaN; }
            var plane = GetZPlane(pt.Z);
            var x = (pt.X - Origin.X) / XRes;
            var y = (pt.Y - Origin.Y) / YRes;
            var patch = plane.GetRectSubPix(new Size(1, 1), new Point2f((float)x, (float)y));
            return patch.At<float>(0, 0);
        }

        public Vector3 ImageToPatientTx(Vector3 pos)
        {
            var withScale = new Vector3(pos.X * XRes, pos.Y * YRes, pos.Z * ZRes);
            var txOffset = Transform.TransformOffset(withScale, ImageOrientation);
            var test2 = this.Origin + txOffset;
            return test2;
        }

        /// <summary>
        /// Constructs the underlying OpenCV matrix which holds the data for this wrapping class
        /// </summary>
        internal virtual void CreateMatrix(float[] values)
        {
            CalculatePatientTransformMatrix();
            CalculateBounds();
            _mat = new Mat(new int[] { DimensionZ, DimensionY, DimensionX }, MatType.CV_32FC1, values).Clone();
        }

        public void CalculatePatientTransformMatrix()
        {
            var txValues = new double[4, 4]
            {
                {(ImageOrientation.xDir[0]*XRes),(ImageOrientation.yDir[0]*YRes),(ImageOrientation.zDir[0]*ZRes),Origin.X },
                {(ImageOrientation.xDir[1]*XRes),(ImageOrientation.yDir[1]*YRes),(ImageOrientation.zDir[1]*ZRes),Origin.Y },
                {(ImageOrientation.xDir[2]*XRes),(ImageOrientation.yDir[2]*YRes),(ImageOrientation.zDir[2]*ZRes),Origin.Z },
                {0,0,0,1 }
            };

            PatientTransformMatrix = new Mat(4, 4, MatType.CV_64FC1, txValues);
        }


        /// <summary>
        /// Returns the 2D image voxel coordinates in the patient coordinate system. Useful
        /// for deciding if a pixel is inside of a contour.
        /// </summary>
        /// <returns>2D vector array corresponding to concatenated voxel rows</returns>
        public Point2f[] GetImageGridPointsPatientCoordinates()
        {
            var points = new List<Vec4f>();

            for (float y = 0; y < DimensionY; y++)
            {
                for (float x = 0; x < DimensionX; x++)
                {
                    points.Add(new Vec4f(x, y, 0, 1));
                }
            }
            var allPoints = new Vec4f[DimensionX * DimensionY];
            using (var pointMat = new Mat<float>(new[] { DimensionY, DimensionX }, points.ToArray()))
            {
                var txPointMat = pointMat.Transform(PatientTransformMatrix);
                txPointMat.GetArray(out allPoints);
            }
            return allPoints.Select(pt => new Point2f(pt.Item0, pt.Item1)).ToArray();
        }

        public double VolumeCC()
        {
            return DimensionX * DimensionY * DimensionZ * XRes / 10 * YRes / 10 * ZRes / 10;
        }

        public Mesh ConvertIsodoseLevelToMesh(double isodoseLevel)
        {
            var mesh = MachingCubes.Calculate(this, isodoseLevel);
            return mesh;
        }
        public MatrixHistogram CalcHistogram(int nBins)
        {
            var histogram = new MatrixHistogram(this, nBins);
            for (int z = 0; z < DimensionZ; z++)
            {
                using (var slice = GetZPlaneBySlice(z))
                {
                    histogram.AddSlice(slice);
                }
            }
            return histogram;
        }

        public void Resample(double xRes, double yRes, double zRes)
        {
            Resampler.Resample(this, xRes, yRes, zRes);
        }

        public (float Max, float Min) MinMaxVal()
        {
            float maxf = float.MinValue;
            float minf = float.MaxValue;
            for (int z = 0; z < DimensionZ; z++)
            {
                double min, max;
                var slice = GetZPlaneBySlice(z);
                slice.MinMaxIdx(out min, out max);
                if (max >= maxf) { maxf = (float)max; }
                if (min <= minf) { minf = (float)min; }
                slice.Dispose();
            }
            return (maxf, minf);
        }

        public Matrix EmptyClone()
        {
            var mat = new Matrix();
            mat.DimensionZ = this.DimensionZ;
            mat.DimensionX = this.DimensionX;
            mat.DimensionY = this.DimensionY;
            mat.ZRes = this.ZRes;
            mat.XRes = this.XRes;
            mat.YRes = this.YRes;
            mat.ImageOrientation = this.ImageOrientation;
            mat.Origin = new Vector3(this.Origin.X, this.Origin.Y, this.Origin.Z);
            var size = DimensionX * DimensionY * DimensionZ;
            var values = new float[size];
            mat.CreateMatrix(values);
            return mat;
        }

        public void Dispose()
        {
            _mat?.Dispose();
        }
    }
}
