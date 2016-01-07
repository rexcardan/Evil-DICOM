using System.Text;

namespace EvilDICOM.Core.IO.Data
{
    public class DICOMString
    {
        private static Encoding _encoder = Encoding.UTF8;

        public static void SetReaderEncoder(string encoderString)
        {
            _encoder = Encoding.GetEncoding(encoderString);
        }

        public static string Read(byte[] data)
        {
            Encoding enc = _encoder;
            return enc.GetString(data).TrimEnd(new[] {'\0'}).TrimEnd(new[] {' '});
        }

        public static byte[] Write(string data)
        {
            Encoding ascii = _encoder;

            if (IsEven(data))
            {
                return ascii.GetBytes(data);
            }
            return PadOddBytes(ascii, data);
        }

        private static bool IsEven(string data)
        {
            return data.Length%2 == 0;
        }

        private static byte[] PadOddBytes(Encoding ascii, string data)
        {
            return ascii.GetBytes(data + '\0');
        }
    }
}