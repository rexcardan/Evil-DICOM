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
        public class OtherFloatString : DICOMElement
        {
            private float[] data;
            public OtherFloatString() { VR = "OF"; }

            public float[] Data
            {
                get
                {
                    if (this.data != null) { return this.data; }
                    else
                    {
                        float[] newData = new float[ByteData.Length / 4];
                        for (int i = 0; i < ByteData.Length; i += 4)
                        {
                            byte[] bytes = new byte[] { ByteData[i], ByteData[i + 1], ByteData[i + 2], ByteData[i + 3] };
                            if (!IsLittleEndian) { bytes = ArrayHelper.ReverseArray(bytes); }
                            newData[i / 4] = BitConverter.ToSingle(bytes, 0);
                        }
                        this.data = newData;
                        return newData;
                    }

                }
                set
                {
                    this.data = value;
                    ByteData = new byte[value.Length * 4];
                    for (int i = 0; i < ByteData.Length; i += 4)
                    {
                        byte[] bytes = BitConverter.GetBytes(value[i / 4]);
                        if (!IsLittleEndian) { bytes = ArrayHelper.ReverseArray(bytes); }
                        ByteData[i] = bytes[0];
                        ByteData[i + 1] = bytes[1];
                        ByteData[i + 2] = bytes[2];
                        ByteData[i + 3] = bytes[3];
                    }
                }
            }

            public override void Encode(bool isLittleEndian)
            {
                if (isLittleEndian == this.IsLittleEndian) { return; }
                else
                {
                    float[] data = this.Data;
                    this.IsLittleEndian = IsLittleEndian;
                    this.Data = data;
                }
            }
        }
    }
}

//Copyright © 2012 Rex Cardan, Ph.D


