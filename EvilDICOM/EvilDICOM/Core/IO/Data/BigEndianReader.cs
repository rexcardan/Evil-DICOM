using System;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOM.Core.IO.Data
{
    public class BigEndianReader : AbstractBinaryDataReader
    {
        public static AttributeTag AttributeTag(Tag tag, byte[] data)
        {
            Tag aTag = TagReader.ReadBigEndian(data);
            return new AttributeTag(tag, aTag);
        }

        public static Tag ReadTag(byte[] data)
        {
            //TODO add support for VM > 1
            return ReadTag(data, ReadTagSingle).First();
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

        #region SINGLE DATA READERS

        public static float ReadSinglePrecisionSingle(byte[] data)
        {
            return BitConverter.ToSingle(data.Reverse().ToArray(), 0);
        }

        public static Tag ReadTagSingle(byte[] data)
        {
            return TagReader.ReadBigEndian(data);
        }

        public static double ReadDoublePrecisionSingle(byte[] data)
        {
            return BitConverter.ToDouble(data.Reverse().ToArray(), 0);
        }

        public static int ReadSignedLongSingle(byte[] data)
        {
            return BitConverter.ToInt32(data.Reverse().ToArray(), 0);
        }

        public static uint ReadUnsignedLongSingle(byte[] data)
        {
            return BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
        }

        public static short ReadSignedShortSingle(byte[] data)
        {
            return BitConverter.ToInt16(data.Reverse().ToArray(), 0);
        }

        public static ushort ReadUnsignedShortSingle(byte[] data)
        {
            return BitConverter.ToUInt16(data.Reverse().ToArray(), 0);
        }

        #endregion
    }
}