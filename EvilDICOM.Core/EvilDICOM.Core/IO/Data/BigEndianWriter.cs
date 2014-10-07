using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Writing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.IO.Data
{
    public class BigEndianWriter
    {
        public static byte[] WriteTag(DICOMData<Tag> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<Tag>(data, WriteTagSingle);
        }

        public static byte[] WriteSinglePrecision(DICOMData<float> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<float>(data, WriteSinglePrecisionSingle);
        }

        public static byte[] WriteDoublePrecision(DICOMData<double> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<double>(data, WriteDoublePrecisionSingle);
        }

        public static byte[] WriteSignedLong(DICOMData<int> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<int>(data, WriteSignedLongSingle);
        }

        public static byte[] WriteUnsignedLong(DICOMData<uint> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<uint>(data, WriteUnsignedLongSingle);
        }

        public static byte[] WriteSignedShort(DICOMData<short> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<short>(data, WriteSignedShortSingle);
        }

        public static byte[] WriteUnsignedShort(DICOMData<ushort> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary<ushort>(data, WriteUnsignedShortSingle);
        }

        //Writes a single data element (VM = 1)
        #region SINGLE WRITERS
        public static byte[] WriteTagSingle(Tag tag)
        {
            return DICOMTagWriter.WriteBigEndian(tag);
        }

        public static byte[] WriteSinglePrecisionSingle(float data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }

        public static byte[] WriteDoublePrecisionSingle(double data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }

        public static byte[] WriteSignedLongSingle(int data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }

        public static byte[] WriteUnsignedLongSingle(uint data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }

        public static byte[] WriteSignedShortSingle(short data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }

        public static byte[] WriteUnsignedShortSingle(ushort data)
        {
            return BitConverter.GetBytes(data).Reverse().ToArray();
        }
        #endregion
    }
}
