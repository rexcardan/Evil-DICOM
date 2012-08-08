using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.Helper;
using System.IO;

namespace EvilDicom
{
    namespace VR
    {
        public class OtherWordString : DICOMElement
        {
            private short[] data;
            public OtherWordString() { VR = "OW"; }

            public short[] Data
            {
                get
                {
                    if (data != null) { return data; }
                    else
                    {
                        using (BinaryReader r = new BinaryReader(new MemoryStream(ByteData)))
                        {
                            short[] newData = new short[ByteData.Length / 2];
                            byte[] bytes = new byte[2];
                            while (r.BaseStream.Position < r.BaseStream.Length)
                            {
                                r.Read(bytes, 0, 2);
                                if (!IsLittleEndian) { bytes = ArrayHelper.ReverseArray(bytes); }
                                newData[(r.BaseStream.Position-2)/ 2] = (short)((bytes[1] << 8) | (bytes[0] << 0));
                            }

                            data = newData;
                            return newData;
                        }
                    }

                }
                set
                {
                    data = value;
                    ByteData = new byte[value.Length * 2];
                    for (int i = 0; i < ByteData.Length; i += 2)
                    {
                        byte[] bytes = BitConverter.GetBytes(value[i / 2]);
                        if (!IsLittleEndian) { bytes = ArrayHelper.ReverseArray(bytes); }
                        ByteData[i] = bytes[0];
                        ByteData[i + 1] = bytes[1];
                    }
                }
            }

            public override void Encode(bool isLittleEndian)
            {
                if (isLittleEndian == this.IsLittleEndian) { return; }
                else
                {
                    short[] data = this.Data;
                    this.IsLittleEndian = IsLittleEndian;
                    this.Data = data;
                }
            }
        }
    }
}

//Copyright © 2012 Rex Cardan, Ph.D


