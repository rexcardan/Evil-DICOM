using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.IO.Data
{
    public abstract class AbstractBinaryDataReader
    {
        #region CORE READ METHODS
        public static Tag[] ReadTag(byte[] data, Func<byte[], Tag> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<Tag>(data, 4, readSingleFunc);
        }

        public static float[] ReadSinglePrecision(byte[] data, Func<byte[], float> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<float>(data, 4, readSingleFunc);
        }

        public static double[] ReadDoublePrecision(byte[] data, Func<byte[], double> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<double>(data, 8, readSingleFunc);
        }

        public static int[] ReadSignedLong(byte[] data, Func<byte[], int> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<int>(data, 4, readSingleFunc);
        }

        public static uint[] ReadUnsignedLong(byte[] data, Func<byte[], uint> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<uint>(data, 4, readSingleFunc);
        }

        public static short[] ReadSignedShort(byte[] data, Func<byte[], short> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<short>(data, 2, readSingleFunc);
        }

        public static ushort[] ReadUnsignedShort(byte[] data, Func<byte[], ushort> readSingleFunc)
        {
            if (CheckForNullData(data)) { return null; }
            return MultiplicityReader.ReadMultipleBinary<ushort>(data, 2, readSingleFunc);
        }
        #endregion
  
        private static bool CheckForNullData(byte[] data)
        {
            return data.Length == 0;
        }
    }
}
