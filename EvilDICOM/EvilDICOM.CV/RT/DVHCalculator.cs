using OpenCvSharp;
using EvilDICOM.CV.Drawing.Renderers;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Geometry;
using EvilDICOM.CV.Geometry.Isosurfaces;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.RT.Meta;
using EvilDICOM.RT.Data;
using EvilDICOM.RT.Data.DVH;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging;
using EvilDICOM.Core.Logging;

namespace EvilDICOM.CV.RT
{
    public class DVHCalculator
    {
        static ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<DVHCalculator>();

        public static DVH CalculateDVH(StructureMeta sm, DoseMatrix dm)
        {
            var maxDose = dm.MaxDose;
            var voxelCC = dm.XRes / 10 * dm.YRes / 10 * dm.ZRes / 10;
            var doseUnit = dm.DoseUnit == DoseUnit.ABSOLUTE ? "Gy" : "%";
            var dvh = new DVH(maxDose, voxelCC, doseUnit);

            var zSlices = sm.SliceContours.GroupBy(sc => sc.Z).OrderBy(grp => grp.Key);
            foreach (var slice in zSlices)
            {
                var z = slice.Key;
                CalculateContoursDVH(slice, dm, dvh);
            }
            return dvh;
        }
        public static DVH CalculateSDF_DVH(StructureMeta sm, DoseMatrix dm)
        {
            var cropDM = dm.Clone();
            cropDM.CropMatrixToStructure(sm, 2);
           // cropDM.ShowAllSlices();
            var sdf = sm.CalculateSDFMatrix(cropDM);
            cropDM.Resample(cropDM.XRes / 3, cropDM.YRes / 3, cropDM.ZRes / 3);
            sdf.Resample(sdf.XRes / 3, sdf.YRes / 3, sdf.ZRes / 3);

            var maxDose = cropDM.MaxDose;
            var voxelCC = cropDM.XRes / 10 * cropDM.YRes / 10 * cropDM.ZRes / 10;
            var doseUnit = dm.DoseUnit == DoseUnit.ABSOLUTE ? "Gy" : "%";
            var dvh = new DVH(maxDose, voxelCC, doseUnit);

            var vol = 0.0;
            for (int z = 0; z < cropDM.DimensionZ; z++)
            {
                var zPos = cropDM.ImageToPatientTx(new Core.Helpers.Vector3(0, 0, z)).Z;
                var contours = new SliceContourMeta[] { sdf.FindStructureContour(zPos) };
                vol += contours.Sum(c => c.CalculateArea()) * cropDM.ZRes;
                CalculateContoursDVH(contours, cropDM, dvh);
            }
            dvh.Volume = vol;

            return dvh;
        }

        public static DVHPoint[] CalculateMarchingDVH(StructureMeta sm, DoseMatrix dm)
        {
            var maxDose = dm.MaxDose;

            var resample = dm.ResampleMatrixToStructure(sm);
            var onlyStructure = resample.ExcludeOutsideStructure(sm);
            onlyStructure.CropMatrixToStructure(sm, 10);

            //for (int z = 0; z < onlyStructure.DimensionZ; z++)
            //{

            //    var slice = onlyStructure.GetZPlaneBySlice(z);
            //    FloatMat.Show(slice);

            //}
            var points = new List<DVHPoint>();
            for (double i = 0; i < maxDose; i += 0.5)
            {
                var mesh = MachingCubes.Calculate(onlyStructure, i);
                if (i == 3)
                {
                    mesh.Export(@"F:\OneDrive\__RED_ION\Git\EvilDICOM.CV\DICOMCV.Tests\gtv3dd.obj");
                }
                points.Add(new DVHPoint(new Dose(i, "Gy"), new Volume(mesh.CalculateVolumeCC(), "cc")));
            }

            return points.ToArray();
        }

        private static void CalculateContoursDVH(IEnumerable<SliceContourMeta> contours, DoseMatrix dm, DVH dvh)
        {
            var z = contours.FirstOrDefault().Z;
            using (var zDose = dm.GetZPlane(z))
            {
                //Mask contours as white/holes as black/and fills as white

                foreach (var contour in contours)
                {
                    using (var mask = new Mat(zDose.Rows, zDose.Cols, MatType.CV_8UC1, new Scalar(0)))
                    {
                        //This method will mask and exclude holes and include fills
                        contour.MaskImageFast(mask, dm.PatientTransformMatrix, 255);
                        dvh.AddSliceToDVH(zDose, mask);
                    }
                }
            }
        }

        //private static void CalculateDVH(StructureMeta str, DoseMatrix scaled)
        //{
        //    var dvhData = new DVHData();
        //    if (scaled.DoseUnit == DoseUnit.RELATIVE)
        //    {
        //        scaled.ConvertUnits(DoseUnit.ABSOLUTE);
        //    }

        //    var topScale = High10((float)scaled.MaxDose);
        //    var nBins = (int)(topScale * 1000);
        //    var histogram = new Mat();

        //    double area = 0;
        //    for (int z = 0; z < scaled.DimensionZ; z++)
        //    {
        //        var slice = str.GetContoursOnSliceZ(z);
        //        if (slice.Any())
        //        {
        //            var dose = scaled.GetZPlaneBySlice(z);
        //            var mask = new Mat(dose.Rows, dose.Cols, MatType.CV_8UC1, new Scalar(0));
        //            Cv2.FillPoly(mask, slice, new Scalar(255),LineTypes.Link8);
        //            area += slice.Sum(s => Cv2.ContourArea(s));

        //            var range = new Rangef(0, topScale );
        //            Cv2.CalcHist(new Mat[] { dose }, new int[] { 0 }, mask, histogram, 1,
        //                new int[] { nBins }, new Rangef[] { range }, true, true);
        //        }
        //    }

        //    var totalVolume = area * scaled.XRes / 10 * scaled.YRes / 10 * scaled.ZRes / 10;

        //    if (histogram.Rows != 0)
        //    {
        //        List<DVHPoint> points = new List<DVHPoint>();

        //        for (int i = 0; i < nBins; i++)
        //        {
        //            var number = histogram.At<float>(i);
        //            var volCC = number * scaled.XRes / 10 * scaled.YRes / 10 * scaled.ZRes / 10;
        //            points.Add(new DVHPoint(new Dose(i * topScale / nBins, scaled.DoseUnit == DoseUnit.ABSOLUTE ? "Gy" : "%"),
        //                new Volume(volCC, "cc")));
        //        }

        //        var dvhVolume = points.Sum(p => p.Volume.Value);
        //        var hasVolume = points.Where(p => p.Volume.Value != 0).ToList();

        //        var max = hasVolume.Any()?hasVolume.Max(p => p.Dose.Value):double.NaN ;
        //        var mean = hasVolume.Any() ? hasVolume.Sum(p => p.Dose.Value * p.Volume.Value) / dvhVolume : double.NaN;
        //        var min = hasVolume.Any() ? hasVolume.Min(p => p.Dose.Value) : double.NaN;

        //        var total = 1.0;
        //        var lastValue = 0;
        //        for (int i = 0; i < points.Count; i++)
        //        {
        //            total -= points[i].Volume.Value / dvhVolume;
        //            points[i].Volume= new Volume(total*100, "%");
        //            if(total < 0.00001)
        //            {
        //                lastValue = i;
        //                break;
        //            }
        //        }

        //        points = points.Take(lastValue+1).ToList();

        //        str.DVHData.Points = points.ToArray();
        //        str.DVHData.MaxDose = new Dose(max, "Gy");
        //        str.DVHData.MinDose = new Dose(min, "Gy");
        //        str.DVHData.MeanDose = new Dose(mean, "Gy");
        //        str.VolumeCC = totalVolume;
        //    }

        //}

        private static float High10(float maxDose)
        {
            return (float)((Math.Ceiling(maxDose / 10)) * 10);
        }
    }
}
