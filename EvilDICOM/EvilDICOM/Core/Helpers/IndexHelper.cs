using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Helpers
{
    /// <summary>
    /// Has helpful methods for converting from an index to x,y,z coordinates in a pixel lattice and visa versa
    /// </summary>
    public class IndexHelper
    {
        /// <summary>
        /// Converts a input 1D index of a pixel in a lattice to the X, Y coordinates of the pixel
        /// </summary>
        /// <param name="index">the 1D index of the pixel</param>
        /// <param name="dimX">the width of the lattice in the X direction</param>
        /// <returns>a value tuple of the X,Y,Z coordiantes </returns>
        public static (int x, int y) IndexToLatticeXY(int index, int dimX)
        {
            var y = index / dimX; 
            var x = index % dimX;
            return (x, y);
        }

        /// <summary>
        /// Converts X,Y coordinates in a 2D pixel lattice to a 1D index
        /// </summary>
        /// <param name="x">the x coordinate of the pixel</param>
        /// <param name="y">the y coordinate of the pixel</param>
        /// <param name="dimX">the width of the lattice in the X direction</param>
        /// <returns>the index of the pixel in the 2D lattife</returns>
        public static int LatticeXYToIndex(int x, int y, int dimX)
        {
            return x + y * dimX;
        }

        /// <summary>
        /// Converts a input 1D index of a pixel in a lattice to the X, Y, Z coordinates of the pixel
        /// </summary>
        /// <param name="index">the 1D index of the pixel</param>
        /// <param name="dimX">the width of the lattice in the X direction</param>
        /// <param name="dimY">the height of the lattice in the Y direction</param>
        /// <returns>a value tuple of the X,Y,Z coordiantes </returns>
        public static (int x, int y, int z) IndexToLatticeXYZ(int index, int dimX, int dimY)
        {
            var z = index / (dimX * dimY);
            var y = index % (dimX * dimY) / dimX;
            var x = index % (dimX * dimY) % dimX;
            return (x, y, z);
        }

        /// <summary>
        /// Converts X,Y,Z coordinates in a 3D pixel lattice to a 1D index
        /// </summary>
        /// <param name="x">the x coordinate of the pixel</param>
        /// <param name="y">the y coordinate of the pixel</param>
        /// <param name="z">the z coordinate of the pixel</param>
        /// <param name="dimX">the width of the lattice in the X direction</param>
        /// <param name="dimY">the height of the lattice in the Y direction</param>
        /// <returns>the index of the pixel in the 3D lattice</returns>
        public static int LatticeXYZToIndex(int x, int y, int z, int dimX, int dimY)
        {
            return x + y * dimX + z * dimX * dimY;
        }
    }
}
