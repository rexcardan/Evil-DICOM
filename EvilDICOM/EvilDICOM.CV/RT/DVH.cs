using OpenCvSharp;
using EvilDICOM.RT.Data.DVH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.RT
{
    public class DVH
    {
        Mat _histogram = new Mat();
        private int _nBins;
        private float _binWidth;
        private Rangef _range;
        private string _doseUnit;
        private double _voxelCC;

        public double Volume { get; internal set; }

        public DVH(double maxDose, double voxelCC, string doseUnit)
        {
            var topScale = High10((float)maxDose);
            _voxelCC = voxelCC;
            _nBins = (int)(topScale * 1000);
            _binWidth = topScale / _nBins;
            _range = new Rangef(0, topScale);
            _doseUnit = doseUnit;
        }

        public void AddSliceToDVH(Mat doseSlice, Mat mask)
        {
            Cv2.CalcHist(new Mat[] { doseSlice }, new int[] { 0 }, mask, _histogram, 1,
                        new int[] { _nBins }, new Rangef[] { _range }, true, true);
        }

        public DVHData GetDVHData()
        {
            var dvhData = new DVHData();

            if (_histogram.Rows != 0)
            {
                List<DVHPoint> points = new List<DVHPoint>();
                var nVoxels = 0;
                for (int i = 0; i < _nBins; i++)
                {
                    var number = _histogram.At<float>(i);
                    nVoxels += (int)number;
                    var volCC = number * _voxelCC;
                    points.Add(new DVHPoint(new Dose(i * _binWidth, _doseUnit),
                        new Volume(volCC, "cc")));
                }

                var dvhVolume = points.Sum(p => p.Volume.Value);
                var hasVolume = points.Where(p => p.Volume.Value != 0).ToList();

                var max = hasVolume.Any() ? hasVolume.Max(p => p.Dose.Value) : double.NaN;
                var mean = hasVolume.Any() ? hasVolume.Sum(p => p.Dose.Value * p.Volume.Value) / dvhVolume : double.NaN;
                var min = hasVolume.Any() ? hasVolume.Min(p => p.Dose.Value) : double.NaN;

                //Now that stats are known, calculate cumulative DVH
                var total = 1.0;
                var lastValue = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    total -= points[i].Volume.Value / dvhVolume;
                    points[i].Volume = new Volume(total * 100, "%");
                    if (total < 0.00001)
                    {
                        lastValue = i;
                        break;
                    }
                }

                points = points.Take(lastValue + 1).ToList();

                dvhData.Points = points.ToArray();
                dvhData.MaxDose = new Dose(max, "Gy");
                dvhData.MinDose = new Dose(min, "Gy");
                dvhData.MeanDose = new Dose(mean, "Gy");
            }
            return dvhData;
        }

        private static float High10(float maxDose)
        {
            return (float)((Math.Ceiling(maxDose / 10)) * 10);
        }
    }
}
