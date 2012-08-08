using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using System.IO;

namespace EvilDicom.Helper
{
    /// <summary>
    /// A class for writing DICOM files.
    /// </summary>
    public class DICOMWriter
    {
        /// <summary>
        /// This method writes the bytes of the DICOM preamble which consists of 128 null bytes followed
        /// byte ASCII characters DIC and M.
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes of the preamble</param>
        public static void WriteDicomPreamble(BinaryWriter b)
        {
            for (int i = 0; i < 128; i++)
            {
                b.Write((byte)0x00);
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            foreach (byte bt in ascii.GetBytes("DICM"))
            {
                b.Write(bt);
            }
        }

        /// <summary>
        /// This method writes the bytes of the passed in tag to the binary writer
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes of the tag ID</param>
        /// <param name="tag">The Tag containing the ID to write</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public static void WriteTag(BinaryWriter b, Tag tag, bool isLittleEndian)
        {
            if (tag != null)
            {
                byte[] groupBytes = ByteHelper.HexStringToByteArray(tag.Group);
                byte[] elemBytes = ByteHelper.HexStringToByteArray(tag.Element);

                if (isLittleEndian)
                {
                    groupBytes = ArrayHelper.ReverseArray(groupBytes);
                    elemBytes = ArrayHelper.ReverseArray(elemBytes);
                }
                b.Write(groupBytes[0]);
                b.Write(groupBytes[1]);
                b.Write(elemBytes[0]);
                b.Write(elemBytes[1]);
            }
        }

        /// <summary>
        /// This method writes the bytes of the passed in tag to a byte array
        /// </summary>
        /// <param name="b">The reference byte array to write the bytes of the tag ID</param>
        /// <param name="tag">The Tag containing the ID to write</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public static void WriteTag(ref byte[] bytes, Tag tag, bool isLittleEndian)
        {
            byte[] groupBytes = ByteHelper.HexStringToByteArray(tag.Group);
            byte[] elemBytes = ByteHelper.HexStringToByteArray(tag.Element);
            if (isLittleEndian)
            {
                groupBytes = ArrayHelper.ReverseArray(groupBytes);
                elemBytes = ArrayHelper.ReverseArray(elemBytes);
            }
            bytes[0] = (groupBytes[0]);
            bytes[1] = (groupBytes[1]);
            bytes[2] = (elemBytes[0]);
            bytes[3] = (elemBytes[1]);
        }

        /// <summary>
        /// This method writes the two letter ASCII characters (in byte form) of the passed in VR to the
        /// binary writer.
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes to</param>
        /// <param name="vr">The two letter string VR to write</param>
        /// <param name="encType">The encoding type of this VR</param>
        public static void WriteVR(BinaryWriter b, string vr, Constants.EncodeType encType)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            switch (encType)
            {
                case Constants.EncodeType.IMPLICIT:
                    return;
                case Constants.EncodeType.EXPLICIT_2:
                    b.Write(ascii.GetBytes(vr)[0]);
                    b.Write(ascii.GetBytes(vr)[1]);
                    return;
                case Constants.EncodeType.EXPLICIT_4:
                    b.Write(ascii.GetBytes(vr)[0]);
                    b.Write(ascii.GetBytes(vr)[1]);
                    b.Write((byte)0x00);
                    b.Write((byte)0x00);
                    return;
            }
        }

        /// <summary>
        /// This method writes the length of the DICOM object to the binary writer
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes to</param>
        /// <param name="encType">The encoding type of the VR</param>
        /// <param name="length">The integer length of the byte data</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public static void WriteLength(BinaryWriter b, Constants.EncodeType encType, int length, bool isLittleEndian)
        {
            switch (encType)
            {
                default:
                    byte[] lengthBytes = BitConverter.GetBytes(length);
                    if (!isLittleEndian) { lengthBytes = ArrayHelper.ReverseArray(lengthBytes); }
                    b.Write(lengthBytes[0]);
                    b.Write(lengthBytes[1]);
                    b.Write(lengthBytes[2]);
                    b.Write(lengthBytes[3]);
                    break;
                case Constants.EncodeType.EXPLICIT_2:
                    short shortLength = (short)length;
                    lengthBytes = BitConverter.GetBytes(length);
                    if (!isLittleEndian) { lengthBytes = ArrayHelper.ReverseArray(lengthBytes); }
                    b.Write(lengthBytes[0]);
                    b.Write(lengthBytes[1]);
                    break;
            }
        }

        /// <summary>
        /// This method should only be used to write the zero or indefinite
        /// length attributes as there is no correction for endianess;
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes to</param>
        /// <param name="length">The integer length of the byte data</param>
        public static void WriteLength(BinaryWriter b, byte[] length)
        {
            foreach (byte bt in length)
            {
                b.Write(bt);
            }
        }

        /// <summary>
        /// This method writes the writes the byte data to the incoming Binary Writer
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes to</param>
        /// <param name="d">The DicomObject containing the data to be written</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public static void WriteData(BinaryWriter b, DICOMElement d, bool isLittleEndian)
        {
            if (d.IsLittleEndian != isLittleEndian)
            {
                d.Encode(isLittleEndian);
            }
            foreach (byte bt in d.ByteData)
            {
                b.Write(bt);
            }
        }

        /// <summary>
        /// This method writes the writes the byte data to the incoming Binary Writer
        /// </summary>
        /// <param name="b">The Binary writer to write the bytes to</param>
        /// <param name="d">The DicomObject containing the data to be written</param>
        /// <param name="isLittleEndian">A boolean that indicates whether or not the bytes are written in little or big endian.</param>
        public static void WriteBytes(BinaryWriter b, byte[] bytes, bool isLittleEndian)
        {
            foreach (byte bt in bytes)
            {
                b.Write(bt);
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


