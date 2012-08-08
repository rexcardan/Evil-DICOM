using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Matrix
{
    public class KernalHelper
    {
        //2D KERNELS
        public static Matrix3D BOXBLUR_3X3 = new Matrix3D(new float[] { 0.111f, 0.111f, 0.111f, 0.111f, 0.111f, 0.111f, 0.111f, 0.111f, 0.111f }, 3, 3, 1);
        public static Matrix3D BOXBLUR_9X9 = populateArray(9, 9, 1, 1 / 81f);
        public static Matrix3D SOBELX_KERNEL_3X3 = new Matrix3D(new float[] { -1,-2f,-1f,0f,0f,0f,1f,2f,1f }, 3, 3, 1);

        //3D KERNELS
        public static Matrix3D SOBELX_KERNEL_3X3X3 = new Matrix3D(new float[] { -1, 0, 1, -3, 0, 3, -1, 0, 1, -3, 0, 3, -6, 0, 6, -3, 0, 3, -1, 0, 1, -3, 0, 3, -1, 0, 1 }, 3, 3, 3);
        public static Matrix3D GUASSIAN_3X3X3=new Matrix3D(new float[]{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},3,3,3);


        private static Matrix3D populateArray(int rows, int columns, int slices, float number){
            Matrix3D m = new Matrix3D(columns, rows, slices);
            for (int i = 0; i < m.Matrix.Length; i++) {
                m.Matrix[i] = number;           
            }
            return m;
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


