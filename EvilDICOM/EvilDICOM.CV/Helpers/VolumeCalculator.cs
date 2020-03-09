using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.CV.RT.Meta;

namespace EvilDICOM.CV.Helpers
{
    public class VolumeCalculator
    {
        /// <summary>
        /// Calculates the volume in cubic centimeters of a structure using slice contours
        /// </summary>
        /// <param name="sliceContours">the contours for all slices of a structure</param>
        /// <returns>the volume in cubic centimeters</returns>
        public static double CalculateVolume(List<SliceContourMeta> sliceContours)
        {
            double volumeZ = 0;
            var anyChildren = sliceContours.Where(s => s.Children.Any()).Count();
            var slices = sliceContours
                .GroupBy(s => s.Z)
                .OrderBy(grp => grp.Key)
                .ToList();
          
            //Method 2 (https://doi.org/10.3171/2012.7.GKS121016)
            for (int i = 0; i < slices.Count - 1; i++)
            {
                var sliceA = slices[i];
                var sliceB = slices[i + 1];
                var sliceA_area = sliceA.Sum(s => s.CalculateArea());
                var sliceB_area = sliceB.Sum(s => s.CalculateArea());
                var thickness = Math.Abs(sliceB.Key - sliceA.Key);
                volumeZ += (sliceA_area + sliceB_area + Math.Sqrt(sliceA_area * sliceB_area)) * thickness;
            }
            return volumeZ / 3000;
        }

        public static double CalculateVolume(List<double> areas, double voxelVol)
        {
            var volumeZ = 0.0;
            //Method 2 (https://doi.org/10.3171/2012.7.GKS121016)
            for (int i = 0; i < areas.Count - 1; i++)
            {
                var sliceA_area = areas[i];
                var sliceB_area = areas[i + 1];
                volumeZ += (sliceA_area + sliceB_area + Math.Sqrt(sliceA_area * sliceB_area)) * voxelVol;
            }
            return volumeZ / 3000;
        }
    }
}
