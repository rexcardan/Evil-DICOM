using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Geometry
{
    public class Mesh
    {
        public List<Triangle> GetTriangles()
        {
            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Indices.Count; i+=3)
            {
                tris.Add(new Triangle()
                {
                    P1 = Vertices[i],
                    P2 = Vertices[i + 1],
                    P3 = Vertices[i + 2],
                });
            }
            return tris;
        }
        public double CalculateVolumeCC()
        {
            return GetTriangles().Sum(t => t.SignedVolume());
        }

        public List<Vector3> Vertices = new List<Vector3>();
        public List<int> Indices = new List<int>();

        public void Export(string filePath)
        {
            ObjFileWriter.Write(this, filePath);
        }
    }
}
