using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Image
{
    public class WindowLevel
    {

        public WindowLevel(int window, int level)
        {
            this.Window = window;
            this.Level = level;
        }

        public WindowLevel(double window, double level)
        {
            this.Window = (float)window;
            this.Level = (float)level;
        }

        public float Window
        {
            get;
            set;
        }

        public float Level
        {
            get;
            set;
        }

        /// <summary>
        /// <summary>
        /// This method returns the inputted value on a 256 scale according to the current window and level
        /// </summary>
        /// <param name="origValue">the original pixel value</param>
        /// <returns>the pixel value on the 256 scale</returns>
        public byte GetValue(int origValue)
        {
            int newValue;
            if (origValue <= WindowLow) { newValue = 0; }
            else if (origValue >= WindowHigh) { newValue = 255; }
            else
            {
                newValue = Convert.ToInt32(Slope * origValue + YIntercept);
            }
            return (byte)newValue;
        }

        /// <summary>
        /// <summary>
        /// This method returns the inputted value on a 256 scale according to the current window and level
        /// </summary>
        /// <param name="origValue">the original pixel value</param>
        /// <returns>the pixel value on the 256 scale</returns>
        public byte GetValue(double origValue)
        {
            int newValue;
            if (origValue <= WindowLow) { newValue = 0; }
            else if (origValue >= WindowHigh) { newValue = 255; }
            else
            {
                newValue = Convert.ToInt32(Slope * origValue + YIntercept);
            }
            return (byte)newValue;
        }

        /// <summary>
        /// <summary>
        /// This method returns the inputted value on a 256 scale according to the current window and level
        /// </summary>
        /// <param name="origValue">the original pixel value</param>
        /// <returns>the pixel value on the 256 scale</returns>
        public byte GetValue(float origValue)
        {
            int newValue;
            if (origValue <= WindowLow) { newValue = 0; }
            else if (origValue >= WindowHigh) { newValue = 255; }
            else
            {
                newValue = Convert.ToInt32(Slope * origValue + YIntercept);
            }
            return (byte)newValue;
        }

        private float WindowLow
        {
            get { return Level - Window / 2; }
        }
        private float WindowHigh
        {
            get { return Level + Window / 2; }
        }
        private double Slope
        {
            get
            {
                double slope = 255 / Convert.ToDouble(Window);
                return slope;
            }
        }

        private int YIntercept
        {
            get { return Convert.ToInt32((-255 * WindowLow) / Convert.ToDouble(Window)); }
        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


