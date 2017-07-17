using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Image
{
    /// <summary>
    /// Slices data into chunks desired
    /// </summary>
    public class PixelSlicer
    {
        /// <summary>
        /// Copies a 2D slice from a 3D pixel matrix
        /// </summary>
        /// <typeparam name="T">the type of the data</typeparam>
        /// <param name="pixels">a 1D array of pixel data</param>
        /// <param name="z">the z-slice index</param>
        /// <param name="dimX">the width of the image in the X direction</param>
        /// <param name="dimY">the height of the image in the Y direction</param>
        /// <returns>a 2D array of the slice pixel data</returns>
        public static T[,] GetSlice<T>(T[] pixels, int z, int dimX, int dimY)
        {
            T[,] slice = new T[dimX, dimY];
            var startIndex = IndexHelper.LatticeXYZToIndex(0, 0, z, dimX, dimY);
            for (int x = 0; x < dimX; x++)
            {
                for (int y = 0; y < dimY; y++)
                {
                    slice[x, y] = pixels[startIndex + IndexHelper.LatticeXYToIndex(x, y, dimX)];
                }
            }
            return slice;
        }
    }
}
