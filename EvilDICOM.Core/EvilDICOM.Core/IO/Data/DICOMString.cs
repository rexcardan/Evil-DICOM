using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.IO.Data
{
    public class DICOMString
    {
        public static string Read(byte[] data)
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            return enc.GetString(data).TrimEnd(new char[] { '\0' }).TrimEnd(new char[] { ' ' });
        }

        public static byte[] Write(string data)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

            if (IsEven(data))
            {
                return ascii.GetBytes(data);
            }
            else
            {
                return PadOddBytes(ascii, data);
            }           
        }

        private static bool IsEven(string data)
        {
            return data.Length % 2 == 0;
        }

        private static byte[] PadOddBytes(ASCIIEncoding ascii, string data)
        {
            return ascii.GetBytes(data + '\0');
        }

      
    }
}
