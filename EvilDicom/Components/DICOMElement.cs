using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvilDicom
{
    namespace Components
    {
        /// <summary>
        /// The DICOMObject class contains all the necessary variables and methods of 
        /// a DICOM object including a Tag, VR (with an encoding type), Byte Data, and a
        /// boolean describing the byte encoding.
        /// </summary>
        public class DICOMElement
        {
         
            #region Constructor
            /// <summary>
            /// The constructor for the DICOM object
            /// </summary>
            public DICOMElement() { Tag = new Tag(); }
            #endregion

            #region Properties
            /// <summary>
            /// The two letter VR type in string format
            /// </summary>
            public string VR
            {
                get;
                set;
            }
            public virtual string[] DataAsStringArray() { return new string[] { string.Empty }; }
            /// <summary>
            /// A boolean that indicates whether or not the bytes are written in little or big endian.
            /// </summary>
            public bool IsLittleEndian
            {
                get;
                set;
            }

            /// <summary>
            /// An integer representing the length of the entire DICOM object which includes
            /// the byte data, length parameter, VR (sometimes explicitly), and Tag ID.
            /// </summary>
            public virtual int Length
            {
                get
                {
                    switch (EncodeType)
                    {
                        case Constants.EncodeType.IMPLICIT: return 8 + ByteData.Length;
                        case Constants.EncodeType.EXPLICIT_2: return 8 + ByteData.Length;
                        case Constants.EncodeType.EXPLICIT_4: return 12 + ByteData.Length;
                        default: return 8 + ByteData.Length;
                    }
                }
            }

            /// <summary>
            /// Describes the encoding type of the VR (Implicit, Explicit, Long Explicit)
            /// </summary>
            public Constants.EncodeType EncodeType
            {
                get;
                set;
            }

            /// <summary>
            /// The Tag which contains the id of this DICOM object
            /// </summary>
            public Tag Tag { get; set; }

            /// <summary>
            /// The raw byte data of the DICOM object. It is not very useful without the VR necessary to decode it.
            /// </summary>
            public byte[] ByteData
            {
                get;
                set;
            }

            /// <summary>
            /// A boolean which indicates if this DICOM object is a sequence.
            /// </summary>
            public bool IsSequence
            {
                get
                {
                    if (VR != null)
                    {
                        if (VR.Equals("SQ")) { return true; }
                    }
                    return false;
                }
            }

            #endregion

            /// <summary>
            /// A method which writes the bytes of this DICOM object in little or big endian encoding
            /// based on the isLittleEndian variable
            /// </summary>
            /// <param name="b">the Binary writer for writing the bytes</param>
            /// <param name="isLittleEndian"> A boolean that indicates whether or not the bytes are written in little or big endian.</param>
            public virtual void WriteBytes(BinaryWriter b, bool isLittleEndian)
            {
                Helper.DICOMWriter.WriteTag(b, this.Tag, isLittleEndian);
                Helper.DICOMWriter.WriteVR(b, this.VR, this.EncodeType);
                Helper.DICOMWriter.WriteLength(b, this.EncodeType, this.ByteData.Length, isLittleEndian);
                Helper.DICOMWriter.WriteData(b, this, isLittleEndian);
            }
            /// <summary>
            /// This method is used to check the endian of the
            /// Dicom object and arrange the byte data accordingly.
            /// This method is normally only called by children of the DICOMObject
            /// class and only when it is changing from one endianness to another.
            /// </summary>
            public virtual void Encode(bool isLittleEndian) { }

            //No children - return null
            /// <summary>
            /// This method is defined to be overridden by descendant classes such as
            /// the Sequence class. For a normal DICOM object with no internal DICOM Objects,
            /// this method returns null. 
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            internal virtual List<DICOMElement> find(string[] p)
            {
                return null;
            }
           
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


