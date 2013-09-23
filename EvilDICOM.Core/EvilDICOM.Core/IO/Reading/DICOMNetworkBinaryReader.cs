using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

namespace EvilDICOM.Core.IO.Reading
{
    public class DICOMNetworkBinaryReader : DICOMBinaryReader
    {
        #region PRIVATE
        Socket _socket;
        NetworkStream _stream;
        #endregion
        /// <summary>
        /// Constructs a new reader from a stream.
        /// </summary>
        /// <param name="byteStream">the byte array to be read</param>
        public DICOMNetworkBinaryReader(NetworkStream stream, Socket socket)
        {
            _socket = socket;
            _stream = stream;
            _binaryReader = new BinaryReader(stream,
                new System.Text.ASCIIEncoding());
        }

        public new byte[] Peek(int count)
        {
            byte[] buffer = new byte[count];
            _socket.Receive(buffer, SocketFlags.Peek);
            return buffer;
        }

        public new DICOMNetworkBinaryReader Skip(int count)
        {
            ReadBytes(count);
            return this;
        }

        /// <summary>
        /// Reads the specified number of bytes. Will block until bytes are completely read (waiting on the network).
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public new byte[] ReadBytes(int count)
        {
            byte[] bytes = new byte[0];
            using (MemoryStream messageBytes = new MemoryStream())
            {
                var myReadBuffer = new byte[1024];
                int numberOfBytesRead = 0;
                var leftToRead = count - numberOfBytesRead;

                while (leftToRead > 0)
                {
                    if (_stream.CanRead)
                    {
                        while (_stream.DataAvailable && leftToRead > 0)
                        {
                            //Don't over-read
                            myReadBuffer = leftToRead > myReadBuffer.Length ? myReadBuffer : myReadBuffer = new byte[leftToRead];
                            numberOfBytesRead = _stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                            messageBytes.Write(myReadBuffer, 0, numberOfBytesRead);
                            leftToRead -= numberOfBytesRead;
                        }
                    }
                }

                bytes = messageBytes.ToArray();
            }
            return bytes;
        }

        /// <summary>
        /// Reads the specified number of bytes. Will block until bytes are completely read (waiting on the network).
        /// </summary>
        /// <param name="count">the number of bytes to be read</param>
        /// <returns>the read bytes</returns>
        public new byte[] ReadBytesAsync(int count)
        {
            List<byte> bytes= new List<byte>();
            var myReadBuffer = new byte[1024];
            int bytesRead = 0;
            var leftToRead =count - bytesRead;

            while (leftToRead > 0)
            {
                if (_stream.CanRead)
                {
                    Task readChunk = Task<int>.Factory.FromAsync(_stream.BeginRead, _stream.EndRead,
                            myReadBuffer, bytesRead, myReadBuffer.Length - bytesRead, null)
                    .ContinueWith((antecedent) =>
                    {
                        if (antecedent.Result < myReadBuffer.Length)
                            Array.Resize(ref myReadBuffer, antecedent.Result);

                        leftToRead -= antecedent.Result;

                        foreach (var b in myReadBuffer)
                        {
                            bytes.Add(b);
                        }
                    });

                    readChunk.Wait();
                }
            }
                        
            return bytes.ToArray();
        }

        public new long StreamPosition
        {
            get
            {
                throw new Exception("Unable to view position on NetworkStream.");
            }
            set
            {
                throw new Exception("Unable to change position on NetworkStream.");
            }

        }

        public new void Reset()
        {
            throw new Exception("Unable to reset NetworkStream.");
        }

        public byte[] ReadAllAvailable()
        {
            byte[] message = new byte[0];
            if (_stream.CanRead)
            {
                var myReadBuffer = new byte[1024];
                using (MemoryStream messageBytes = new MemoryStream())
                {
                    do
                    {
                        var numberOfBytesRead = _stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        messageBytes.Write(myReadBuffer, 0, numberOfBytesRead);
                    } while (_stream.DataAvailable);
                    message = messageBytes.ToArray();
                }
            }
            return message;
        }


        public bool AwaitingData
        {
            get
            {
                return !DataAvailable;
            }
        }

        public bool DataAvailable
        {
            get { return _stream.DataAvailable; }
        }
    }
}
