using System;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.IO.Data
{
    public abstract class AbstractBinaryDataReader
    {
        #region CORE READ METHODS

        public static Tag[] ReadTag(byte[] data, Func<byte[], Tag> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 4, readSingleFunc);
        }

        public static float[] ReadSinglePrecision(byte[] data, Func<byte[], float> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 4, readSingleFunc);
        }

        public static double[] ReadDoublePrecision(byte[] data, Func<byte[], double> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 8, readSingleFunc);
        }

        public static int[] ReadSignedLong(byte[] data, Func<byte[], int> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 4, readSingleFunc);
        }

        public static uint[] ReadUnsignedLong(byte[] data, Func<byte[], uint> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 4, readSingleFunc);
        }

        public static short[] ReadSignedShort(byte[] data, Func<byte[], short> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 2, readSingleFunc);
        }

        public static ushort[] ReadUnsignedShort(byte[] data, Func<byte[], ushort> readSingleFunc)
        {
            if (CheckForNullData(data))
            {
                return null;
            }
            return MultiplicityReader.ReadMultipleBinary(data, 2, readSingleFunc);
        }

        #endregion

        private static bool CheckForNullData(byte[] data)
        {
            return data.Length == 0;
        }
    }
}