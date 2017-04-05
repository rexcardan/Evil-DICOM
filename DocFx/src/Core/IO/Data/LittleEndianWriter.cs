using System;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Writing;

namespace EvilDICOM.Core.IO.Data
{
    public class LittleEndianWriter
    {
        public static byte[] WriteTag(DICOMData<Tag> data)
        {
            //TODO modify to make VM > 1 possible
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteTagSingle);
        }

        public static byte[] WriteSinglePrecision(DICOMData<float> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSinglePrecisionSingle);
        }

        public static byte[] WriteDoublePrecision(DICOMData<double> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteDoublePrecisionSingle);
        }

        public static byte[] WriteSignedLong(DICOMData<int> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSignedLongSingle);
        }

        public static byte[] WriteUnsignedLong(DICOMData<uint> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteUnsignedLongSingle);
        }

        public static byte[] WriteSignedShort(DICOMData<short> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSignedShortSingle);
        }

        public static byte[] WriteUnsignedShort(DICOMData<ushort> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteUnsignedShortSingle);
        }

        #region SINGLE WRITERS

        public static byte[] WriteTagSingle(Tag tag)
        {
            return DICOMTagWriter.WriteLittleEndian(tag);
        }

        public static byte[] WriteSinglePrecisionSingle(float data)
        {
            return BitConverter.GetBytes(data);
        }

        public static byte[] WriteDoublePrecisionSingle(double data)
        {
            return BitConverter.GetBytes(data);
        }

        public static byte[] WriteSignedLongSingle(int data)
        {
            return BitConverter.GetBytes(data);
        }

        public static byte[] WriteUnsignedLongSingle(uint data)
        {
            return BitConverter.GetBytes(data);
        }

        public static byte[] WriteSignedShortSingle(short data)
        {
            return BitConverter.GetBytes(data);
        }

        public static byte[] WriteUnsignedShortSingle(ushort data)
        {
            return BitConverter.GetBytes(data);
        }

        #endregion

        //Writes a single data element (VM = 1)
    }
}