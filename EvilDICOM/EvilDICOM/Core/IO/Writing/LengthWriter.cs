using System;
using System.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOM.Core.IO.Writing
{
    /// <summary>
    /// Class LengthWriter.
    /// </summary>
    public class LengthWriter
    {
        /// <summary>
        /// Writes the little endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="length">The length.</param>
        /// <param name="numberOfBytes">The number of bytes.</param>
        public static void WriteLittleEndian(DICOMBinaryWriter dw, int length, int numberOfBytes)
        {
            var lengthBytes = new byte[0];
            switch (numberOfBytes)
            {
                case 2:
                    lengthBytes = BitConverter.GetBytes((short) length);
                    break;
                case 4:
                    lengthBytes = BitConverter.GetBytes(length);
                    break;
            }
            dw.Write(lengthBytes);
        }

        /// <summary>
        /// Writes the specified dw.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="vr">The vr.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="length">The length.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is greater than allowed for explicit VR syntax. Try using implicit VR</exception>
        public static void Write(DICOMBinaryWriter dw, VR vr, DICOMWriteSettings settings, int length)
        {
            if (length % 2 != 0 || length ==1198421)
            {
                Console.WriteLine("Length is odd!");
            }
            var lengthBytes = BitConverter.GetBytes(length);
            if (settings.TransferSyntax != TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
            {
                //Length byte size depends on VR Encoding
                switch (VRDictionary.GetEncodingFromVR(vr))
                {
                    case VREncoding.ExplicitLong:
                        dw.WriteNullBytes(2);
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                    case VREncoding.ExplicitShort:
                        lengthBytes = BitConverter.GetBytes((ushort) length);
                        if (length > 65536) { throw new ArgumentOutOfRangeException("Length is greater than allowed for explicit VR syntax. Try using implicit VR"); }
                        break;
                    case VREncoding.Implicit:
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                }
            }
            if (settings.TransferSyntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
                Array.Reverse(lengthBytes);
            }
            dw.Write(lengthBytes);
        }

        /// <summary>
        /// Writes the big endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="length">The length.</param>
        /// <param name="numberOfBytes">The number of bytes.</param>
        public static void WriteBigEndian(DICOMBinaryWriter dw, int length, int numberOfBytes)
        {
            byte[] lengthBytes=null;
            switch (numberOfBytes)
            {
                case 2:
                    lengthBytes = BitConverter.GetBytes((ushort) length);
                    break;
                case 4:
                    lengthBytes = BitConverter.GetBytes(length);
                    break;
            }
            Array.Reverse(lengthBytes);
            dw.Write(lengthBytes);
        }
    }
}