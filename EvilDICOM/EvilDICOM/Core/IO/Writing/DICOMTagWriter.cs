using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Writing
{
    /// <summary>
    /// Class DICOMTagWriter.
    /// </summary>
    public class DICOMTagWriter
    {
        /// <summary>
        /// Writes the little endian.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteLittleEndian(Tag tag)
        {
            byte[] tagBytes = ByteHelper.HexStringToByteArray(tag.CompleteID);
            tagBytes = new[] {tagBytes[1], tagBytes[0], tagBytes[3], tagBytes[2]};
            return tagBytes;
        }

        /// <summary>
        /// Writes the little endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="tag">The tag.</param>
        public static void WriteLittleEndian(DICOMBinaryWriter dw, Tag tag)
        {
            byte[] tagBytes = WriteLittleEndian(tag);
            dw.Write(tagBytes);
        }

        /// <summary>
        /// Writes the big endian.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteBigEndian(Tag tag)
        {
            return ByteHelper.HexStringToByteArray(tag.CompleteID);
        }

        /// <summary>
        /// Writes the big endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="tag">The tag.</param>
        public static void WriteBigEndian(DICOMBinaryWriter dw, Tag tag)
        {
            byte[] tagBytes = WriteBigEndian(tag);
            dw.Write(tagBytes);
        }
    }
}