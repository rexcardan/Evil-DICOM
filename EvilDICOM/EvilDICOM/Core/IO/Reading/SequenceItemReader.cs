using System.Collections.Generic;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Reading
{
    public class SequenceItemReader
    {
        private static readonly byte[] _endOfSequenceItem_LE = {0xFE, 0xFF, 0x0D, 0xE0, 0x00, 0x00, 0x00, 0x00};
        private static readonly byte[] _endOfSequenceItem_BE = {0xFF, 0xFE, 0xE0, 0x0D, 0x00, 0x00, 0x00, 0x00};

        public static DICOMObject ReadLittleEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            DICOMObject d;
            int length = LengthReader.ReadLittleEndian(VR.Null, dr.Skip(4));
            if (LengthReader.IsIndefinite(length))
            {
                d = ReadIndefiniteLittleEndian(dr, syntax);
            }
            else
            {
                d = DICOMObjectReader.ReadObject(dr.ReadBytes(length), syntax);
            }

            return d;
        }

        public static DICOMObject ReadBigEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            DICOMObject d;
            int length = LengthReader.ReadLittleEndian(VR.Null, dr.Skip(4));
            if (LengthReader.IsIndefinite(length))
            {
                d = ReadIndefiniteBigEndian(dr, syntax);
            }
            else
            {
                d = DICOMObjectReader.ReadObject(dr.ReadBytes(length), syntax);
            }

            return d;
        }

        public static void SkipItemLittleEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            int length = LengthReader.ReadLittleEndian(VR.Null, dr.Skip(4));
            if (length != -1)
            {
                dr.Skip(length);
            }
            else
            {
                if (syntax == TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN)
                {
                    while (!IsEndOfSequenceItemLittleEndian(dr))
                    {
                        dr.StreamPosition -= 8;
                        DICOMElementReader.SkipElementExplicitLittleEndian(dr);
                    }
                }
                else
                {
                    while (!IsEndOfSequenceItemLittleEndian(dr))
                    {
                        dr.StreamPosition -= 8;
                        DICOMElementReader.SkipElementImplicitLittleEndian(dr);
                    }
                }
            }
        }

        public static void SkipItemBigEndian(DICOMBinaryReader dr)
        {
            int length = LengthReader.ReadBigEndian(VR.Null, dr.Skip(4));
            if (length != -1)
            {
                dr.Skip(length);
            }
            else
            {
                while (!IsEndOfSequenceItemBigEndian(dr))
                {
                    dr.StreamPosition -= 8;
                    DICOMElementReader.SkipElementExplicitBigEndian(dr);
                }
            }
        }

        private static bool IsEndOfSequenceItemLittleEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequenceItem_LE);
        }

        private static bool IsEndOfSequenceItemBigEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequenceItem_BE);
        }

        private static DICOMObject ReadIndefiniteBigEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            var elements = new List<IDICOMElement>();
            while (!IsEndOfSequenceItemLittleEndian(dr))
            {
                dr.StreamPosition -= 8;
                elements.Add(DICOMElementReader.ReadElementExplicitBigEndian(dr));
            }
            return new DICOMObject(elements);
        }

        private static DICOMObject ReadIndefiniteLittleEndian(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            var elements = new List<IDICOMElement>();
            while (!IsEndOfSequenceItemLittleEndian(dr))
            {
                dr.StreamPosition -= 8;
                if (syntax == TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN)
                {
                    elements.Add(DICOMElementReader.ReadElementExplicitLittleEndian(dr));
                }
                else
                {
                    elements.Add(DICOMElementReader.ReadElementImplicitLittleEndian(dr));
                }
            }
            return new DICOMObject(elements);
        }
    }
}