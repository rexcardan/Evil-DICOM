using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Writing
{
    public class LengthWriter
    {
        public static void WriteLittleEndian(DICOMBinaryWriter dw, int length, int numberOfBytes)
        {
            byte[] lengthBytes = new byte[0];
            switch (numberOfBytes)
            {
                case 2: lengthBytes = BitConverter.GetBytes((short)length);
                    break;
                case 4: lengthBytes = BitConverter.GetBytes(length);
                    break;
            }
            dw.Write(lengthBytes);
        }

        public static void WriteLittleEndian(DICOMBinaryWriter dw, VR vr, DICOMWriteSettings settings, int length)
        {
            byte[] lengthBytes = new byte[0];
            if (!(settings.TransferSyntax == TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN))
            {
                switch (VRDictionary.GetEncodingFromVR(vr))
                {
                    case VREncoding.ExplicitLong:
                        dw.WriteNullBytes(2);
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                    case VREncoding.ExplicitShort:
                        lengthBytes = BitConverter.GetBytes((short)length);
                        break;
                    case VREncoding.Implicit:
                        lengthBytes = BitConverter.GetBytes(length);
                        break;
                }
            }
            else
            {
                lengthBytes =BitConverter.GetBytes(length);
            }
            dw.Write(lengthBytes); 
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, VR vr, int length)
        {

        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, int length, int numberOfBytes)
        {
            byte[] lengthBytes = new byte[0];
            switch (numberOfBytes)
            {
                case 2: lengthBytes = BitConverter.GetBytes((short)length).Reverse().ToArray();
                    break;
                case 4: lengthBytes = BitConverter.GetBytes(length).Reverse().ToArray();
                    break;
            }
            dw.Write(lengthBytes);
        }
    }
}
