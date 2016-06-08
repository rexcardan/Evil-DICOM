using System;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOM.Core.IO.Data
{
    /// <summary>
    /// Class BigEndianReader.
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.IO.Data.AbstractBinaryDataReader" />
    public class BigEndianReader : AbstractBinaryDataReader
    {
        /// <summary>
        /// Attributes the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        /// <returns>AttributeTag.</returns>
        public static AttributeTag AttributeTag(Tag tag, byte[] data)
        {
            Tag aTag = TagReader.ReadBigEndian(data);
            return new AttributeTag(tag, aTag);
        }

        /// <summary>
        /// Reads the tag.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Tag.</returns>
        public static Tag ReadTag(byte[] data)
        {
            //TODO add support for VM > 1
            return ReadTag(data, ReadTagSingle).First();
        }

        /// <summary>
        /// Reads the single precision.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Single[].</returns>
        public static float[] ReadSinglePrecision(byte[] data)
        {
            return ReadSinglePrecision(data, ReadSinglePrecisionSingle);
        }

        /// <summary>
        /// Reads the double precision.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Double[].</returns>
        public static double[] ReadDoublePrecision(byte[] data)
        {
            return ReadDoublePrecision(data, ReadDoublePrecisionSingle);
        }

        /// <summary>
        /// Reads the signed long.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int32[].</returns>
        public static int[] ReadSignedLong(byte[] data)
        {
            return ReadSignedLong(data, ReadSignedLongSingle);
        }

        /// <summary>
        /// Reads the unsigned long.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.UInt32[].</returns>
        public static uint[] ReadUnsignedLong(byte[] data)
        {
            return ReadUnsignedLong(data, ReadUnsignedLongSingle);
        }

        /// <summary>
        /// Reads the signed short.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int16[].</returns>
        public static short[] ReadSignedShort(byte[] data)
        {
            return ReadSignedShort(data, ReadSignedShortSingle);
        }

        /// <summary>
        /// Reads the unsigned short.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.UInt16[].</returns>
        public static ushort[] ReadUnsignedShort(byte[] data)
        {
            return ReadUnsignedShort(data, ReadUnsignedShortSingle);
        }

        #region SINGLE DATA READERS

        /// <summary>
        /// Reads the single precision single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Single.</returns>
        public static float ReadSinglePrecisionSingle(byte[] data)
        {
            return BitConverter.ToSingle(data.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// Reads the tag single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Tag.</returns>
        public static Tag ReadTagSingle(byte[] data)
        {
            return TagReader.ReadBigEndian(data);
        }

        /// <summary>
        /// Reads the double precision single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Double.</returns>
        public static double ReadDoublePrecisionSingle(byte[] data)
        {
            return BitConverter.ToDouble(data.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// Reads the signed long single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int32.</returns>
        public static int ReadSignedLongSingle(byte[] data)
        {
            return BitConverter.ToInt32(data.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// Reads the unsigned long single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.UInt32.</returns>
        public static uint ReadUnsignedLongSingle(byte[] data)
        {
            return BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// Reads the signed short single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int16.</returns>
        public static short ReadSignedShortSingle(byte[] data)
        {
            return BitConverter.ToInt16(data.Reverse().ToArray(), 0);
        }

        /// <summary>
        /// Reads the unsigned short single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.UInt16.</returns>
        public static ushort ReadUnsignedShortSingle(byte[] data)
        {
            return BitConverter.ToUInt16(data.Reverse().ToArray(), 0);
        }

        #endregion
    }
}