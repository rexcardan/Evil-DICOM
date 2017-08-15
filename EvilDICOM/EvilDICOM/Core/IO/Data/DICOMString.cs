#region

using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace EvilDICOM.Core.IO.Data
{
    public class DICOMString
    {
        public static string Read(byte[] data, StringEncoding enc)
        {
            return EncodingDictionary.GetEncodingFromISO(enc).GetString(data).TrimEnd('\0').TrimEnd(' ');
        }

        public static byte[] Write(string data, StringEncoding enc)
        {
            if (IsEven(data))
                return EncodingDictionary.GetEncodingFromISO(enc).GetBytes(data);
            return PadOddBytes(EncodingDictionary.GetEncodingFromISO(enc), data);
        }

        private static bool IsEven(string data)
        {
            return data.Length % 2 == 0;
        }

        private static byte[] PadOddBytes(Encoding enc, string data)
        {
            return enc.GetBytes(data + '\0');
        }

        public static List<string> ReadMultiple(byte[] data, StringEncoding enc)
        {
            var text = Read(data, enc);
            return text.Split('\\').ToList();
        }
    }
}