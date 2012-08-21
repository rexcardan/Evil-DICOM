using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMBinaryWriter : IDisposable
    {
        private BinaryWriter _writer;

        /// <summary>
        /// Constructs a new writer from a file path.
        /// </summary>
        /// <param name="filePath">path to the file to be written</param>
        public DICOMBinaryWriter(string filePath)
        {
            _writer = new BinaryWriter(
                File.Open(filePath, FileMode.Create),
                new ASCIIEncoding());
        }

        public DICOMBinaryWriter(Stream stream)
        {
            _writer = new BinaryWriter(stream,new ASCIIEncoding());
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
            char[] asCharArray = chars.ToCharArray(0, chars.Length);
            Write(asCharArray);
        }

        public void WriteNullBytes(int numberToWrite)
        {
            for (int i = 0; i < numberToWrite; i++)
            {
                Write(0x00);
            }
        }

        public void Dispose()
        {
            _writer.Close();
        }
    }
}
