using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.Geometry.Isosurfaces
{
    public class MachingCubes
    {
        public static Mesh Calculate(Matrix mat, double isodoseLevel)
        {
            var dim = new int[] { mat.DimensionX, mat.DimensionY, mat.DimensionZ };
            var vals = new float[dim[0] * dim[1] * dim[2]];
            var voxDim = new float[] { (float)mat.XRes/10, (float)mat.YRes/10, (float)mat.ZRes/10};
            for (int z = 0; z < mat.DimensionZ; z++)
            {
                var slice = mat.GetZPlaneBySlice(z);
                var temp = new float[slice.Rows * slice.Cols];
                slice.GetArray(out temp);
                Array.Copy(temp, 0, vals, z * slice.Rows * slice.Cols, temp.Length);
                slice.Dispose();
            }
            return Calculate(vals, dim, dim[2], voxDim, isodoseLevel);
        }

        public static Mesh Calculate(float[] values, int[] volDim, int volZFull, float[] voxDim, double isoLevel, int offset = 0)
        {
            // Actual position along edge weighted according to function values.
            float[][] vertList = new float[12][];


            // Calculate maximal possible axis value (used in vertice normalization)
            float maxX = voxDim[0] * (volDim[0] - 1);
            float maxY = voxDim[1] * (volDim[1] - 1);
            float maxZ = voxDim[2] * (volZFull - 1);

            var mesh = new Mesh();

            // Volume iteration
            for (int z = 0; z < volDim[2] - 1; z++)
            {
                for (int y = 0; y < volDim[1] - 1; y++)
                {
                    for (int x = 0; x < volDim[0] - 1; x++)
                    {

                        // Indices pointing to cube vertices
                        //              pyz  ___________________  pxyz
                        //                  /|                 /|
                        //                 / |                / |
                        //                /  |               /  |
                        //          pz   /___|______________/pxz|
                        //              |    |              |   |
                        //              |    |              |   |
                        //              | py |______________|___| pxy
                        //              |   /               |   /
                        //              |  /                |  /
                        //              | /                 | /
                        //              |/__________________|/
                        //             p                     px

                        int p = x + (volDim[0] * y) + (volDim[0] * volDim[1] * (z + offset)),
                                px = p + 1,
                                py = p + volDim[0],
                                pxy = py + 1,
                                pz = p + volDim[0] * volDim[1],
                                pxz = px + volDim[0] * volDim[1],
                                pyz = py + volDim[0] * volDim[1],
                                pxyz = pxy + volDim[0] * volDim[1];

                        //							  X              Y                    Z
                        var position = new float[] { x * voxDim[0], y * voxDim[1], (z + offset) * voxDim[2] };

                        // Voxel intensities
                        double value0 = values[p],
                                value1 = values[px],
                                value2 = values[py],
                                value3 = values[pxy],
                                value4 = values[pz],
                                value5 = values[pxz],
                                value6 = values[pyz],
                                value7 = values[pxyz];

                        // Voxel is active if its intensity is above isolevel
                        int cubeindex = 0;
                        if (value0 > isoLevel) cubeindex |= 1;
                        if (value1 > isoLevel) cubeindex |= 2;
                        if (value2 > isoLevel) cubeindex |= 8;
                        if (value3 > isoLevel) cubeindex |= 4;
                        if (value4 > isoLevel) cubeindex |= 16;
                        if (value5 > isoLevel) cubeindex |= 32;
                        if (value6 > isoLevel) cubeindex |= 128;
                        if (value7 > isoLevel) cubeindex |= 64;

                        // Fetch the triggered edges
                        int bits = Tables.MC_EDGE_TABLE[cubeindex];

                        // If no edge is triggered... skip
                        if (bits == 0) continue;

                        // Interpolate the positions based od voxel intensities
                        float mu = 0.5f;

                        // bottom of the cube
                        if ((bits & 1) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value1 - value0));
                            vertList[0] = Lerp(position, new float[] { position[0] + voxDim[0], position[1], position[2] }, mu);
                        }
                        if ((bits & 2) != 0)
                        {
                            mu = (float)((isoLevel - value1) / (value3 - value1));
                            vertList[1] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        if ((bits & 4) != 0)
                        {
                            mu = (float)((isoLevel - value2) / (value3 - value2));
                            vertList[2] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        if ((bits & 8) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value2 - value0));
                            vertList[3] = Lerp(position, new float[] { position[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        // top of the cube
                        if ((bits & 16) != 0)
                        {
                            mu = (float)((isoLevel - value4) / (value5 - value4));
                            vertList[4] = Lerp(new float[] { position[0], position[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 32) != 0)
                        {
                            mu = (float)((isoLevel - value5) / (value7 - value5));
                            vertList[5] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 64) != 0)
                        {
                            mu = (float)((isoLevel - value6) / (value7 - value6));
                            vertList[6] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 128) != 0)
                        {
                            mu = (float)((isoLevel - value4) / (value6 - value4));
                            vertList[7] = Lerp(new float[] { position[0], position[1], position[2] + voxDim[2] }, new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        // vertical lines of the cube
                        if ((bits & 256) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value4 - value0));
                            vertList[8] = Lerp(position, new float[] { position[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 512) != 0)
                        {
                            mu = (float)((isoLevel - value1) / (value5 - value1));
                            vertList[9] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] }, new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 1024) != 0)
                        {
                            mu = (float)((isoLevel - value3) / (value7 - value3));
                            vertList[10] = Lerp(new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 2048) != 0)
                        {
                            mu = (float)((isoLevel - value2) / (value6 - value2));
                            vertList[11] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] }, new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }

                        // construct triangles -- get correct vertices from triTable.
                        int i = 0;
                        // "Re-purpose cubeindex into an offset into triTable."
                        cubeindex <<= 4;


                        while (Tables.MC_TRI_TABLE[cubeindex + i] != -1)
                        {
                            int index1 = Tables.MC_TRI_TABLE[cubeindex + i];
                            int index2 = Tables.MC_TRI_TABLE[cubeindex + i + 1];
                            int index3 = Tables.MC_TRI_TABLE[cubeindex + i + 2];

                            // Add triangles vertices normalized with the maximal possible value
                            var startIndex = mesh.Vertices.Count;
                            mesh.Vertices.Add(new Vector3(vertList[index3][0], vertList[index3][1] , vertList[index3][2]));
                            mesh.Vertices.Add(new Vector3(vertList[index2][0], vertList[index2][1] , vertList[index2][2] ));
                            mesh.Vertices.Add(new Vector3(vertList[index1][0] , vertList[index1][1] , vertList[index1][2]));
                            mesh.Indices.AddRange(new[] { startIndex + 1, startIndex + 2, startIndex + 3 });
                            i += 3;
                        }
                    }
                }
            }
            return mesh;
        }

        public static List<Triangle> Calculate(short[] values, int[] volDim, int volZFull, float[] voxDim, double isoLevel, int offset = 0)
        {
            List<Triangle> vertices = new List<Triangle>();
            // Actual position along edge weighted according to function values.
            float[][] vertList = new float[12][];


            // Calculate maximal possible axis value (used in vertice normalization)
            float maxX = voxDim[0] * (volDim[0] - 1);
            float maxY = voxDim[1] * (volDim[1] - 1);
            float maxZ = voxDim[2] * (volZFull - 1);

            // Volume iteration
            for (int z = 0; z < volDim[2] - 1; z++)
            {
                for (int y = 0; y < volDim[1] - 1; y++)
                {
                    for (int x = 0; x < volDim[0] - 1; x++)
                    {

                        // Indices pointing to cube vertices
                        //              pyz  ___________________  pxyz
                        //                  /|                 /|
                        //                 / |                / |
                        //                /  |               /  |
                        //          pz   /___|______________/pxz|
                        //              |    |              |   |
                        //              |    |              |   |
                        //              | py |______________|___| pxy
                        //              |   /               |   /
                        //              |  /                |  /
                        //              | /                 | /
                        //              |/__________________|/
                        //             p                     px

                        int p = x + (volDim[0] * y) + (volDim[0] * volDim[1] * (z + offset)),
                                px = p + 1,
                                py = p + volDim[0],
                                pxy = py + 1,
                                pz = p + volDim[0] * volDim[1],
                                pxz = px + volDim[0] * volDim[1],
                                pyz = py + volDim[0] * volDim[1],
                                pxyz = pxy + volDim[0] * volDim[1];

                        //							  X              Y                    Z
                        var position = new float[] { x * voxDim[0], y * voxDim[1], (z + offset) * voxDim[2] };

                        // Voxel intensities
                        double value0 = values[p],
                                value1 = values[px],
                                value2 = values[py],
                                value3 = values[pxy],
                                value4 = values[pz],
                                value5 = values[pxz],
                                value6 = values[pyz],
                                value7 = values[pxyz];

                        // Voxel is active if its intensity is above isolevel
                        int cubeindex = 0;
                        if (value0 > isoLevel) cubeindex |= 1;
                        if (value1 > isoLevel) cubeindex |= 2;
                        if (value2 > isoLevel) cubeindex |= 8;
                        if (value3 > isoLevel) cubeindex |= 4;
                        if (value4 > isoLevel) cubeindex |= 16;
                        if (value5 > isoLevel) cubeindex |= 32;
                        if (value6 > isoLevel) cubeindex |= 128;
                        if (value7 > isoLevel) cubeindex |= 64;

                        // Fetch the triggered edges
                        int bits = Tables.MC_EDGE_TABLE[cubeindex];

                        // If no edge is triggered... skip
                        if (bits == 0) continue;

                        // Interpolate the positions based od voxel intensities
                        float mu = 0.5f;

                        // bottom of the cube
                        if ((bits & 1) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value1 - value0));
                            vertList[0] = Lerp(position, new float[] { position[0] + voxDim[0], position[1], position[2] }, mu);
                        }
                        if ((bits & 2) != 0)
                        {
                            mu = (float)((isoLevel - value1) / (value3 - value1));
                            vertList[1] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        if ((bits & 4) != 0)
                        {
                            mu = (float)((isoLevel - value2) / (value3 - value2));
                            vertList[2] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        if ((bits & 8) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value2 - value0));
                            vertList[3] = Lerp(position, new float[] { position[0], position[1] + voxDim[1], position[2] }, mu);
                        }
                        // top of the cube
                        if ((bits & 16) != 0)
                        {
                            mu = (float)((isoLevel - value4) / (value5 - value4));
                            vertList[4] = Lerp(new float[] { position[0], position[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 32) != 0)
                        {
                            mu = (float)((isoLevel - value5) / (value7 - value5));
                            vertList[5] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 64) != 0)
                        {
                            mu = (float)((isoLevel - value6) / (value7 - value6));
                            vertList[6] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 128) != 0)
                        {
                            mu = (float)((isoLevel - value4) / (value6 - value4));
                            vertList[7] = Lerp(new float[] { position[0], position[1], position[2] + voxDim[2] }, new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        // vertical lines of the cube
                        if ((bits & 256) != 0)
                        {
                            mu = (float)((isoLevel - value0) / (value4 - value0));
                            vertList[8] = Lerp(position, new float[] { position[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 512) != 0)
                        {
                            mu = (float)((isoLevel - value1) / (value5 - value1));
                            vertList[9] = Lerp(new float[] { position[0] + voxDim[0], position[1], position[2] }, new float[] { position[0] + voxDim[0], position[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 1024) != 0)
                        {
                            mu = (float)((isoLevel - value3) / (value7 - value3));
                            vertList[10] = Lerp(new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] }, new float[] { position[0] + voxDim[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }
                        if ((bits & 2048) != 0)
                        {
                            mu = (float)((isoLevel - value2) / (value6 - value2));
                            vertList[11] = Lerp(new float[] { position[0], position[1] + voxDim[1], position[2] }, new float[] { position[0], position[1] + voxDim[1], position[2] + voxDim[2] }, mu);
                        }

                        // construct triangles -- get correct vertices from triTable.
                        int i = 0;
                        // "Re-purpose cubeindex into an offset into triTable."
                        cubeindex <<= 4;

                        while (Tables.MC_TRI_TABLE[cubeindex + i] != -1)
                        {
                            int index1 = Tables.MC_TRI_TABLE[cubeindex + i];
                            int index2 = Tables.MC_TRI_TABLE[cubeindex + i + 1];
                            int index3 = Tables.MC_TRI_TABLE[cubeindex + i + 2];

                            // Add triangles vertices normalized with the maximal possible value
                            var tri = new Triangle();
                            tri.P1 = new Vector3(vertList[index3][0], vertList[index3][1], vertList[index3][2]);
                            tri.P2 = new Vector3(vertList[index2][0], vertList[index2][1], vertList[index2][2]);
                            tri.P3 = new Vector3(vertList[index1][0], vertList[index1][1], vertList[index1][2]);
                            vertices.Add(tri);
                            i += 3;
                        }
                    }
                }
            }
            return vertices;
        }

        /// <summary>
        /// Linearly interpolates between two vectors
        /// </summary>
        /// <param name="vec1">vector 1</param>
        /// <param name="vec2">vector 2</param>
        /// <param name="alpha">interpolate parameter</param>
        /// <returns></returns>
        public static float[] Lerp(float[] vec1, float[] vec2, float alpha)
        {
            return new float[] { vec1[0] + (vec2[0] - vec1[0]) * alpha, vec1[1] + (vec2[1] - vec1[1]) * alpha, vec1[2] + (vec2[2] - vec1[2]) * alpha };
        }
    }
}
