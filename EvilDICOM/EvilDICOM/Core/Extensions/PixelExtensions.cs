using EvilDICOM.Core.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Extensions
{
    public static class PixelExtensions
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
        public static int[,] GetSlice(this int[] pixels, int z, int dimX, int dimY)
        {
            return PixelSlicer.GetSlice(pixels, z, dimX, dimY);
        }

        /// <summary>
        /// Copies a 2D slice from a 3D pixel matrix
        /// </summary>
        /// <typeparam name="T">the type of the data</typeparam>
        /// <param name="pixels">a 1D array of pixel data</param>
        /// <param name="z">the z-slice index</param>
        /// <param name="dimX">the width of the image in the X direction</param>
        /// <param name="dimY">the height of the image in the Y direction</param>
        /// <returns>a 2D array of the slice pixel data</returns>
        public static long[,] GetSlice(this long[] pixels, int z, int dimX, int dimY)
        {
            return PixelSlicer.GetSlice(pixels, z, dimX, dimY);
        }
        /// <summary>
        /// Copies a 2D slice from a 3D pixel matrix
        /// </summary>
        /// <typeparam name="T">the type of the data</typeparam>
        /// <param name="pixels">a 1D array of pixel data</param>
        /// <param name="z">the z-slice index</param>
        /// <param name="dimX">the width of the image in the X direction</param>
        /// <param name="dimY">the height of the image in the Y direction</param>
        /// <returns>a 2D array of the slice pixel data</returns>
        public static short[,] GetSlice(this short[] pixels, int z, int dimX, int dimY)
        {
            return PixelSlicer.GetSlice(pixels, z, dimX, dimY);
        }
    }
}
