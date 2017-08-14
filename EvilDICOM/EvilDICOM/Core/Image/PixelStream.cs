using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Core.Image
{
    public class PixelStream : MemoryStream
    {
        public PixelStream(IEnumerable<byte> bytes) : base(bytes.ToArray()) { }

        /// <summary>
        /// Converts the byte array to 64 bit integer array
        /// </summary>
        /// <returns></returns>
        public long[] GetValues64(bool isDataLittleEndian = true)
        {
            long[] values = new long[this.Length / 8];
            var binReader = new BinaryReader(this);
            int i = 0;
            binReader.BaseStream.Position = 0;
            while (binReader.BaseStream.Position < binReader.BaseStream.Length)
            {
                var data = binReader.ReadBytes(8);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                values[i++] = BitConverter.ToInt64(data, 0);
            }
            return values;
        }

        public void SetValues8(int[] pixels)
        {
            var binWriter = new BinaryWriter(this);
            binWriter.BaseStream.Position = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                var data = (byte)pixels[i];
                binWriter.Write(data);
            }
        }

        /// <summary>
        /// Converts the byte array to 32 bit integer array
        /// </summary>
        /// <returns></returns>
        public int[] GetValues32(bool isDataLittleEndian = true)
        {
            int[] values = new int[this.Length / 4];
            var binReader = new BinaryReader(this);
            int i = 0;
            binReader.BaseStream.Position = 0;
            while (binReader.BaseStream.Position < binReader.BaseStream.Length)
            {
                var data = binReader.ReadBytes(4);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                values[i++] = BitConverter.ToInt32(data, 0);
            }
            return values;
        }

        /// <summary>
        /// Converts the integer pixels into bytes and sets the pixel data
        /// </summary>
        /// <returns></returns>
        public void SetValues32(int[] pixels, bool isDataLittleEndian = true)
        {
            var binWriter = new BinaryWriter(this);
            binWriter.BaseStream.Position = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                var data = BitConverter.GetBytes(pixels[i]);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                binWriter.Write(data);
            }
        }

        /// <summary>
        /// Converts the integer pixels into bytes and sets the pixel data
        /// </summary>
        /// <returns></returns>
        public void SetValues32(ushort[] pixels, bool isDataLittleEndian = true)
        {
            var binWriter = new BinaryWriter(this);
            binWriter.BaseStream.Position = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                var data = BitConverter.GetBytes(pixels[i]);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                binWriter.Write(data);
            }
        }

        /// <summary>
        /// Converts the integer pixels into bytes and sets the pixel data
        /// </summary>
        public void SetValues16(int[] pixels, bool isDataLittleEndian = true)
        {
            var binWriter = new BinaryWriter(this);
            binWriter.BaseStream.Position = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                var data = BitConverter.GetBytes((Int16)pixels[i]);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                binWriter.Write(data);
            }
        }

        /// <summary>
        /// Converts the byte array to 16 bit integer array
        /// </summary>
        /// <returns></returns>
        public short[] GetValues16(bool isDataLittleEndian = true)
        {
            short[] values = new short[this.Length / 2];
            var binReader = new BinaryReader(this);
            int i = 0;
            binReader.BaseStream.Position = 0;
            while (binReader.BaseStream.Position < binReader.BaseStream.Length)
            {
                var data = binReader.ReadBytes(2);
                if (isDataLittleEndian != BitConverter.IsLittleEndian) Array.Reverse(data);
                values[i++] = BitConverter.ToInt16(data, 0);
            }
            return values;
        }
    }
}
