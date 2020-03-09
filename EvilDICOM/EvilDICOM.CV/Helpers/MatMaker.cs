using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Helpers
{
    public static class MatMaker
    {
        public static Mat Identity(int rows, int cols)
        {
            return Mat.Eye(rows, cols, MatType.CV_32FC1).ToMat();
        }
    }
}
