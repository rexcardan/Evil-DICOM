using System.Collections;
using System.IO;

namespace EvilDICOM.Core.Extensions
{
    /// <summary>
    ///     Adds useful methods to the byte and byte[] data types
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        ///     Gets a specific bit in a byte
        /// </summary>
        /// <param name="b">the byte containing the bit</param>
        /// <param name="bitNumber">the index of the bit within the byte (zero index based)</param>
        /// <returns></returns>
        public static bool GetBit(this byte b, int bitNumber)
        {
            var bits = new BitArray(new[] {b});
            return bits.Get(bitNumber);
        }

        /// <summary>
        ///     Appends a byte array to another byte array
        /// </summary>
        /// <param name="first">the first byte array</param>
        /// <param name="toAppend">the second byte array which will be appended to the first</param>
        /// <returns>the finished appended byte array</returns>
        public static byte[] Append(this byte[] first, byte[] toAppend)
        {
            var result = new byte[0];
            ;
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