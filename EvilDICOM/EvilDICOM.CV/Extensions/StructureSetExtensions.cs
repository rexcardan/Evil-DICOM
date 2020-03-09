using OpenCvSharp;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    //public static class StructureSetExtensions
    //{
    //    public static double CalculateVolumeCC(this StructureSetMeta meta, StructureMeta str)
    //    {
    //        double areaz = 0;
    //        for (int z = 0; z < meta.Image.DimensionZ-1; z++)
    //        {
    //            var sliceA = str.GetContoursOnSliceZ(z);
    //            var sliceB = str.GetContoursOnSliceZ(z + 1);
    //            var sliceA_area = sliceA.Sum(s => Cv2.ContourArea(s)) * meta.Image.XRes / 10 * meta.Image.YRes / 10;
    //            var sliceB_area = sliceB.Sum(s => Cv2.ContourArea(s)) * meta.Image.XRes / 10 * meta.Image.YRes / 10;
    //            areaz += (sliceA_area + sliceB_area + Math.Sqrt(sliceA_area * sliceB_area)) * meta.Image.ZRes / 10;
    //        }
    //        var totalVolume = areaz / 3;
    //        return totalVolume;
    //    }
    //}
}
