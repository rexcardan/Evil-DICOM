using System.Collections.Generic;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Reading
{
    public class SequenceReader
    {
        private static readonly byte[] _endOfSequence_LE = {0xFE, 0xFF, 0xDD, 0xE0, 0x00, 0x00, 0x00, 0x00};
        private static readonly byte[] _endOfSequence_BE = {0xFF, 0xFE, 0xE0, 0xDD, 0x00, 0x00, 0x00, 0x00};

        public static int ReadIndefiniteLengthLittleEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            long startingPos = dr.StreamPosition;
            while (!IsEndOfSequenceLittleEndian(dr))
            {
                dr.StreamPosition -= 8;
                SequenceItemReader.SkipItemLittleEndian(dr, syntax);
            }
            return CalculateLength(dr, startingPos) - 8;
        }

        public static int ReadIndefiniteLengthBigEndian(DICOMBinaryReader dr)
        {
            long startingPos = dr.StreamPosition;
            while (!IsEndOfSequenceBigEndian(dr))
            {
                dr.StreamPosition -= 8;
                SequenceItemReader.SkipItemBigEndian(dr);
            }
            return CalculateLength(dr, startingPos) - 8;
        }

        private static bool IsEndOfSequenceLittleEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequence_LE);
        }

        private static bool IsEndOfSequenceBigEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequence_BE);
        }

        private static int CalculateLength(DICOMBinaryReader dr, long startingPos)
        {
            var length = (int) (dr.StreamPosition - startingPos);
            dr.StreamPosition = startingPos;
            return length;
        }

        public static List<DICOMObject> ReadItems(byte[] data, TransferSyntax syntax)
        {
            var objects = new List<DICOMObject>();
            using (var dr = new DICOMBinaryReader(data))
            {
                while (dr.StreamPosition < dr.StreamLength)
                {
                    switch (syntax)
                    {
                        case TransferSyntax.EXPLICIT_VR_BIG_ENDIAN:
                            objects.Add(SequenceItemReader.ReadBigEndian(dr, syntax));
                            break;
                        default:
                            objects.Add(SequenceItemReader.ReadLittleEndian(dr, syntax));
                            break;
                    }
                }
            }
            return objects;
        }
    }
}