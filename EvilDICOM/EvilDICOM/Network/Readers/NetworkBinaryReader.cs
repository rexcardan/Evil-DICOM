﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.IO.Reading;
using System.Diagnostics;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.DIMSE;

namespace EvilDICOM.Network.Readers
{
    /// <summary>
    /// Class NetworkBinaryReader.
    /// </summary>
    public class NetworkBinaryReader
    {
        #region PRIVATE
        /// <summary>
        /// The _binary reader
        /// </summary>
        protected BinaryReader _binaryReader;
        /// <summary>
        /// The _logger
        /// </summary>
        private IByteLogger _logger;
        #endregion

        /// <summary>
        /// Constructs a DICOM binary reader from a network stream
        /// </summary>
        /// <param name="stream">The stream.</param>
        public NetworkBinaryReader(BufferedStream stream)
            : base()
        {
            _binaryReader = new BinaryReader(stream, new ASCIIEncoding());
        }

        /// <summary>
        /// Constructs a DICOM binary reader from a network stream
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="logger">The logger.</param>
        public NetworkBinaryReader(BufferedStream stream, IByteLogger logger)
            : base()
        {
            _binaryReader = new BinaryReader(stream, new ASCIIEncoding());
            _logger = logger;
        }

        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public void SetLogger(IByteLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Reads the specified number of bytes
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public byte[] ReadBytes(int count)
        {
            var read = new List<byte>();
            while (read.Count < count)
            {
                var buffer = new byte[count - read.Count];
                var numRead = _binaryReader.Read(buffer, 0, count - read.Count);
                read.AddRange(buffer.Take(numRead));
            }
            var bytes = read.ToArray();
            if (_logger != null) { _logger.Log(bytes); }
            return bytes;

        }

        /// <summary>
        /// Reads the specified number of bytes (shorthand for ReadBytes method).
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public byte[] Take(int count)
        {
            var read = ReadBytes(count);
            return read;
        }

        /// <summary>
        /// Creates a new stream that is trimmed to the specification length.
        /// </summary>
        /// <param name="substreamLength">the number of bytes to include in the new stream (starting from the current position)</param>
        /// <returns>DICOMBinaryReader.</returns>
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
            var charBytes = ReadBytes(count);
            return ASCIIEncoding.UTF8.GetChars(charBytes);
        }

        /// <summary>
        /// Reads the specified number of chars and converts to a string
        /// </summary>
        /// <param name="length">the number of chars to be read</param>
        /// <returns>the read chars</returns>
        public string ReadString(int length)
        {
            var buffer = ReadChars(length);
            return new string(buffer);
        }

        /// <summary>
        /// Skips the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>NetworkBinaryReader.</returns>
        public NetworkBinaryReader Skip(int count)
        {
            ReadBytes(count);
            return this;
        }

        /// <summary>
        /// Dumps the log.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DumpLog<T>()
        {
            var name = typeof(T).Name;
            if (_logger!= null) { _logger.Dump(name); }
        }
    }
}
