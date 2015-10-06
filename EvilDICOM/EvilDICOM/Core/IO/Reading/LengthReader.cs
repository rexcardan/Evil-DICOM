using System;
using System.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     Contains methods for reading the length of DICOM elements
    /// </summary>
    public class LengthReader
    {
        /// <summary>
        ///     Reads the length from a series of bytes in a stream. The number of bytes is automatically determined from
        ///     VR.
        /// </summary>
        /// <param name="vr">the value representation of the element</param>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <param name="syntax">the transfer syntax of this element</param>
        /// <returns></returns>
        public static int Read(VR vr, DICOMBinaryReader dr, TransferSyntax syntax)
        {
            switch (syntax)
            {
                case TransferSyntax.EXPLICIT_VR_BIG_ENDIAN:
                    return ReadBigEndian(vr, dr);
                default:
                    return ReadLittleEndian(vr, dr);
            }
        }

        /// <summary>
        ///     Reads the length in little endian byte format from a series of bytes in a stream
        /// </summary>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <param name="length">the number of bytes containing the length</param>
        /// <returns>the length</returns>
        public static int ReadLittleEndian(DICOMBinaryReader dr, int length)
        {
            switch (length)
            {
                case 2:
                    return BitConverter.ToUInt16(dr.Take(2), 0);
                case 4:
                    return BitConverter.ToInt32(dr.Take(4), 0);
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Reads the length in little endian byte format from a series of bytes in a stream. The number of bytes is
        ///     automatically determined from
        ///     VR.
        /// </summary>
        /// <param name="vr">the value representation of the element</param>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <returns></returns>
        public static int ReadLittleEndian(VR vr, DICOMBinaryReader dr)
        {
            int length = 0;

            switch (VRDictionary.GetEncodingFromVR(vr))
            {
                case VREncoding.Implicit:
                    byte[] byteLength = dr.ReadBytes(4);
                    length = BitConverter.ToInt32(byteLength, 0);
                    break;
                case VREncoding.ExplicitLong:
                    byteLength = dr.Skip(2).ReadBytes(4);
                    length = BitConverter.ToInt32(byteLength, 0);
                    break;
                case VREncoding.ExplicitShort:
                    byteLength = dr.ReadBytes(2);
                    length = BitConverter.ToUInt16(byteLength, 0);
                    break;
            }
            return length;
        }

        /// <summary>
        ///     Reads the length in big endian byte format from a series of bytes in a stream. The number of bytes is automatically
        ///     determined from
        ///     VR.
        /// </summary>
        /// <param name="vr">the value representation of the element</param>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <returns></returns>
        public static int ReadBigEndian(VR vr, DICOMBinaryReader dr)
        {
            int length = 0;

            switch (VRDictionary.GetEncodingFromVR(vr))
            {
                case VREncoding.Implicit:
                    byte[] byteLength = dr.ReadBytes(4).Reverse().ToArray();
                    length = BitConverter.ToInt32(byteLength, 0);
                    break;
                case VREncoding.ExplicitLong:
                    byteLength = dr.Skip(2).ReadBytes(4).Reverse().ToArray();
                    length = BitConverter.ToInt32(byteLength, 0);
                    break;
                case VREncoding.ExplicitShort:
                    byteLength = dr.ReadBytes(2).Reverse().ToArray();
                    length = BitConverter.ToUInt16(byteLength, 0);
                    break;
            }
            return length;
        }

        public static bool IsIndefinite(int length)
        {
            return length == -1;
        }

        /// <summary>
        ///     Reads the length in big endian byte format from a series of bytes in a stream
        /// </summary>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <param name="length">the number of bytes containing the length</param>
        /// <returns>the length</returns>
        public static int ReadBigEndian(DICOMBinaryReader dr, int length)
        {
            switch (length)
            {
                case 2:
                    return BitConverter.ToUInt16(dr.Take(2).Reverse().ToArray(), 0);
                case 4:
                    return BitConverter.ToInt32(dr.Take(4).Reverse().ToArray(), 0);
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Reads the length in big endian byte format from a series of bytes in a stream
        /// </summary>
        /// <param name="dr">the binary stream with a current position on the length parameter</param>
        /// <param name="length">the number of bytes containing the length</param>
        /// <returns>the length</returns>
        public static int ReadBigEndian(byte[] length)
        {
            switch (length.Length)
            {
                case 2:
                    return BitConverter.ToUInt16(length.Reverse().ToArray(), 0);
                case 4:
                    return BitConverter.ToInt32(length.Reverse().ToArray(), 0);
                default:
                    return 0;
            }
        }
    }
}