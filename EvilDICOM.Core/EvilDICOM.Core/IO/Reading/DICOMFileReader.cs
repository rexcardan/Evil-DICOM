using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    ///     Class for reading DICOM files
    /// </summary>
    public class DICOMFileReader
    {
        private const string _metaGroup = "0002";

        /// <summary>
        ///     Reads a DICOM file from a path
        /// </summary>
        /// <param name="filePath">the path to the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static DICOMObject Read(string filePath, TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            TransferSyntax syntax = trySyntax;
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                List<IDICOMElement> metaElements = ReadFileMetadata(dr, ref syntax);
                elements = metaElements.Concat(DICOMElementReader.ReadAllElements(dr, syntax)).ToList();
            }
            return new DICOMObject(elements);
        }

        /// <summary>
        ///     Reads a DICOM file from a byte array
        /// </summary>
        /// <param name="fileBytes">the bytes of the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static DICOMObject Read(byte[] fileBytes, TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            TransferSyntax syntax = trySyntax; //Will keep if metadata doesn't exist
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(fileBytes))
            {
                DICOMPreambleReader.Read(dr);
                List<IDICOMElement> metaElements = ReadFileMetadata(dr, ref syntax);
                elements = metaElements.Concat(DICOMElementReader.ReadAllElements(dr, syntax)).ToList();
            }
            return new DICOMObject(elements);
        }


        /// <summary>
        ///     Read the meta data from the DICOM object
        /// </summary>
        /// <param name="filePath">the path to the DICOM file</param>
        /// <returns>a DICOM object containing the metadata elements</returns>
        public static DICOMObject ReadFileMetadata(string filePath)
        {
            var syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
            List<IDICOMElement> metaElements;
            using (var dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                metaElements = ReadFileMetadata(dr, ref syntax);
            }
            return new DICOMObject(metaElements);
        }

        /// <summary>
        ///     Read explicit VR little endian up to transfer syntax element and determines transfer syntax for rest of elements
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <param name="syntax">the transfer syntax of the DICOM file</param>
        /// <returns>elements preceeding and including transfer syntax element</returns>
        public static List<IDICOMElement> ReadFileMetadata(DICOMBinaryReader dr, ref TransferSyntax syntax)
        {
            var elements = new List<IDICOMElement>();
            syntax = syntax != TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN ? syntax : TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;

            while (dr.StreamPosition < dr.StreamLength)
            {
                long position = dr.StreamPosition;
                if (TagReader.ReadLittleEndian(dr).Group == _metaGroup)
                {
                    dr.StreamPosition = position;
                    IDICOMElement el = DICOMElementReader.ReadElementExplicitLittleEndian(dr);
                    if (el.Tag.CompleteID == TagHelper.TRANSFER_SYNTAX_UID.CompleteID)
                    {
                        syntax = TransferSyntaxHelper.GetSyntax(el);
                    }
                    elements.Add(el);
                }
                else
                {
                    dr.StreamPosition = position;
                    break;
                }
            }

            return elements;
        }
    }
}