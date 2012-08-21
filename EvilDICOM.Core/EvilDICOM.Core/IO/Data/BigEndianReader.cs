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

        internal static Tag ReadTag(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static double? ReadDoublePrecision(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static float? ReadSinglePrecision(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static int? ReadSignedLong(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static short? ReadSignedShort(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static uint? ReadUnsignedLong(byte[] p)
        {
            throw new NotImplementedException();
        }

        internal static ushort? ReadUnsignedShort(byte[] p)
        {
            throw new NotImplementedException();
        }
    }
}
