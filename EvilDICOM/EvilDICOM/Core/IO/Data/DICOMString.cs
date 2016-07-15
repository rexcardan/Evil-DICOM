using System.Text;

namespace EvilDICOM.Core.IO.Data
{
    public class DICOMString
    {
        private static Encoding _encoder = Encoding.UTF8;

        public static string Read(byte[] data)
        {
            return _encoder.GetString(data).TrimEnd(new[] {'\0'}).TrimEnd(new[] {' '});
        }

        public static byte[] Write(string data)
        {
            if (IsEven(data))
            {
                return _encoder.GetBytes(data);
            }
            return PadOddBytes(_encoder, data);
        }

        private static bool IsEven(string data)
        {
            return data.Length%2 == 0;
        }

        private static byte[] PadOddBytes(Encoding ascii, string data)
        {
            return ascii.GetBytes(data + '\0');
        }

        public void SetEncoder(Encoding enc)
        {
            _encoder = enc;
        }

    }
}