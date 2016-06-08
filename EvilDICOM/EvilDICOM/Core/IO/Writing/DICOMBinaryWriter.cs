using System;
using System.IO;
using System.Text;

namespace EvilDICOM.Core.IO.Writing
{
    /// <summary>
    /// Class DICOMBinaryWriter.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class DICOMBinaryWriter : IDisposable
    {
        /// <summary>
        /// The _writer
        /// </summary>
        private readonly BinaryWriter _writer;

        /// <summary>
        /// Constructs a new writer from a file path.
        /// </summary>
        /// <param name="filePath">path to the file to be written</param>
        public DICOMBinaryWriter(string filePath)
        {
            _writer = new BinaryWriter(
                File.Open(filePath, FileMode.Create),
                Encoding.UTF8);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DICOMBinaryWriter"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public DICOMBinaryWriter(Stream stream)
        {
            _writer = new BinaryWriter(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }

        /// <summary>
        /// Writes the specified b.
        /// </summary>
        /// <param name="b">The b.</param>
        public void Write(byte b)
        {
            _writer.Write(b);
        }

        /// <summary>
        /// Writes the specified bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        public void Write(byte[] bytes)
        {
            _writer.Write(bytes);
        }

        /// <summary>
        /// Writes the specified chars.
        /// </summary>
        /// <param name="chars">The chars.</param>
        public void Write(char[] chars)
        {
            _writer.Write(chars);
        }

        /// <summary>
        /// Writes the specified chars.
        /// </summary>
        /// <param name="chars">The chars.</param>
        public void Write(string chars)
        {
            char[] asCharArray = chars.ToCharArray();
            Write(asCharArray);
        }

        /// <summary>
        /// Writes the null bytes.
        /// </summary>
        /// <param name="numberToWrite">The number to write.</param>
        public void WriteNullBytes(int numberToWrite)
        {
            for (int i = 0; i < numberToWrite; i++)
            {
                Write(0x00);
            }
        }
    }
}