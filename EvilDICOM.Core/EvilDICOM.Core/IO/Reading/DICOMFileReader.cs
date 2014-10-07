using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using System.IO;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Reading
{
    /// <summary>
    /// Class for reading DICOM files
    /// </summary>
    public class DICOMFileReader
    {
        private const string _MetaGroup = "0002";

        /// <summary>
        /// Reads a DICOM file from a path
        /// </summary>
        /// <param name="filePath">the path to the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static DICOMObject Read(string filePath)
        {
            TransferSyntax syntax;
            List<IDICOMElement> elements;
            using (DICOMBinaryReader dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                List<IDICOMElement> metaElements = ReadFileMetadata(dr, out syntax);
                elements = metaElements.Union(DICOMElementReader.ReadAllElements(dr, syntax)).ToList();
            }
            return new DICOMObject(elements);
        }

        /// <summary>
        /// Reads a DICOM file from a byte array
        /// </summary>
        /// <param name="fileBytes">the bytes of the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static DICOMObject Read(byte[] fileBytes)
        {
            TransferSyntax syntax;
            List<IDICOMElement> elements;
            using (DICOMBinaryReader dr = new DICOMBinaryReader(fileBytes))
            {
                DICOMPreambleReader.Read(dr);
                List<IDICOMElement> metaElements = ReadFileMetadata(dr, out syntax);
                elements = metaElements.Union(DICOMElementReader.ReadAllElements(dr, syntax)).ToList();
            }
            return new DICOMObject(elements);
        }


        /// <summary>
        /// Read the meta data from the DICOM object
        /// </summary>
        /// <param name="filePath">the path to the DICOM file</param>
        /// <returns>a DICOM object containing the metadata elements</returns>
        public static DICOMObject ReadFileMetadata(string filePath)
        {
            TransferSyntax syntax;
            List<IDICOMElement> metaElements;
            using (DICOMBinaryReader dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                 metaElements= ReadFileMetadata(dr, out syntax);
            }
            return new DICOMObject(metaElements);
        }

        /// <summary>
        /// Read explicit VR little endian up to transfer syntax element and determines transfer syntax for rest of elements
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <param name="syntax">the transfer syntax of the DICOM file</param>
        /// <returns>elements preceeding and including transfer syntax element</returns>
        public static List<IDICOMElement> ReadFileMetadata(DICOMBinaryReader dr, out TransferSyntax syntax)
        {
            List<IDICOMElement> elements = new List<IDICOMElement>();
            syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;

            while (dr.StreamPosition < dr.StreamLength)
            {
                long position = dr.StreamPosition;
                if (TagReader.ReadLittleEndian(dr).Group == _MetaGroup)
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
