using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    /// A wrapper for the Binary Reader class that is specific to DICOM.
    /// </summary>
    public class DICOMBinaryReader : IDisposable
    {
        #region PRIVATE
        protected BinaryReader _binaryReader;
        #endregion

        //Constructor for inherited classes
        protected DICOMBinaryReader() { }
        /// <summary>
        /// Constructs a new reader from a file path.
        /// </summary>
        /// <param name="filePath">path to the file to be read</param>
        public DICOMBinaryReader(string filePath)
        {
            _binaryReader = new BinaryReader(
                new FileStream(filePath, FileMode.Open, FileAccess.Read),
                new System.Text.ASCIIEncoding());
        }

        /// <summary>
        /// Constructs a new reader from a byte array.
        /// </summary>
        /// <param name="byteStream">the byte array to be read</param>
        public DICOMBinaryReader(byte[] byteStream)
        {
            _binaryReader = new BinaryReader(new MemoryStream(byteStream),
                new System.Text.ASCIIEncoding());
        }

        /// <summary>
        /// Reads the specified number of bytes
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            _binaryReader.Read(buffer, 0, count);
            return buffer;
        }

        /// <summary>
        /// Reads the specified number of bytes (shorthand for ReadBytes method).
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public byte[] Take(int count)
        {
            return ReadBytes(count);
        }

        public byte[] Peek(int count)
        {
            byte[] buffer = this.ReadBytes(count);
            this.StreamPosition -= count;
            return buffer;
        }

        /// <summary>
        /// Creates a new stream that is trimmed to the specification length.
        /// </summary>
        /// <param name="substreamLength">the number of bytes to include in the new stream (starting from the current position)</param>
        public DICOMBinaryReader GetSubStream(int substreamLength)
        {
            byte[] newStream = Take(substreamLength);
            return new DICOMBinaryReader(newStream);
        }

        /// <summary>
        /// Reads the specified number of chars
        /// </summary>
        /// <param name="count">the number of chars to be read</param>
        /// <returns>the read chars</returns>
        public char[] ReadChars(int count)
        {
            char[] buffer = new char[count];
            _binaryReader.Read(buffer, 0, count);
            return buffer;
        }

        /// <summary>
        /// Reads the specified number of chars and converts to a string
        /// </summary>
        /// <param name="count">the number of chars to be read</param>
        /// <returns>the read chars</returns>
        public string ReadString(int length)
        {
            char[] buffer = new char[length];
            _binaryReader.Read(buffer, 0, length);
            return new string(buffer);
        }

        public void ReadBytes(byte[] buffer, int index, int count)
        {
            _binaryReader.Read(buffer, index, count);
        }

        public DICOMBinaryReader Skip(int count)
        {
            this.ReadBytes(count);
            return this;
        }

        public void Dispose()
        {
            _binaryReader.Close();
        }

        /// <summary>
        /// Will return the index of a given byte pattern in the byte stream
        /// </summary>
        /// <param name="bytePattern">the pattern to be found</param>
        /// <returns>the index of the pattern</returns>
        public long IndexOf(byte[] bytePattern)
        {
            int candidate;
            int index = 0;
            while ((candidate = _binaryReader.ReadByte()) != -1)
            {
                if (candidate == bytePattern[index++])
                {
                    if (index == bytePattern.Length)
                        return (int)(StreamPosition - bytePattern.Length);
                }
                else
                {
                    index = 0;
                }
            }
            return -1;
        }

        /// <summary>
        /// Returns the current position of the byte stream
        /// </summary>
        public long StreamPosition
        {
            get { return _binaryReader.BaseStream.Position; }
            set
            {
                _binaryReader.BaseStream.Position = value;
            }
        }

        /// <summary>
        /// Returnts the length of the byte stream
        /// </summary>
        public long StreamLength
        {
            get { return _binaryReader.BaseStream.Length; }
        }

        public void Reset()
        {
            _binaryReader.BaseStream.Position = 0;
        }
    }
}
