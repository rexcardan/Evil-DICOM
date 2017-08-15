#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

#endregion

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
        public static DICOMObject Read(string filePath,
            TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            var enc = StringEncoding.ISO_IR_192;
            var syntax = trySyntax;
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                var metaElements = ReadFileMetadata(dr, ref syntax, ref enc);
                elements = metaElements.Concat(DICOMElementReader.ReadAllElements(dr, syntax, enc)).ToList();
            }
            return new DICOMObject(elements);
        }


        /// <summary>
        ///     Asynchronously reads a DICOM file from a path
        /// </summary>
        /// <param name="filePath">the path to the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static async Task<DICOMObject> ReadAsync(string filePath,
            TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            return await Task.Run(() =>
            {
                var enc = StringEncoding.ISO_IR_192;
                var syntax = trySyntax;
                List<IDICOMElement> elements;
                using (var dr = new DICOMBinaryReader(filePath))
                {
                    DICOMPreambleReader.Read(dr);
                    var metaElements = ReadFileMetadata(dr, ref syntax, ref enc);
                    elements = metaElements.Concat(DICOMElementReader.ReadAllElements(dr, syntax, enc)).ToList();
                }
                return new DICOMObject(elements);
            });
        }

        /// <summary>
        ///     Reads a DICOM file from a byte array
        /// </summary>
        /// <param name="fileBytes">the bytes of the DICOM file</param>
        /// <returns>a DICOM object containing all elements</returns>
        public static DICOMObject Read(byte[] fileBytes,
            TransferSyntax trySyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            var syntax = trySyntax; //Will keep if metadata doesn't exist
            var enc = StringEncoding.ISO_IR_192;
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(fileBytes))
            {
                DICOMPreambleReader.Read(dr);
                var metaElements = ReadFileMetadata(dr, ref syntax, ref enc);
                elements = metaElements.Concat(DICOMElementReader.ReadAllElements(dr, syntax, enc)).ToList();
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
            var enc = StringEncoding.ISO_IR_192;
            var syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
            List<IDICOMElement> metaElements;
            using (var dr = new DICOMBinaryReader(filePath))
            {
                DICOMPreambleReader.Read(dr);
                metaElements = ReadFileMetadata(dr, ref syntax, ref enc);
            }
            return new DICOMObject(metaElements);
        }

        /// <summary>
        ///     Read the meta data from the DICOM object
        /// </summary>
        /// <param name="filePath">the bytes of the DICOM file</param>
        /// <returns>a DICOM object containing the metadata elements</returns>
        public static DICOMObject ReadFileMetadata(byte[] fileBytes)
        {
            var enc = StringEncoding.ISO_IR_192;
            var syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
            List<IDICOMElement> metaElements;
            using (var dr = new DICOMBinaryReader(fileBytes))
            {
                DICOMPreambleReader.Read(dr);
                metaElements = ReadFileMetadata(dr, ref syntax, ref enc);
            }
            return new DICOMObject(metaElements);
        }

        /// <summary>
        ///     Read explicit VR little endian up to transfer syntax element and determines transfer syntax for rest of elements
        /// </summary>
        /// <param name="dr">the binary reader which is reading the DICOM object</param>
        /// <param name="syntax">the transfer syntax of the DICOM file</param>
        /// <returns>elements preceeding and including transfer syntax element</returns>
        public static List<IDICOMElement> ReadFileMetadata(DICOMBinaryReader dr, ref TransferSyntax syntax, ref StringEncoding enc)
        {
            var elements = new List<IDICOMElement>();

            while (dr.StreamPosition < dr.StreamLength)
            {
                var position = dr.StreamPosition;
                if (TagReader.ReadLittleEndian(dr).Group == _metaGroup)
                {
                    dr.StreamPosition = position;
                    var el = DICOMElementReader.ReadElementExplicitLittleEndian(dr, enc);
                    if (el.Tag == TagHelper.TransferSyntaxUID)
                        syntax = TransferSyntaxHelper.GetSyntax(el);
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