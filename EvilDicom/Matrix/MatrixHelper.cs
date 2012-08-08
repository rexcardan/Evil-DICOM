using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Matrix
{
    public class MatrixHelper
    {
        public static void padMatrix2D(ref Matrix3D m, int pad)
        {
            //Copy OldValues
            float[] oldMatrix = m.Matrix;
            int rows = m.Rows;
            int columns = m.Columns;

            //Reset Matrix
            m.Rows = m.Rows + 2 * pad;
            m.Columns = m.Columns + 2 * pad;
            m.Matrix = new float[m.Rows * m.Columns * m.Slices];
            //Start of padded image
            int start = pad * m.Columns + pad;

            //Set Voxels of Pad to value
            unsafe
            {
                fixed (float* padP = m.Matrix)
                {
                    for (int i = 0; i < m.Matrix.Length; i++)
                    {
                        padP[i] = -1000;
                    }
                }
            }

            //Copy Image to Pad
            unsafe
            {
                fixed (float* padP = m.Matrix)
                {
                    fixed (float* matrixP = oldMatrix)
                    {
                        for (int s = 0; s < m.Slices; s++)
                        {
                            for (int rn = 0; rn < rows; rn++)
                            {
                                for (int c = 0; c < columns; c++)
                                {
                                    //Since the matrix contains all slices have to target slice with s * (rows * columns)
                                    padP[start + rn * m.Columns + c] = matrixP[rn * columns + c + s * (rows * columns)];
                                }
                            }
                            start += m.SliceSize;
                        }
                    }
                }
            }
        }

        public static void remove2DPad(ref Matrix3D m, int pad)
        {
            //Copy OldValues
            float[] oldMatrix = m.Matrix;
            int rows = m.Rows;
            int columns = m.Columns;

            //Reset Matrix
            m.Rows = m.Rows - 2 * pad;
            m.Columns = m.Columns - 2 * pad;
            m.Matrix = new float[m.Rows * m.Columns * m.Slices];
            //Start of padded image
            int start = pad * columns + pad;


            //Copy Pad to Image
            unsafe
            {
                fixed (float* padP = oldMatrix)
                {
                    fixed (float* matrixP = m.Matrix)
                    {
                        for (int s = 0; s < m.Slices; s++)
                        {
                            for (int rn = 0; rn < m.Rows; rn++)
                            {
                                for (int c = 0; c < m.Columns; c++)
                                {
                                    //Since the matrix contains all slices have to target slice with s * (m.Rows * m.Columns)
                                    int indexM = rn * columns + c + s * (m.Rows * m.Columns);
                                    int indexP = start + rn * columns + c;
                                    matrixP[rn * m.Columns + c + s * (m.Rows * m.Columns)] = padP[start + rn * columns + c];
                                }
                            }
                            start += rows*columns;
                        }
                    }
                }
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


