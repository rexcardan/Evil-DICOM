#region

using System;
using System.IO;
using System.Text;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMBinaryWriter : IDisposable
    {
        private readonly BinaryWriter _writer;

        protected DICOMBinaryWriter()
        {
#if NETCOREAPP

            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
#endif
        }
        /// <summary>
        ///     Constructs a new writer from a file path.
        /// </summary>
        /// <param name="filePath">path to the file to be written</param>
        public DICOMBinaryWriter(string filePath):base()
        {
            _writer = new BinaryWriter(
                File.Open(filePath, FileMode.Create),
                Encoding.UTF8);
        }

        public DICOMBinaryWriter(Stream stream) : base()
        {
            _writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        public void Write(byte b)
        {
            _writer.Write(b);
        }

        public void Write(byte[] bytes)
        {
            _writer.Write(bytes);
        }

        public void Write(char[] chars)
        {
            _writer.Write(chars);
        }

        public void Write(string chars)
        {
            var asCharArray = chars.ToCharArray();
            Write(asCharArray);
        }

        public void WriteNullBytes(int numberToWrite)
        {
            for (var i = 0; i < numberToWrite; i++)
                Write(0x00);
        }
    }
}