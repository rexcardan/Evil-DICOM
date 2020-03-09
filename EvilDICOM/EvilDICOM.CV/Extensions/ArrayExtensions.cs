using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    public static class ArrayExtensions
    {
        public static Point3f ToPoint3f(this List<double> array)
        {
            return new Point3f((float)array[0], (float)array[1], (float)array[2]);
        }
    }
}
