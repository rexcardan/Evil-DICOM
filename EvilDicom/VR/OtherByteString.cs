using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.Helper;

namespace EvilDicom
{
    namespace VR
    {
        public class OtherByteString : DICOMElement
        {
            public OtherByteString() { VR = "OB"; }

            public byte[] Data
            {
                get
                {
                    //If byte data is even return byte data,
                    //Otherwise, trim last byte and return that
                    if (ByteData.Length % 2 == 0)
                    {
                        return ByteData;
                    }
                    else
                    {
                        byte[] trimmedData = new byte[ByteData.Length - 1];
                        for (int i = 0; i < trimmedData.Length; i++)
                        {
                            trimmedData[i] = ByteData[i];
                        }
                        return trimmedData;
                    }
                }
                set
                {
                    //If the incoming byte array is even, set ByteData to it,
                    //Otherwise, pad byte array
                    if (value.Length % 2 == 0) { ByteData = value; }
                    else
                    {
                        ByteData = new byte[value.Length + 1];
                        for (int i = 0; i < value.Length; i++)
                        {
                            ByteData[i] = value[i];
                        }
                        //Pad byte array to make it even
                        ByteData[value.Length] = 0x00;
                    }
                }
            }
        }
    }
}

//Copyright © 2012 Rex Cardan, Ph.D


