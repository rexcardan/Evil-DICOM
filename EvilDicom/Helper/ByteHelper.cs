using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvilDicom.Helper
{
    /// <summary>
    /// A class containing useful methods for working with bytes
    /// </summary>
    public class ByteHelper
    {
        /// <summary>
        /// This method converts an array of bytes to a hexadecimal string
        /// </summary>
        /// <param name="Bytes">the array of bytes to be converted</param>
        /// <returns>a hexadecimal string representing the array of bytes passed in</returns>
        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder();
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        /// <summary>
        /// This method converts a hexadecimal string to an array of bytes.
        /// </summary>
        /// <param name="Hex">the hexadecimal string to be converted</param>
        /// <returns>an array of bytes representing the hexadecimal string passed in</returns>
        public static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D,
                                 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

        public static byte[] TagToLittleEndian(byte[] littleEndian)
        {
            return new byte[] { littleEndian[1], littleEndian[0], littleEndian[3], littleEndian[2] };
        }

    }

}




//Copyright © 2012 Rex Cardan, Ph.D


