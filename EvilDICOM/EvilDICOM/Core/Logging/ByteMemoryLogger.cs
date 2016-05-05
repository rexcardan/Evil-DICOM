using EvilDICOM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Logging
{
    /// <summary>
    /// A class to store bytes for a time, and then dump the bytes to a file and reset
    /// </summary>
    public class ByteMemoryLogger : IByteLogger
    {

        private MemoryStream _mem;
        private int _messageNum = 0;
        private string _dir;

        public ByteMemoryLogger(string folder)
        {
            _mem = new MemoryStream();
            _dir = folder;
        }

        /// <summary>
        /// Adds new bytes to the currently stored bytes
        /// </summary>
        /// <param name="bytes"></param>
        public void Log(byte[] bytes)
        {
            _mem.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Dumps the current stored bytes to a file and resets the stored bytes
        /// </summary>
        /// <param name="file"></param>
        public void Dump(string file)
        {
            var fn = Path.GetFileNameWithoutExtension(file);

            file = Path.Combine(_dir, fn + _messageNum + ".bin");
            File.WriteAllBytes(file, _mem.ToArray());
            _mem.Dispose();
            _mem = new MemoryStream();
        }
    }
}
