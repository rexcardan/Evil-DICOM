using System;
using System.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Writing
{
    public class LengthWriter
    {
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

        public static void Write(DICOMBinaryWriter dw, VR vr, DICOMWriteSettings settings, int length)
        {
            var lengthBytes = new byte[0];
            if (!(settings.TransferSyntax == TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN))
            {
                switch (VRDictionary.GetEncodingFromVR(vr))
                {
                    case VREncoding.ExplicitLong:
                        dw.WriteNullBytes(2);
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                    case VREncoding.ExplicitShort:
                        lengthBytes = BitConverter.GetBytes((ushort) length);
                        break;
                    case VREncoding.Implicit:
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                }
            }
            else if (settings.TransferSyntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
                lengthBytes = BitConverter.GetBytes(length);
                lengthBytes.Reverse();
            }
            else
            {
                //Explicit VR Little Endian
                lengthBytes = BitConverter.GetBytes(length);
            }
            dw.Write(lengthBytes);
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, VR vr, int length)
        {
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, int length, int numberOfBytes)
        {
            var lengthBytes = new byte[0];
            switch (numberOfBytes)
            {
                case 2:
                    lengthBytes = BitConverter.GetBytes((ushort) length).Reverse().ToArray();
                    break;
                case 4:
                    lengthBytes = BitConverter.GetBytes(length).Reverse().ToArray();
                    break;
            }
            dw.Write(lengthBytes);
        }
    }
}