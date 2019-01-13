#region

using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.Helpers
{
    public static class TransferSyntaxHelper
    {
        //TRANSFER SYNTAX
        /// <summary>
        ///     Unique ID that represents an implicit VR with little endian encoding
        /// </summary>
        public const string IMPLICIT_VR_LITTLE_ENDIAN = "1.2.840.10008.1.2";

        /// <summary>
        ///     Unique ID that represents an explicit VR with little endian encoding
        /// </summary>
        public const string EXPLICIT_VR_LITTLE_ENDIAN = "1.2.840.10008.1.2.1";

        /// <summary>
        ///     Unique ID that represents an explicit VR with big endian encoding
        /// </summary>
        public const string EXPLICIT_VR_BIG_ENDIAN = "1.2.840.10008.1.2.2";

        /// <summary>
        ///     Unique ID that represents RLE lossless image encoding
        /// </summary>
        public const string RLE_LOSSLESS = "1.2.840.10008.1.2.5";

        /// <summary>
        ///     Unique ID that represents JPEG baseline image encoding
        /// </summary>
        public const string JPEG_BASELINE = "1.2.840.10008.1.2.4.50";

        /// <summary>
        ///     Unique ID that represents JPEG Extended image encoding
        /// </summary>
        public const string JPEG_EXTENDED = "1.2.840.10008.1.2.4.51";

        /// <summary>
        ///     Unique ID that represents JPEG Progressive image encoding
        /// </summary>
        public const string JPEG_PROGRESSIVE = "1.2.840.10008.1.2.4.55";

        /// <summary>
        ///     Unique ID that represents JPEG lossless (Process 14) image encoding
        /// </summary>
        public const string JPEG_LOSSLESS_14 = "1.2.840.10008.1.2.4.57";

        /// <summary>
        ///     Unique ID that represents JPEG lossless (Process 15) image encoding
        /// </summary>
        public const string JPEG_LOSSLESS_15 = "1.2.840.10008.1.2.4.58";

        /// <summary>
        ///     Unique ID that represents JPEG lossless (Process 14 Selection Value 1) image encoding
        /// </summary>
        public const string JPEG_LOSSLESS_14_S1 = "1.2.840.10008.1.2.4.70";

        /// <summary>
        ///     Unique ID that represents JPEG-LS lossless image encoding
        /// </summary>
        public const string JPEG_LS_LOSSLESS = "1.2.840.10008.1.2.4.80";

        /// <summary>
        ///     Unique ID that represents JPEG-LS near lossless image encoding
        /// </summary>
        public const string JPEG_LS_NEAR_LOSSLESS = "1.2.840.10008.1.2.4.81";

        /// <summary>
        ///     Unique ID that represents JPEG 2000 lossless image encoding
        /// </summary>
        public const string JPEG_2000_LOSSLESS = "1.2.840.10008.1.2.4.90";

        /// <summary>
        ///     Unique ID that represents JPEG 2000 image encoding
        /// </summary>
        public const string JPEG_2000 = "1.2.840.10008.1.2.4.91";

        /// <summary>
        ///     Converts the string in the Transfer syntax element to the transfer syntax enum for reading the file
        /// </summary>
        /// <param name="el">the transfer syntax element</param>
        /// <returns>the transfer syntax in enum format</returns>
        public static TransferSyntax GetSyntax(IDICOMElement el)
        {
            var tSyntax = el as UniqueIdentifier;
            return GetSyntax(tSyntax.Data);
        }

        /// <summary>
        /// Indicates if the pixel data is in compressed form
        /// </summary>
        /// <param name="syntax">the string of the transfer syntax UID</param>
        /// <returns>boolean indicating if image is compressed</returns>
        public static bool IsCompressedImage(string syntaxUIDString)
        {
            var syntax = GetSyntax(syntaxUIDString);
            return syntax != TransferSyntax.EXPLICIT_VR_BIG_ENDIAN &&
                   syntax != TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN &&
                   syntax != TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
        }

        /// <summary>
        ///     Converts the string to the transfer syntax enum for reading the file
        /// </summary>
        /// <param name="el">the transfer syntax element</param>
        /// <returns>the transfer syntax in enum format</returns>
        public static TransferSyntax GetSyntax(string syntaxUID)
        {
            switch (syntaxUID)
            {
                case IMPLICIT_VR_LITTLE_ENDIAN:
                    return TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
                case EXPLICIT_VR_LITTLE_ENDIAN:
                    return TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN;
                case EXPLICIT_VR_BIG_ENDIAN:
                    return TransferSyntax.EXPLICIT_VR_BIG_ENDIAN;
                case JPEG_2000:
                    return TransferSyntax.JPEG_2000;
                case JPEG_2000_LOSSLESS:
                    return TransferSyntax.JPEG_2000_LOSSLESS;
                case JPEG_BASELINE:
                    return TransferSyntax.JPEG_BASELINE;
                case JPEG_EXTENDED:
                    return TransferSyntax.JPEG_EXTENDED;
                case JPEG_LOSSLESS_14:
                    return TransferSyntax.JPEG_LOSSLESS_14;
                case JPEG_LOSSLESS_14_S1:
                    return TransferSyntax.JPEG_LOSSLESS_14_S1;
                case JPEG_LOSSLESS_15:
                    return TransferSyntax.JPEG_LOSSLESS_15;
                case JPEG_LS_LOSSLESS:
                    return TransferSyntax.JPEG_LS_LOSSLESS;
                case JPEG_LS_NEAR_LOSSLESS:
                    return TransferSyntax.JPEG_LS_NEAR_LOSSLESS;
                case JPEG_PROGRESSIVE:
                    return TransferSyntax.JPEG_PROGRESSIVE;
                case RLE_LOSSLESS:
                    return TransferSyntax.RLE_LOSSLESS;
                default:
                    return TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
            }
        }

        /// <summary>
        ///     Sets the transfer syntax of the DICOM object. The purpose of this is to go from an enum to a string.
        /// </summary>
        /// <param name="dicom">the DICOM object to set syntax</param>
        /// <param name="selector">the transfer syntax to set</param>
        public static void SetSyntax(DICOMObject dicom, TransferSyntax selector)
        {
            var syntax = DICOMForge.TransferSyntaxUID();
            if (syntax != null)
            {
                var transferSyntax = string.Empty;
                switch (selector)
                {
                    case TransferSyntax.EXPLICIT_VR_BIG_ENDIAN:
                        transferSyntax = EXPLICIT_VR_BIG_ENDIAN;
                        break;
                    case TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN:
                        transferSyntax = EXPLICIT_VR_LITTLE_ENDIAN;
                        break;
                    case TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN:
                        transferSyntax = IMPLICIT_VR_LITTLE_ENDIAN;
                        break;
                    case TransferSyntax.JPEG_2000:
                        transferSyntax = JPEG_2000;
                        break;
                    case TransferSyntax.JPEG_2000_LOSSLESS:
                        transferSyntax = JPEG_2000_LOSSLESS;
                        break;
                    case TransferSyntax.JPEG_BASELINE:
                        transferSyntax = JPEG_BASELINE;
                        break;
                    case TransferSyntax.JPEG_EXTENDED:
                        transferSyntax = JPEG_EXTENDED;
                        break;
                    case TransferSyntax.JPEG_LOSSLESS_14:
                        transferSyntax = JPEG_LOSSLESS_14;
                        break;
                    case TransferSyntax.JPEG_LOSSLESS_14_S1:
                        transferSyntax = JPEG_LOSSLESS_14_S1;
                        break;
                    case TransferSyntax.JPEG_LOSSLESS_15:
                        transferSyntax = JPEG_LOSSLESS_15;
                        break;
                    case TransferSyntax.JPEG_LS_LOSSLESS:
                        transferSyntax = JPEG_LS_LOSSLESS;
                        break;
                    case TransferSyntax.JPEG_LS_NEAR_LOSSLESS:
                        transferSyntax = JPEG_LS_NEAR_LOSSLESS;
                        break;
                    case TransferSyntax.JPEG_PROGRESSIVE:
                        transferSyntax = JPEG_PROGRESSIVE;
                        break;
                    case TransferSyntax.RLE_LOSSLESS:
                        transferSyntax = RLE_LOSSLESS;
                        break;
                    default:
                        transferSyntax = IMPLICIT_VR_LITTLE_ENDIAN;
                        break;
                }
                syntax.Data = transferSyntax;
                dicom.ReplaceOrAdd(syntax);
            }
        }
    }
}