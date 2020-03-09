using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Image
{
    public struct Bounds
    {
        public Bounds(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;
        }

        public int MinX { get; private set; }
        public int MaxX { get; private set; }

        public int MinY { get; private set; }
        public int MaxY { get; private set; }

        public int MinZ { get; private set; }
        public int MaxZ { get; private set; }
    }
}
