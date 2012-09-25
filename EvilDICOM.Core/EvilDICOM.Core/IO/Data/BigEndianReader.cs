using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOM.Core.IO.Data
{
    public class BigEndianReader
    {
        public static AttributeTag AttributeTag(Tag tag, byte[] data)
        {
            Tag aTag = TagReader.ReadBigEndian(data);
            return new AttributeTag(tag, aTag);
        }

        public static Tag ReadTag(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return TagReader.ReadBigEndian(data);
        }

        public static float? ReadSinglePrecision(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToSingle(data.Reverse().ToArray(), 0);
        }

        public static double? ReadDoublePrecision(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToDouble(data.Reverse().ToArray(), 0);
        }      

        public static int? ReadSignedLong(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToInt32(data.Reverse().ToArray(), 0);
        }
        public static uint? ReadUnsignedLong(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
        }

        public static short? ReadSignedShort(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToInt16(data.Reverse().ToArray(), 0);
        }

        public static ushort? ReadUnsignedShort(byte[] data)
        {
            if (CheckForNullData(data)) { return null; }
            return BitConverter.ToUInt16(data.Reverse().ToArray(), 0);
        }

        private static bool CheckForNullData(byte[] data)
        {
            return data.Length == 0;
        }
    }
}
