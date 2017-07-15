using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMTagWriter
    {
        public static byte[] WriteLittleEndian(Tag tag)
        {
            byte[] tagBytes = ByteHelper.HexStringToByteArray(tag.CompleteID);
            tagBytes = new[] {tagBytes[1], tagBytes[0], tagBytes[3], tagBytes[2]};
            return tagBytes;
        }

        public static void WriteLittleEndian(DICOMBinaryWriter dw, Tag tag)
        {
            byte[] tagBytes = WriteLittleEndian(tag);
            dw.Write(tagBytes);
        }

        public static byte[] WriteBigEndian(Tag tag)
        {
            return ByteHelper.HexStringToByteArray(tag.CompleteID);
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, Tag tag)
        {
            byte[] tagBytes = WriteBigEndian(tag);
            dw.Write(tagBytes);
        }
    }
}