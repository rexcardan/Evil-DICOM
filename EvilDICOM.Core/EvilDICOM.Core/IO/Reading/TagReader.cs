using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.IO.Reading
{
    public class TagReader
    {
        public static Tag ReadLittleEndian(DICOMBinaryReader dr)
        {
            byte[] tag = dr.ReadBytes(4);
            tag = new byte[] { tag[1], tag[0], tag[3], tag[2] };
            return CreateTag(tag);
        }

        public static Tag ReadLittleEndian(byte[] tagLE)
        {  
            byte[] tag = new byte[] { tagLE[1], tagLE[0], tagLE[3], tagLE[2] };
            return CreateTag(tag);
        }

        public static Tag ReadBigEndian(DICOMBinaryReader dr)
        {
            byte[] tag = dr.ReadBytes(4);
            return CreateTag(tag);                  
        }

        public static Tag ReadBigEndian(byte[] tagBE)
        {
            return CreateTag(tagBE);
        }

        private static Tag CreateTag(byte[] tag)
        {
            string tagId = ByteHelper.ByteArrayToHexString(tag);
            return new Tag(tagId);
        }
    }
}
