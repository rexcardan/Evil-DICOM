using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Extensions
{
    public static class StructureExtensions
    {
        public static double GetContourSliceThickness(this StructureMeta meta)
        {
            var zs = meta.SliceContours.Select(sc => sc.Z).Distinct().OrderBy(z => z).ToList();
            return zs.Min();
        }
    }
}
