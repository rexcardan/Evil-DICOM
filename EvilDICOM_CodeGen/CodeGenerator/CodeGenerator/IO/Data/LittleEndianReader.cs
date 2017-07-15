using System;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Data
{
    public class LittleEndianReader : AbstractBinaryDataReader
    {
        public static Tag ReadTag(byte[] data)
        {
            //TODO add support for VM > 1
            if (data.Any()) { return ReadTag(data, ReadTagSingle).First(); }
            else { return new Tag("00000000"); }
        }

        public static float[] ReadSinglePrecision(byte[] data)
        {
            return ReadSinglePrecision(data, ReadSinglePrecisionSingle);
        }

        public static double[] ReadDoublePrecision(byte[] data)
        {
            return ReadDoublePrecision(data, ReadDoublePrecisionSingle);
        }

        public static int[] ReadSignedLong(byte[] data)
        {
            return ReadSignedLong(data, ReadSignedLongSingle);
        }

        public static uint[] ReadUnsignedLong(byte[] data)
        {
            return ReadUnsignedLong(data, ReadUnsignedLongSingle);
        }

        public static short[] ReadSignedShort(byte[] data)
        {
            return ReadSignedShort(data, ReadSignedShortSingle);
        }

        public static ushort[] ReadUnsignedShort(byte[] data)
        {
            return ReadUnsignedShort(data, ReadUnsignedShortSingle);
        }

        #region SINGLE READERS

        public static Tag ReadTagSingle(byte[] data)
        {
            return TagReader.ReadLittleEndian(data);
        }

        public static float ReadSinglePrecisionSingle(byte[] data)
        {
            return BitConverter.ToSingle(data, 0);
        }

        public static double ReadDoublePrecisionSingle(byte[] data)
        {
            return BitConverter.ToDouble(data, 0);
        }

        public static int ReadSignedLongSingle(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        public static uint ReadUnsignedLongSingle(byte[] data)
        {
            return BitConverter.ToUInt32(data, 0);
        }

        public static short ReadSignedShortSingle(byte[] data)
        {
            return BitConverter.ToInt16(data, 0);
        }

        public static ushort ReadUnsignedShortSingle(byte[] data)
        {
            return BitConverter.ToUInt16(data, 0);
        }

        #endregion
    }
}