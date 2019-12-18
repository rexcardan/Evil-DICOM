#region

using System.Collections.Generic;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     Reads in DICOM elements from a DICOM object
    /// </summary>
    public class DICOMElementReader
    {
        /// <summary>
        /// Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementExplicitLittleEndian(DICOMBinaryReader dr, StringEncoding enc = StringEncoding.ISO_IR_192)
        {
            var tag = TagReader.ReadLittleEndian(dr);
            var vr = VRReader.ReadVR(dr);
            return ReadElementExplicitLittleEndian(tag, vr, dr, enc);
        }

        /// <summary>
        /// Reads and returns the next DICOM element starting at the current location in the DICOM binary reader after the tag and VR have been read
        /// </summary>
        /// <param name="tag">the DICOM tag of the element</param>
        /// <param name="vr">the read VR of the element</param>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns></returns>
        private static IDICOMElement ReadElementExplicitLittleEndian(Tag tag, VR vr, DICOMBinaryReader dr, StringEncoding enc)
        {
            var length = LengthReader.ReadLittleEndian(vr, dr);
            var data = DataReader.ReadLittleEndian(length, dr, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN);
            return ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN, enc);
        }

        /// <summary>
        /// Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementImplicitLittleEndian(DICOMBinaryReader dr, StringEncoding enc = StringEncoding.ISO_IR_192)
        {
            var tag = TagReader.ReadLittleEndian(dr);
            var vr = TagDictionary.GetVRFromTag(tag);

            var length = LengthReader.ReadLittleEndian(VR.Null, dr);
            var data = DataReader.ReadLittleEndian(length, dr, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN);
            var el = ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN, enc);
            return el;
        }



        /// <summary>
        ///     Reads and returns the next DICOM element starting at the current location in the DICOM binary reader
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>the next DICOM element</returns>
        public static IDICOMElement ReadElementExplicitBigEndian(DICOMBinaryReader dr, StringEncoding enc)
        {
            var tag = TagReader.ReadBigEndian(dr);
            var vr = VRReader.ReadVR(dr);
            var length = LengthReader.ReadBigEndian(vr, dr);
            var data = DataReader.ReadBigEndian(length, dr);
            return ElementFactory.GenerateElement(tag, vr, data, TransferSyntax.EXPLICIT_VR_BIG_ENDIAN, enc);
        }

        #region SKIPPERS

        public static void SkipElementExplicitLittleEndian(DICOMBinaryReader dr)
        {
            var tag = TagReader.ReadLittleEndian(dr);
            var vr = VRReader.ReadVR(dr);
            var length = LengthReader.ReadLittleEndian(vr, dr);
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
            var tag = TagReader.ReadLittleEndian(dr);
            var length = LengthReader.ReadLittleEndian(VR.Null, dr);
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
            var tag = TagReader.ReadBigEndian(dr);
            var vr = VRReader.ReadVR(dr);
            var length = LengthReader.ReadBigEndian(vr, dr);
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

        public static List<IDICOMElement> ReadAllElements(DICOMBinaryReader dr, TransferSyntax syntax, StringEncoding enc)
        {
            List<IDICOMElement> elements;
            switch (syntax)
            {
                case TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN:
                    elements = ReadAllElementsImplicitLittleEndian(dr, enc);
                    break;
                case TransferSyntax.EXPLICIT_VR_BIG_ENDIAN:
                    elements = ReadAllElementsExplicitBigEndian(dr, enc);
                    break;
                default:
                    elements = ReadAllElementsExplicitLittleEndian(dr, enc);
                    break;
            }
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in implicit little endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsImplicitLittleEndian(DICOMBinaryReader dr, StringEncoding enc)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
                elements.Add(ReadElementImplicitLittleEndian(dr, enc));
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in explicit big endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsExplicitBigEndian(DICOMBinaryReader dr, StringEncoding enc)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
                elements.Add(ReadElementExplicitBigEndian(dr, enc));
            return elements;
        }

        /// <summary>
        ///     Reads and returns all elements in explilcit little endian format
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <returns>DICOM elements read</returns>
        public static List<IDICOMElement> ReadAllElementsExplicitLittleEndian(DICOMBinaryReader dr, StringEncoding enc)
        {
            var elements = new List<IDICOMElement>();
            while (dr.StreamPosition < dr.StreamLength)
                elements.Add(ReadElementExplicitLittleEndian(dr, enc));
            return elements;
        }

        #endregion
    }
}