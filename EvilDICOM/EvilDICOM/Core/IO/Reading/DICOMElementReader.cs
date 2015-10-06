using System.Collections.Generic;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     Reads in DICOM elements from a DICOM object
    /// </summary>
    public class DICOMElementReader
    {
        /// <summary>
        ///     Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementExplicitLittleEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadLittleEndian(dr);
            VR vr = VRReader.Read(dr);
            int length = LengthReader.ReadLittleEndian(vr, dr);
            byte[] data = DataReader.ReadLittleEndian(length, dr, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN);
            return ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN);
        }

        /// <summary>
        ///     Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementImplicitLittleEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadLittleEndian(dr);
            VR vr = TagDictionary.GetVRFromTag(tag);
            int length = LengthReader.ReadLittleEndian(VR.Null, dr);
            byte[] data = DataReader.ReadLittleEndian(length, dr, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN);
            IDICOMElement el = ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN);
            return el;
        }

        /// <summary>
        ///     Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementExplicitBigEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadBigEndian(dr);
            VR vr = VRReader.Read(dr);
            int length = LengthReader.ReadBigEndian(vr, dr);
            byte[] data = DataReader.ReadBigEndian(length, dr);
            return ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.EXPLICIT_VR_BIG_ENDIAN);
        }

        #region SKIPPERS

        public static void SkipElementExplicitLittleEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadLittleEndian(dr);
            VR vr = VRReader.Read(dr);
            int length = LengthReader.ReadLittleEndian(vr, dr);
            if (length != -1)
            {
                dr.Skip(length);
            }
            else
            {
                dr.Skip(SequenceReader.ReadIndefiniteLengthLittleEndian(dr, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN));
                dr.Skip(8);
            }
        }

        public static void SkipElementImplicitLittleEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadLittleEndian(dr);
            int length = LengthReader.ReadLittleEndian(VR.Null, dr);
            if (length != -1)
            {
                dr.Skip(length);
            }
            else
            {
                dr.Skip(SequenceReader.ReadIndefiniteLengthLittleEndian(dr, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN));
                dr.Skip(8);
            }
        }

        public static void SkipElementExplicitBigEndian(DICOMBinaryReader dr)
        {
            Tag tag = TagReader.ReadBigEndian(dr);
            VR vr = VRReader.Read(dr);
            int length = LengthReader.ReadBigEndian(vr, dr);
            if (length != -1)
            {
                dr.Skip(length);
            }
            else
            {
                dr.Skip(SequenceReader.ReadIndefiniteLengthBigEndian(dr));
                dr.Skip(8);
            }
        }

        #endregion

        #region READ ALL ELEMENT METHODS

        public static List<IDICOMElement> ReadAllElements(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            List<IDICOMElement> elements;
            switch (syntax)
            {
                case TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN:
                    elements = ReadAllElementsImplicitLittleEndian(dr);
                    break;
                case TransferSyntax.EXPLICIT_VR_BIG_ENDIAN:
                    elements = ReadAllElementsExplicitBigEndian(dr);
                    break;
                default:
                    elements = ReadAllElementsExplicitLittleEndian(dr);
                    break;
            }
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in implicit little endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsImplicitLittleEndian(DICOMBinaryReader dr)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
            {
                elements.Add(ReadElementImplicitLittleEndian(dr));
            }
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in explicit big endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsExplicitBigEndian(DICOMBinaryReader dr)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
            {
                elements.Add(ReadElementExplicitBigEndian(dr));
            }
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in explilcit little endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsExplicitLittleEndian(DICOMBinaryReader dr)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
            {
                elements.Add(ReadElementExplicitLittleEndian(dr));
            }
            return elements;
        }

        #endregion
    }
}