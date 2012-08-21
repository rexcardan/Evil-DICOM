using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace EvilDICOM.Core.Extensions
{
    public static class ByteExtensions
    {
        public static bool GetBit(this byte b, int bitNumber)
        {
            BitArray bits = new BitArray(new byte[] { b });
            return bits.Get(bitNumber);
        }

        public static byte[] Append(this byte[] first, byte[] toAppend)
        {
            byte[] result = new byte[0]; ;
            using (var stream = new MemoryStream())
            {
                stream.Write(first, 0, first.Length);
                stream.Write(toAppend, 0, toAppend.Length);
                result = stream.ToArray();
            }
            return result;
        }
    }
}
