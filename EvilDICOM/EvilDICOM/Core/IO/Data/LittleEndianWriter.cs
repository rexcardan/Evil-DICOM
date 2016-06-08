using System;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Writing;

namespace EvilDICOM.Core.IO.Data
{
    /// <summary>
    /// Class LittleEndianWriter.
    /// </summary>
    public class LittleEndianWriter
    {
        /// <summary>
        /// Writes the tag.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteTag(DICOMData<Tag> data)
        {
            //TODO modify to make VM > 1 possible
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteTagSingle);
        }

        /// <summary>
        /// Writes the single precision.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSinglePrecision(DICOMData<float> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSinglePrecisionSingle);
        }

        /// <summary>
        /// Writes the double precision.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteDoublePrecision(DICOMData<double> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteDoublePrecisionSingle);
        }

        /// <summary>
        /// Writes the signed long.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSignedLong(DICOMData<int> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSignedLongSingle);
        }

        /// <summary>
        /// Writes the unsigned long.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteUnsignedLong(DICOMData<uint> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteUnsignedLongSingle);
        }

        /// <summary>
        /// Writes the signed short.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSignedShort(DICOMData<short> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteSignedShortSingle);
        }

        /// <summary>
        /// Writes the unsigned short.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteUnsignedShort(DICOMData<ushort> data)
        {
            return MultiplicityComposer.ComposeMultipleBinary(data, WriteUnsignedShortSingle);
        }

        #region SINGLE WRITERS

        /// <summary>
        /// Writes the tag single.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteTagSingle(Tag tag)
        {
            return DICOMTagWriter.WriteLittleEndian(tag);
        }

        /// <summary>
        /// Writes the single precision single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSinglePrecisionSingle(float data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// Writes the double precision single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteDoublePrecisionSingle(double data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// Writes the signed long single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSignedLongSingle(int data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// Writes the unsigned long single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteUnsignedLongSingle(uint data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// Writes the signed short single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteSignedShortSingle(short data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// Writes the unsigned short single.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteUnsignedShortSingle(ushort data)
        {
            return BitConverter.GetBytes(data);
        }

        #endregion

        //Writes a single data element (VM = 1)
    }
}