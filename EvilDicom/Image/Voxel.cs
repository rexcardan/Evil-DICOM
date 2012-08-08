using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Image
{
    public struct Voxel
    {
        //X,Y,Z are in units of the grid or cube
        //they are stored in. They don't have a
        //physical meaning without some more calculation
        private int x;
        private int y;
        private int z;

        //Contains actual physical coordinates
        private Position position;

        //Values
        private int red;
        private int green;
        private int blue;
        private int alpha;
        private double value;


        public int X
        {
            set { x = value; }
            get { return x; }
        }

        public int Y
        {
            set { y = value; }
            get { return y; }
        }

        public int Z
        {
            get { return z; }
            set { z = value; }
        }

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        public double Value
        {
            set
            {
                this.value = value;
            }
            get { return value; }
        }
        public int Red
        {
            get { return red; }
            set { red = value; }
        }
        public int Green
        {
            get { return green; }
            set { green = value; }
        }
        public int Blue
        {
            get { return blue; }
            set { blue = value; }
        }
        public int Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


