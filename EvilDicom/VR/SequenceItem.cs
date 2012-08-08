using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;
using EvilDicom.Helper;

namespace EvilDicom
{

    namespace VR
    {
        /// <summary>
        /// The Sequence Item is the object contained within the Sequence Class.
        /// Each Sequence Item holds a list of dicom objects, which can include a Sequence Object.
        /// </summary>
        public class SequenceItem : DICOMObject
        {
            protected Constants.LengthType lengthType = Constants.LengthType.FINITE;

            //Constants for Sequence Items
            private static Tag START_TAG = new Tag("FFFE", "E000");
            private static Tag END_TAG = new Tag("FFFE", "E00D");

            /// <summary>
            /// The Sequence Item does not have a VR as it is just a container to a 
            /// list of dicom objects. Additionally, the Encode Type is set to Implicit because
            /// the encoding is similar to Implicit encoding (eg. the length is written
            /// in bytes 4-7 for both length types)
            /// </summary>
            public SequenceItem()
            {
            }

            public static Tag StartTag { get { return START_TAG; } }

            public static Tag EndTag { get { return END_TAG; } }


            public Constants.LengthType LengthType
            {
                get { return lengthType; }
                set { lengthType = value; }
            }

            /// <summary>
            /// This length adds the tag to the length of all DICOM objects contained within the sequence item.
            /// </summary>
            public new int Length
            {
                get
                {
                    if (this.lengthType == Constants.LengthType.FINITE) { return base.Length + 8; }
                    else { return base.Length + 16; }
                }
            }

            public override void WriteBytes(System.IO.BinaryWriter b, bool isLittleEndian)
            {
                //Write Start Tag
                DICOMWriter.WriteTag(b, START_TAG, isLittleEndian);

                //Write Length
                if (lengthType == Constants.LengthType.FINITE)
                {
                    DICOMWriter.WriteLength(b, Constants.EncodeType.IMPLICIT, this.Length-8, isLittleEndian);
                }
                else
                {
                    DICOMWriter.WriteLength(b, Constants.INDEFINITE_LENGTH);
                }

                //Write Dicom Objects
                base.WriteBytes(b, isLittleEndian);

                //Write Delimeter for Indefinite Sequence Items
                if (lengthType == Constants.LengthType.INDEFINITE)
                {
                    DICOMWriter.WriteTag(b, END_TAG, isLittleEndian);
                    DICOMWriter.WriteLength(b, Constants.ZERO_LENGTH);
                }
            }



        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


