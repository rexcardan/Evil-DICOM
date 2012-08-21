using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Writing;

namespace EvilDICOM.Core.IO.Data
{
    public class LittleEndianWriter
    {
        public static byte[] WriteTag(Tag tag)
        {
            return DICOMTagWriter.WriteLittleEndian(tag);
        }

        public static byte[] WriteSinglePrecision(float? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((float)data);
            }
        }

        public static byte[] WriteDoublePrecision(double? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((double)data);
            }
        }

        public static byte[] WriteSignedLong(int? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((int)data);
            }
        }

        public static byte[] WriteUnsignedLong(uint? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((uint)data);
            }
        }

        public static byte[] WriteSignedShort(short? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((short)data);
            }
        }

        public static byte[] WriteUnsignedShort(ushort? data)
        {
            if (data == null)
            {
                return new byte[0];
            }
            else
            {
                return BitConverter.GetBytes((ushort)data);
            }
        }
    }
}
