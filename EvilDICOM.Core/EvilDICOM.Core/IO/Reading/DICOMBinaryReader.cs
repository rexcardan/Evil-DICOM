using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     A wrapper for the Binary Reader class that is specific to DICOM.
    /// </summary>
    public class DICOMBinaryReader : IDisposable
    {
        #region PRIVATE

        protected BinaryReader _binaryReader;

        #endregion

        //Constructor for inherited classes
        protected DICOMBinaryReader()
        {
        }

        /// <summary>
        ///     Constructs a new reader from a file path.
        /// </summary>
        /// <param name="filePath">path to the file to be read</param>
        public DICOMBinaryReader(string filePath)
        {
            _binaryReader = new BinaryReader(
                new FileStream(filePath, FileMode.Open, FileAccess.Read),
                Encoding.UTF8);
        }

        /// <summary>
        ///     Constructs a new reader from a byte array.
        /// </summary>
        /// <param name="byteStream">the byte array to be read</param>
        public DICOMBinaryReader(byte[] byteStream)
        {
            _binaryReader = new BinaryReader(new MemoryStream(byteStream),
                Encoding.UTF8);
        }

        /// <summary>
        ///     Returns the current position of the byte stream
        /// </summary>
        public long StreamPosition
        {
            get { return _binaryReader.BaseStream.Position; }
            set { _binaryReader.BaseStream.Position = value; }
        }

        /// <summary>
        ///     Returnts the length of the byte stream
        /// </summary>
        public long StreamLength
        {
            get { return _binaryReader.BaseStream.Length; }
        }

        public void Dispose()
        {
            _binaryReader.Close();
        }

        /// <summary>
        ///     Reads the specified number of bytes
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public virtual byte[] ReadBytes(int count)
        {
            var buffer = new byte[count];
            int read = _binaryReader.Read(buffer, 0, count);
            return buffer.Take(read).ToArray();
        }

        /// <summary>
        ///     Reads the specified number of bytes (shorthand for ReadBytes method).
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public virtual byte[] Take(int count)
        {
            return ReadBytes(count);
        }

        public virtual byte[] Peek(int count)
        {
            if (_binaryReader.BaseStream.CanSeek)
            {
                byte[] buffer = ReadBytes(count);
                StreamPosition -= count;
                return buffer;
            }
            return null;
        }

        /// <summary>
        ///     Creates a new stream that is trimmed to the specification length.
        /// </summary>
        /// <param name="substreamLength">the number of bytes to include in the new stream (starting from the current position)</param>
        public virtual DICOMBinaryReader GetSubStream(int substreamLength)
        {
            byte[] newStream = Take(substreamLength);
            return new DICOMBinaryReader(newStream);
        }

        /// <summary>
        ///     Reads the specified number of chars
        /// </summary>
        /// <param name="count">the number of chars to be read</param>
        /// <returns>the read chars</returns>
        public virtual char[] ReadChars(int count)
        {
            var buffer = new char[count];
            int read = _binaryReader.Read(buffer, 0, count);
            return buffer.Take(read).ToArray();
        }

        /// <summary>
        ///     Reads the specified number of chars and converts to a string
        /// </summary>
        /// <param name="count">the number of chars to be read</param>
        /// <returns>the read chars</returns>
        public virtual string ReadString(int length)
        {
            var buffer = new char[length];
            int read = _binaryReader.Read(buffer, 0, length);
            return new string(buffer.Take(read).ToArray());
        }

        public int ReadBytes(byte[] buffer, int index, int count)
        {
            return _binaryReader.Read(buffer, index, count);
        }

        public virtual DICOMBinaryReader Skip(int count)
        {
            ReadBytes(count);
            return this;
        }

        /// <summary>
        ///     Will return the index of a given byte pattern in the byte stream
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
                        return (int) (StreamPosition - bytePattern.Length);
                }
                else
                {
                    index = 0;
                }
            }
            return -1;
        }

        public void Reset()
        {
            _binaryReader.BaseStream.Position = 0;
        }
    }
}