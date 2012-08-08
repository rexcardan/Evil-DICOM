using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.VR;
using EvilDicom.Helper;

namespace EvilDicom.Image
{
    public class ImageProperties
    {
        #region PROPERTIES

        public bool IsDose { get; set; }

        public double PixelHeight
        {
            get;
            set;
        }

        public double PixelWidth
        {
            get;
            set;
        }
        public int BitsAllocated
        {
            get;
            set;
        }
        public Constants.TransferSyntax TransferSyntax
        {
            get;
            set;
        }
        public int Columns
        {
            get;
            set;
        }
        public int Rows
        {
            get;
            set;
        }

        public int SamplesPerPixel
        {
            get;
            set;
        }
        public WindowLevel WindowAndLevel
        {
            get;
            set;
        }

        public double SliceThickness
        {
            get;
            set;
        }

        public ScalingFunction Function
        {
            get;
            set;
        }
        public Position ImagePosition
        {
            get;
            set;
        }

        public int NumberOfFrames
        {
            get;
            set;
        }

        public double[] OffsetVector
        {
            get;
            set;
        }

        public int ImageNumber
        {
            get;
            set;
        }

        public int Size
        {
            get { return Rows * Columns; }
        }

        #endregion
    }


}


//Copyright © 2012 Rex Cardan, Ph.D


