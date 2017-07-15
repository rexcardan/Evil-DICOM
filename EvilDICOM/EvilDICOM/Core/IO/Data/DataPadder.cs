using System;

namespace EvilDICOM.Core.IO.Data
{
    public class DataPadder
    {
        /// <summary>
        ///     Pads null bytes around the data to make it even
        /// </summary>
        /// <param name="toPad"></param>
        /// <returns></returns>
        public static byte[] PadNull(byte[] toPad)
        {
            if (toPad != null)
            {
                return Pad(toPad, 0x00);
            }
            return toPad;
        }

        /// <summary>
        ///     Pads a space character around the data to make it even
        /// </summary>
        /// <param name="toPad"></param>
        /// <returns></returns>
        public static byte[] PadSpace(byte[] toPad)
        {
            return Pad(toPad, 0x20);
        }

        private static byte[] Pad(byte[] toPad, byte padCharacter)
        {
            if (!IsEven(toPad))
            {
                var padded = new byte[toPad.Length + 1];
                Array.Copy(toPad, padded, toPad.Length);
                padded[padded.Length - 1] = padCharacter;
                return padded;
            }
            return toPad;
        }

        /// <summary>
        ///     Checks to see if the data is an even number of bytes
        /// </summary>
        /// <param name="toPad"></param>
        /// <returns></returns>
        private static bool IsEven(byte[] toPad)
        {
            return toPad.Length%2 == 0;
        }
    }
}