using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOM.Core.IO.Data
{
    public class LittleEndianReader
    {
        public static Tag ReadTag(byte[] data)
        {
            if(CheckForNullData(data)){return null;}
            return TagReader.ReadLittleEndian(data);
        }

        private static bool CheckForNullData(byte[] data)
        {
            return data.Length == 0;
        }

        public static float? ReadSinglePrecision(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToSingle(data, 0);
        }

        public static double? ReadDoublePrecision(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToDouble(data, 0);
        }

        public static int? ReadSignedLong(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToInt32(data, 0);
        }

        public static uint? ReadUnsignedLong(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToUInt32(data, 0);
        }

        public static short? ReadSignedShort(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToInt16(data, 0);
        }

        public static ushort? ReadUnsignedShort(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToUInt16(data, 0);
        }
    }
}
