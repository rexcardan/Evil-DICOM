using OpenCvSharp;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Drawing;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    public static class MatrixExtensions
    {
        public static double[,] To2DDoubleArray(this Mat mat)
        {
            var data = new double[mat.Rows * mat.Cols];
            mat.GetArray(out data);
            var data2D = new double[mat.Cols, mat.Rows];
            Buffer.BlockCopy(data, 0, data2D, 0, data.Length * sizeof(double));
            return data2D;
        }

        public static Mat WindowAndLevel(this Mat mat, double level, double window)
        {
            var m = 255.0 / window;
            var x1 = level - window / 2.0;
            var b = m * x1;// y-intercept
            var x2 = level + window / 2.0;
            var x1y = x1 * m + b; // y at x1
            var x2y = x2 * m + b; // y at x2
            var greyScale = new Mat(mat.Size(), MatType.CV_8UC1);

            float[] data = new float[mat.Rows * mat.Cols];
            byte[] bytes = new byte[mat.Rows * mat.Cols];

            mat.GetArray(out data);
            var toUC1 = new Func<double, byte>((val) =>
            {
                return (byte)((int)(val * m + b - x1y));
            });

            for (int i = 0; i < data.Length; i++)
            {
                bytes[i] = data[i] < x1 ? (byte)0x00 : data[i] > x2 ? (byte)0xFF : toUC1(data[i]);
            }

            greyScale.SetArray(bytes);
            return greyScale;
        }

        public static Mat WindowAndLevel(this Mat mat, WindowLevelPreset preset)
        {
            return WindowAndLevel(mat, preset.Level, preset.Window);
        }

        public static Mat HeatMap(this Mat mat)
        {
            Mat heatMap = new Mat();
            Cv2.ApplyColorMap(mat, heatMap, ColormapTypes.Jet);
            return heatMap;
        }

        public static Mat Rotate90(this Mat mat)
        {
            return mat.Transpose().Flip(FlipMode.Y);


        }


        public static StructureMeta[] Find2DIsodoseLines(this Matrix matrix, params IsodoseLevel[] levels)
        {
            List<StructureMeta> metas = new List<StructureMeta>();
            foreach (var level in levels)
            {
                var meta = new StructureMeta()
                {
                    StructureId = level.Value.ToString(),
                    StructureName = level.Value.ToString()
                };

                meta.Color = level.Color;

                for (int z = 0; z < matrix.DimensionZ; z++)
                {
                    using (var mat = matrix.GetZPlaneBySlice(z))
                    {
                        var ctrs = mat.Find2DIsodoseLine(level);
                        if (ctrs.Any())
                        {
                            foreach (var contour in ctrs)
                            {
                                var allPoints = new Vec4f[contour.Length];
                                var points = contour.Select(c => new Vec4f(c.X, c.Y, z, 1));
                                using (var pointMat = new Mat<float>(new[] { contour.Length }, points.ToArray()))
                                {
                                    var txPointMat = pointMat.Transform(matrix.PatientTransformMatrix);
                                    txPointMat.GetArray(out allPoints);
                                }
                                var sliceContour = new SliceContourMeta();
                                var points3D = allPoints.Select(a => new Point3f(a.Item0, a.Item1, a.Item2)).ToList();
                                points3D.ForEach(sliceContour.AddPoint);
                                meta.SliceContours.Add(sliceContour);
                            }
                        }
                    }
                }
                metas.Add(meta);
            }
            return metas.ToArray();
        }

        public static Point[][] Find2DIsodoseLine(this Mat mat, IsodoseLevel level)
        {
            Point[][] ctrs;
            HierarchyIndex[] hi;
            var thresh = mat.Threshold(level.Value, 255, ThresholdTypes.Binary);
            thresh = thresh.WindowAndLevel(128, 256);
            thresh.FindContours(out ctrs, out hi, RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);
            return ctrs;
        }

        public static SliceContourMeta FindZeroLevelContours(this Mat mat, Func<Vector3, Vector3> imageToPatientFunc, int z)
        {
            var zPos = imageToPatientFunc(new Vector3(0, 0, z)).Z;
            var meta = new SliceContourMeta();
            var child = new SliceContourMeta();
            var level = new IsodoseLevel() { Value = 0, Color = new Scalar(255) };
            Point[][] ctrs;
            HierarchyIndex[] hi;
            var thresh = mat.Threshold(level.Value, 255, ThresholdTypes.Binary);
            thresh = thresh.WindowAndLevel(128, 256);
            thresh.FindContours(out ctrs, out hi, RetrievalModes.CComp, ContourApproximationModes.ApproxSimple);
            for (int i = 0; i < hi.Length; i++)
            {
                var contour = ctrs[i];
                if (hi[i].Parent == -1)
                {
                    contour.ToList().ForEach(c =>
                    {
                        var pos = imageToPatientFunc(new Vector3(c.X, c.Y, 1));
                        pos.Z = zPos;
                        meta.AddPoint(new Point3f((float)pos.X, (float)pos.Y, (float)pos.Z));
                    });
                }
                if (hi[i].Parent != -1)
                {
                    contour.ToList().ForEach(c =>
                    {
                        var pos = imageToPatientFunc(new Vector3(c.X, c.Y, 1));
                        pos.Z = zPos;
                        child.AddPoint(new Point3f((float)pos.X, (float)pos.Y, (float)pos.Z));
                    });
                }
            }
            if (child.ContourPoints.Any())
                meta.Children.Add(child);
            thresh.Dispose();
            return meta;
        }

        /// <summary>
        /// Finds contours on StructureMeta model (1 inside, -1 outsize, 0 boundry)
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static SliceContourMeta FindStructureContour(this Matrix model, double zPos)
        {
            var contourSlice = model.GetZPlane(zPos);
            var meta = new SliceContourMeta();
            var child = new SliceContourMeta();
            Point[][] ctrs;
            HierarchyIndex[] hi;
            var thresh = contourSlice.Threshold(0, 255, ThresholdTypes.Binary).WindowAndLevel(128, 255);
            //Cv2.ImShow("Threshold", thresh);
            //Cv2.WaitKey();
            thresh.FindContours(out ctrs, out hi, RetrievalModes.CComp, ContourApproximationModes.ApproxNone);
            for (int i = 0; i < hi.Length; i++)
            {
                var contour = ctrs[i];
                if (hi[i].Parent == -1)
                {
                    contour.ToList().ForEach(c =>
                    {
                        var pos = model.ImageToPatientTx(new Vector3(c.X, c.Y, 1));
                        pos.Z = zPos;
                        meta.AddPoint(new Point3f((float)pos.X, (float)pos.Y, (float)pos.Z));
                    });
                }
                if (hi[i].Parent != -1)
                {
                    contour.ToList().ForEach(c =>
                    {
                        var pos = model.ImageToPatientTx(new Vector3(c.X, c.Y, 1));
                        pos.Z = zPos;
                        child.AddPoint(new Point3f((float)pos.X, (float)pos.Y, (float)pos.Z));
                    });
                }
            }
            if (child.ContourPoints.Any())
                meta.Children.Add(child);
            contourSlice.Dispose();
            thresh.Dispose();
            return meta;
        }

        public static void ShowAllSlices(this Matrix m)
        {
            for (int z = 0; z < m.DimensionZ; z++)
            {
                using (var mat = m.GetZPlaneBySlice(z))
                {
                    var sliceText = mat.EmptyClone();
                    sliceText.PutText($"{z}", new Point(m.DimensionX / 2, m.DimensionY - 5), HersheyFonts.Italic, 0.5, new Scalar(1));
                    var combined = new Mat();
                    Cv2.AddWeighted(mat, 1, sliceText, 0.5, 0, combined);
                    FloatMat.Show(combined);
                    combined.Dispose();
                    mat.Dispose();
                    sliceText.Dispose();
                }
            }
        }

        public static void ShowSDFMatrix(this Matrix src)
        {
            var drawing = new Mat(src.DimensionY, src.DimensionX, MatType.CV_8UC3);

            for (int z = 0; z < src.DimensionZ; z++)
            {
                using (var sdf = src.GetZPlaneBySlice(z))
                {
                    double minVal, maxVal;
                    sdf.MinMaxIdx(out minVal, out maxVal);
                    for (int j = 0; j < src.DimensionY; j++)
                    {
                        for (int i = 0; i < src.DimensionX; i++)
                        {
                            if (sdf.At<float>(j, i) < 0)
                            { drawing.Set(j, i, new Vec3b((byte)(255 - (int)Math.Abs((sdf.At<float>(j, i)) * 255 / minVal)), 0, 0)); }
                            else if (sdf.At<float>(j, i) > 0)
                            { drawing.Set(j, i, new Vec3b(0, 0, (byte)(255 - (int)sdf.At<float>(j, i) * 255 / maxVal))); }
                            else
                            { drawing.Set(j, i, new Vec3b(255, 255, 255)); }
                        }
                    }

                    Cv2.ImShow("SDF", drawing);
                    Cv2.WaitKey();
                }
            }

        }

        public static void DrawAllContours(this Mat mat, Point[][] ctrs, StructureLook look)
        {
            for (int i = 0; i < ctrs.Length; i++)
            {
                mat.DrawContours(ctrs, i, look.OutlineColor, look.OutlineThickness);
            }
        }

        //public static Matrix<short> MaskMatrixToStructure(this Matrix<float> matrix, IEnumerable<SliceContourMeta> contours)
        //{
        //        var z = contours.FirstOrDefault().Z;
        //        var zDose = matrix.GetZPlane(z);

        //        //Mask contours as white/holes as black/and fills as white

        //        foreach (var contour in contours)
        //        {
        //            using (var mask = new Mat(zDose.Rows, zDose.Cols, MatType.CV_8UC1, new Scalar(0)))
        //            {
        //                //This method will mask and exclude holes and include fills
        //                contour.MaskImage(mask, dm.PatientTransformMatrix, 255);
        //                dvh.AddSliceToDVH(zDose, mask);
        //            }
        //        }
        //}
    }
}
