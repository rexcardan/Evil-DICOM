using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;

namespace EvilDicom
{
    namespace VR
    {
        public class AttributeTag : AbstractBinaryVR
        {

            public AttributeTag() { VR = "AT"; }


            public new UInt32 Data
            {
                set
                {
                    //The reverse array methods exist to counter the default little endian
                    //that the bitconverter returns
                    base.Data = ArrayHelper.ReverseArray(BitConverter.GetBytes(value));
                }
                get { return BitConverter.ToUInt32(ArrayHelper.ReverseArray(base.Data), 0); }
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


