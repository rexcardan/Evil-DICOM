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

        public class AbstractBinaryVR : DICOMElement
        {
            public byte[] Data
            {
                set
                {
                    //Check to make sure byte array has even number of elements
                    //If not add null byte afterwards
                    if (value.Length % 2 == 0)
                    {
                        if (IsLittleEndian)
                        {
                            //By default the BitConverter returns little endian
                            ByteData = ArrayHelper.ReverseArray<byte>(value);
                        }
                        else
                        {
                            ByteData = new byte[value.Length];
                            Array.Copy(value, ByteData, value.Length);
                        }

                    }
                    else
                    {
                        ByteData = new byte[value.Length + 1];
                        Array.Copy(value, ByteData, value.Length);
                        //Pad insignificant byte 
                        ByteData[ByteData.Length - 1] = 0x00;
                        //Reverse if little endian
                        if (IsLittleEndian) { ByteData = ArrayHelper.ReverseArray(ByteData); }
                    }
                }
                get
                {
                    if (IsLittleEndian)
                    {
                        return ArrayHelper.ReverseArray<byte>(ByteData);
                    }
                    else
                    {
                        //Return a copy of the dicom byte array
                        byte[] bytesCopy = new byte[ByteData.Length];
                        Array.Copy(ByteData, bytesCopy, ByteData.Length);
                        return bytesCopy;
                    }
                }
            }

            public override void Encode(bool isLittleEndian)
            {
                if (isLittleEndian == this.IsLittleEndian) { return; }
                else {
                    byte[] data = this.Data;
                    this.IsLittleEndian = IsLittleEndian;
                    this.Data = data;
                }
            }

            public override string[] DataAsStringArray()
            {
                string[] sData = new string[Data.Length];
                for (int i=0;i<Data.Length;i++)
                {
                    sData[i] = ByteHelper.ByteArrayToHexString(new byte[] { Data[i] });
                }
                return sData;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


