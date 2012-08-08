using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Image
{
    public struct Pixel
    {
        private double x;
        private double y;

        private double value;

        public double X
        {
            set { x = value; }
            get { return x; }
        }

        public double Y
        {
            set { y = value; }
            get { return y; }
        }

        public double Value
        {
            set { this.value = value; }
            get { return value; }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


