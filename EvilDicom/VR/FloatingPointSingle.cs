using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;

namespace EvilDicom
{
    namespace VR
    {
        public class FloatingPointSingle : AbstractBinaryVR
        {
            public FloatingPointSingle() { VR = "FS"; }

            public new Double Data
            {
                //The reverse array methods exist to counter the default little endian
                //that the bitconverter returns
                set { base.Data = ArrayHelper.ReverseArray(BitConverter.GetBytes(value)); }
                get { return BitConverter.ToDouble(ArrayHelper.ReverseArray(base.Data), 0); }
            }

            public override string[] DataAsStringArray()
            {
                string[] sData = new string[] { Data.ToString() };
                return sData;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


