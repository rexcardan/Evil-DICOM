using OpenCvSharp;
using EvilDICOM.CV.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image
{
    public class MatrixHistogram : IDisposable
    {
        Mat _histogram = new Mat();
        private int _nBins;
        private float _binWidth;
        private Rangef _range;
        private double _min;
        private double _max;

        public MatrixHistogram(Matrix m, int nBins)
        {
            m._mat.MinMaxIdx(out _min, out _max);
            _nBins = nBins;
            _binWidth = (float)((_max+1 - _min) / _nBins);
            _range = new Rangef((float)_min, (float)_max+1);
        }

        public void AddSlice(Mat slice)
        {
            Cv2.CalcHist(new Mat[] { slice }, new int[] { 0 }, null, _histogram, 1,
                        new int[] { _nBins }, new Rangef[] { _range }, true, true);
        }

        /// <summary>
        /// Counts voxels between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Count(float min, float max)
        {
            var nVoxels = 0;
            if (_histogram.Rows != 0)
            {
                for (int i = 0; i < _nBins; i++)
                {
                    var val = _min + (i * _binWidth);
                    if (val >= min && val <= max)
                    {
                        var number = _histogram.At<float>(i);
                        if (number > 0)
                        {
                            nVoxels += (int)number;
                        }
                    }
                }
            }
            return nVoxels;
        }

        public void Dispose()
        {
            _histogram.Dispose();
        }
    }
}
