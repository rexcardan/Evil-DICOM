using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;
using System.Diagnostics;

namespace EvilDicom.Matrix
{
    public struct Matrix3D
    {
        float[] matrix;
        int rows;
        int columns;
        int slices;

        public Matrix3D(int columns, int rows, int slices)
        {
            this.rows = rows;
            this.columns = columns;
            this.slices = slices;
            matrix = new float[rows * columns * slices];
        }

        public Matrix3D(float[] matrix, int columns, int rows, int slices)
        {
            this.rows = rows;
            this.columns = columns;
            this.slices = slices;
            this.matrix = matrix;
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }
        public int Columns
        {
            get { return columns; }
            set { columns = value; }
        }
        public int Slices
        {
            get { return slices; }
            set { slices = value; }
        }

        public float[] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public int SliceSize
        {
            get { return Columns * Rows; }
        }

        public void setValue(int column, int row, int slice, int value)
        {
            int i = column + (row * Columns) + (slice * Rows * Columns);
            matrix[i] = value;

        }

        public float getValue(int column, int row, int slice)
        {

            int i = column + (row * Columns) + (slice * Rows * Columns);
            return matrix[i];
        }

        public void convolve2D_3X3(Matrix3D kernel)
        {
            if (kernel.Slices != 1 || kernel.Rows != kernel.Columns)
            {
                Console.WriteLine("Only square 2D kernels are allowed!");
                return;
            }
            if (kernel.Rows != 3)
            {
                Console.WriteLine("Kernel has to be 3 x 3 matrix!");
                return;
            }

            for (int s = 0; s < this.Slices; s++)
            {
                //Break off slice for convolution in 2D
                float[] oldSlice = new float[SliceSize];
                float[] newSlice = new float[SliceSize];
                Buffer.BlockCopy(matrix, s * sizeof(float) * this.SliceSize, oldSlice, 0, SliceSize * sizeof(float));

                unsafe
                {
                    fixed (float* sliceP = oldSlice)
                    {
                        fixed (float* kernelP = kernel.matrix)
                        {
                            fixed (float* newSliceP = newSlice)
                            {
                                //Start convolution
                                for (int k = 0; k < oldSlice.Length - 2 * Columns; k += 3)
                                {
                                    int cr = k / Columns + 1;
                                    for (int j = k; j < cr * Columns - 2; j++)
                                    {
                                        newSliceP[j + Columns + 1] = sliceP[j] * kernelP[0] +
                                            sliceP[j + 1] * kernelP[1] + sliceP[j + 2] * kernelP[2] +
                                            sliceP[j + Columns] * kernelP[3] + sliceP[j + Columns + 1] * kernelP[4] +
                                            sliceP[j + Columns + 2] * kernelP[5] + sliceP[j + Columns * 2] * kernelP[6] +
                                            sliceP[j + Columns * 2 + 1] * kernelP[7] + sliceP[j + Columns * 2 + 2] * kernelP[8];
                                        k++;
                                    }
                                    cr++;
                                }
                            }
                        }
                    }
                }

                //Copy slice back to matrix
                Buffer.BlockCopy(newSlice, 0, matrix, s * sizeof(float) * this.SliceSize, SliceSize * sizeof(float));
            }
        }

        public void convolve2D_GENERIC(Matrix3D kernel)
        {
            if (kernel.Slices != 1 || kernel.Rows != kernel.Columns)
            {
                Console.WriteLine("Only square 2D kernels are allowed!");
                return;
            }
            if (kernel.Rows % 2 == 0)
            {
                Console.WriteLine("Kernel has to have an odd number of rows and columns!");
                return;
            }

            for (int s = 0; s < this.Slices; s++)
            {
                //Break off slice for convolution in 2D
                float[] oldSlice = new float[SliceSize];
                float[] newSlice = new float[SliceSize];
                Stopwatch stop = new Stopwatch();
                stop.Start();
                Buffer.BlockCopy(matrix, s * sizeof(float) * this.SliceSize, oldSlice, 0, SliceSize * sizeof(float));

                unsafe
                {
                    fixed (float* sliceP = oldSlice)
                    {
                        fixed (float* kernelP = kernel.matrix)
                        {
                            fixed (float* newSliceP = newSlice)
                            {
                                int edgeStop = kernel.Columns - 1;
                                int kernelHalf = kernel.Columns / 2;

                                //Start convolution
                                for (int k = 0; k < oldSlice.Length - edgeStop * Columns; k += kernel.Columns)
                                {
                                    //Current row
                                    int cr = k / Columns + 1;
                                    for (int j = k; j < cr * Columns - edgeStop; j++)
                                    {
                                        for (int m = 0; m < kernel.Columns; m++)
                                        {
                                            for (int n = 0; n < kernel.Columns; n++)
                                            {
                                                newSliceP[j + kernelHalf * Columns + kernelHalf] += sliceP[j + m * Columns + n] * kernelP[n];
                                            }
                                        }
                                        k++;
                                    }
                                    cr++;
                                }
                            }
                        }
                    }
                }
                stop.Stop();

                //Copy slice back to matrix
                Buffer.BlockCopy(newSlice, 0, matrix, s * sizeof(float) * this.SliceSize, SliceSize * sizeof(float));
            }

        }


        public static Matrix3D operator -(Matrix3D m1, Matrix3D m2)
        {
            if (m1.matrix.Length != m2.matrix.Length)
            {
                Console.WriteLine("Matrices are different sizes. Subtraction not possible!");
                return m1;
            }
            Matrix3D newM = new Matrix3D(m1.Columns, m1.Rows, m1.Slices);
            unsafe
            {
                fixed (float* newMP = newM.matrix)
                {
                    fixed (float* m1P = m1.matrix)
                    {
                        fixed (float* m2P = m2.matrix)
                        {
                            for (int i = 0; i < newM.matrix.Length; i++)
                            {
                                newMP[i] = m1P[i] - m2P[i];
                            }
                        }
                    }
                }
            }
            return newM;
        }

        public static Matrix3D operator +(Matrix3D m1, Matrix3D m2)
        {
            if (m1.matrix.Length != m2.matrix.Length)
            {
                Console.WriteLine("Matrices are different sizes. Addition not possible!");
                return m1;
            }
            Matrix3D newM = new Matrix3D(m1.Columns, m1.Rows, m1.Slices);
            unsafe
            {
                fixed (float* newMP = newM.matrix)
                {
                    fixed (float* m1P = m1.matrix)
                    {
                        fixed (float* m2P = m2.matrix)
                        {
                            for (int i = 0; i < newM.matrix.Length; i++)
                            {
                                newMP[i] = m1P[i] + m2P[i];
                            }
                        }
                    }
                }
            }
            return newM;
        }

        /// <summary>
        /// This method calculates the new position in the matrix based on the current position and x, y and z shifts.
        /// </summary>
        /// <param name="currentI">The starting position in the matrix</param>
        /// <param name="dx">The unit shift in the x direction</param>
        /// <param name="dy">The unit shift int the y direction</param>
        /// <param name="dz">The unit shift in the z direction</param>
        /// <returns></returns>
        public int moveI(int currentI, int dx, int dy, int dz)
        {
            int z = currentI / (Rows * Columns);
            int y = (currentI - z * Columns * Rows) / Columns;
            int x = (currentI - z * Columns * Rows) - y * Columns;
            if ((x + dx < 0 || y + dy < 0) || z + dz < 0)
            {
                return 0;
            }
            return x + dx + (y + dy) * Columns + (z + dz) * Columns * Rows;
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


