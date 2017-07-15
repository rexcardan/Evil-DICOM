using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     Reads the byte data from a DICOM element
    /// </summary>
    public class DataReader
    {
        /// <summary>
        ///     Reads the data from an element encoded in little endian byte order
        /// </summary>
        /// <param name="lengthToRead">the length of the data</param>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the data from this element</returns>
        public static byte[] ReadLittleEndian(int lengthToRead, DICOMBinaryReader dr, TransferSyntax syntax)
        {
            if (lengthToRead != -1)
            {
                return dr.ReadBytes(lengthToRead);
            }
            int length = SequenceReader.ReadIndefiniteLengthLittleEndian(dr, syntax);
            byte[] seqBytes = dr.ReadBytes(length);
            dr.Skip(8);
            return seqBytes;
        }

        /// <summary>
        ///     Reads the data from an element encoded in big endian byte order
        /// </summary>
        /// <param name="lengthToRead">the length of the data</param>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the data from this element</returns>
        public static byte[] ReadBigEndian(int lengthToRead, DICOMBinaryReader dr)
        {
            if (lengthToRead != -1)
            {
                return dr.ReadBytes(lengthToRead);
            }
            int length = SequenceReader.ReadIndefiniteLengthBigEndian(dr);
            byte[] seqBytes = dr.ReadBytes(length);
            dr.Skip(8);
            return seqBytes;
        }
    }
}