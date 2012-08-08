using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom.Components
{
    /// <summary>
    /// The Constants class stores all of the Constant values (except for Tag IDs) that might need to be accessed from most DICOM programs.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// A four byte array containing the bytes representing indefinite length of a sequence or sequence item.
        /// </summary>
        public static byte[] INDEFINITE_LENGTH = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF};
        /// <summary>
        /// A four byte array containing the bytes representing zero length.
        /// </summary>
        public static byte[] ZERO_LENGTH = new byte[] { 0x00, 0x00, 0x00, 0x00 };

        public static byte[] SEQUENCE_END_DELIMITER = new byte[] { 0xFF, 0xFE, 0xE0, 0xDD };
        public static byte[] SEQUENCE_ITEM_END_DELIMITER = new byte[] { 0xFF, 0xFE, 0xE0, 0x0D };
        /// <summary>
        /// An enum to distiguish between finite and indefinite types of sequences or sequence items.
        /// </summary>
        public enum LengthType
        {
            FINITE, INDEFINITE
        }

        /// <summary>
        /// An enum to distinguish between the three types of Value Representation coding.
        /// Implicit = no VR specified
        /// Explicit_2 = VR specified, 2 byte length
        /// Explicit_4 = VR specified, 4 byte length
        /// </summary>
        public enum EncodeType
        {
            IMPLICIT, EXPLICIT_2, EXPLICIT_4
        }

        //TRANSFER SYNTAX
        /// <summary>
        /// Unique ID that represents an implicit VR with little endian encoding
        /// </summary>
        public const string IMPLICIT_VR_LITTLE_ENDIAN = "1.2.840.10008.1.2";
        /// <summary>
        /// Unique ID that represents an explicit VR with little endian encoding
        /// </summary>
        public const string EXPLICIT_VR_LITTLE_ENDIAN = "1.2.840.10008.1.2.1";
        /// <summary>
        /// Unique ID that represents an explicit VR with big endian encoding
        /// </summary>
        public const string EXPLICIT_VR_BIG_ENDIAN = "	1.2.840.10008.1.2.2";
        /// <summary>
        /// Unique ID that represents RLE lossless image encoding
        /// </summary>
        public const string RLE_LOSSLESS = "1.2.840.10008.1.2.5";
        /// <summary>
        /// Unique ID that represents JPEG baseline image encoding
        /// </summary>
        public  const string JPEG_BASELINE = "1.2.840.10008.1.2.4.50";
        /// <summary>
        /// Unique ID that represents JPEG Extended image encoding
        /// </summary>
        public  const string JPEG_EXTENDED = "1.2.840.10008.1.2.4.51";
        /// <summary>
        /// Unique ID that represents JPEG Progressive image encoding
        /// </summary>
        public  const string JPEG_PROGRESSIVE = "1.2.840.10008.1.2.4.55";
        /// <summary>
        /// Unique ID that represents JPEG lossless (Process 14) image encoding
        /// </summary>
        public  const string JPEG_LOSSLESS_14 = "1.2.840.10008.1.2.4.57";
        /// <summary>
        ///  Unique ID that represents JPEG lossless (Process 15) image encoding
        /// </summary>
        public  const string JPEG_LOSSLESS_15 = "1.2.840.10008.1.2.4.58";
        /// <summary>
        ///  Unique ID that represents JPEG lossless (Process 14 Selection Value 1) image encoding
        /// </summary>
        public  const string JPEG_LOSSLESS_14_S1 = "1.2.840.10008.1.2.4.70";
        /// <summary>
        ///  Unique ID that represents JPEG-LS lossless image encoding
        /// </summary>
        public  const string JPEG_LS_LOSSLESS = "1.2.840.10008.1.2.4.80";
        /// <summary>
        /// Unique ID that represents JPEG-LS near lossless image encoding
        /// </summary>
        public  const string JPEG_LS_NEAR_LOSSLESS = "1.2.840.10008.1.2.4.81";
        /// <summary>
        /// Unique ID that represents JPEG 2000 lossless image encoding
        /// </summary>
        public const string JPEG_2000_LOSSLESS = "1.2.840.10008.1.2.4.90";
        /// <summary>
        /// Unique ID that represents JPEG 2000 image encoding
        /// </summary>
        public  const string JPEG_2000 = "1.2.840.10008.1.2.4.91";

        /// <summary>
        /// An enum representing the transfer syntax of a given DICOM file.
        /// </summary>
        public enum TransferSyntax {
            IMPLICIT_VR_LITTLE_ENDIAN, EXPLICIT_VR_LITTLE_ENDIAN, EXPLICIT_VR_BIG_ENDIAN, RLE_LOSSLESS, JPEG_BASELINE, JPEG_EXTENDED, JPEG_PROGRESSIVE, JPEG_LOSSLESS_14,
            JPEG_LOSSLESS_15, JPEG_LOSSLESS_14_S1, JPEG_LS_LOSSLESS, JPEG_LS_NEAR_LOSSLESS, JPEG_2000_LOSSLESS, JPEG_2000
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


