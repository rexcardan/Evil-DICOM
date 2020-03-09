using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Geometry
{
    public class ObjFileWriter
    {
        public static void Write(Mesh model, string outputPath)
        {
            var lines = new List<string>();

            // Write the header lines
            lines.Add("#");
            lines.Add("# OBJ file created EvilDICOM.CV");
            lines.Add("#");

            // Sequentially write the 3 vertices of the triangle, for each triangle
            for (int i = 0; i < model.Vertices.Count; i++)
            {
                Vector3 vertex = model.Vertices[i];
                string vertexString = "v " + vertex.X.ToString(CultureInfo.InvariantCulture) + " ";
                vertexString += vertex.Y.ToString(CultureInfo.InvariantCulture) + " " +
                                vertex.Z.ToString(CultureInfo.InvariantCulture);
                lines.Add(vertexString);
            }

            List<int> indices = model.Indices;
            for (int i = 0; i < indices.Count; i+=3)
            {
                string baseIndex0 = (indices[i]).ToString(CultureInfo.InvariantCulture);
                string baseIndex1 = (indices[i+1]).ToString(CultureInfo.InvariantCulture);
                string baseIndex2 = (indices[i +2]).ToString(CultureInfo.InvariantCulture);

                string faceString = "f " + baseIndex0 + "//" + baseIndex0 + " " + baseIndex1 + "//" + baseIndex1 + " " +
                                    baseIndex2 + "//" + baseIndex2;
                lines.Add(faceString);
            }
            File.WriteAllLines(outputPath, lines.ToArray());
        }
    }
}
