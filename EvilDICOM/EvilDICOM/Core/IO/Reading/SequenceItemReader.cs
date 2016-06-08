using System.Collections.Generic;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    /// Class SequenceItemReader.
    /// </summary>
    public class SequenceItemReader
    {
        /// <summary>
        /// The _end of sequence item_ le
        /// </summary>
        private static readonly byte[] _endOfSequenceItem_LE = {0xFE, 0xFF, 0x0D, 0xE0, 0x00, 0x00, 0x00, 0x00};
        /// <summary>
        /// The _end of sequence item_ be
        /// </summary>
        private static readonly byte[] _endOfSequenceItem_BE = {0xFF, 0xFE, 0xE0, 0x0D, 0x00, 0x00, 0x00, 0x00};

        /// <summary>
        /// Reads the little endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
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

        /// <summary>
        /// Reads the big endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
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

        /// <summary>
        /// Skips the item little endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="syntax">The syntax.</param>
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

        /// <summary>
        /// Skips the item big endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
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

        /// <summary>
        /// Determines whether [is end of sequence item little endian] [the specified dr].
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if [is end of sequence item little endian] [the specified dr]; otherwise, <c>false</c>.</returns>
        private static bool IsEndOfSequenceItemLittleEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequenceItem_LE);
        }

        /// <summary>
        /// Determines whether [is end of sequence item big endian] [the specified dr].
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns><c>true</c> if [is end of sequence item big endian] [the specified dr]; otherwise, <c>false</c>.</returns>
        private static bool IsEndOfSequenceItemBigEndian(DICOMBinaryReader dr)
        {
            byte[] bytes = dr.ReadBytes(8);
            return ByteHelper.AreEqual(bytes, _endOfSequenceItem_BE);
        }

        /// <summary>
        /// Reads the indefinite big endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
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

        /// <summary>
        /// Reads the indefinite little endian.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
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