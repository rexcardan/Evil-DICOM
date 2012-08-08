using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Image
{
    public struct ScalingFunction
    {
        private double intercept;       
        private double slope;

        public ScalingFunction(double slope, double intercept)
        {
            this.intercept = intercept;
            this.slope = slope;
        }

        public double Intercept
        {
            get { return intercept; }
            set { intercept = value; }
        }
        public double Slope
        {
            get { return slope; }
            set { slope = value; }
        }

        public double RescaledValue(double value) {
            if (slope != 0)
            {
                return value * slope + intercept;
            }
            else
            {
                return value;
            }
        }

        public float RescaledValue(int value)
        {
            if (slope != 0)
            {
                return (float)value * (float)slope + (float)intercept;
            }
            else
            {
                return (float)value;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


