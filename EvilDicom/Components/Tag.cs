using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;

namespace EvilDicom
{
    namespace Components
    {
        /// <summary>
        /// The Tag class contains all of the properties and methods for working with DICOM tags.
        /// </summary>
        public class Tag
        {
            /// <summary>
            /// The group ID of the tag
            /// </summary>
            private String group;
            /// <summary>
            /// The element ID of the tag.
            /// </summary>
            private String element;
            /// <summary>
            /// The description of this tag as defined in the DICOM dictionary.
            /// </summary>
            private String description;

            /// <summary>
            /// The empty constructor for the Tag class
            /// </summary>
            public Tag() { }
            /// <summary>
            /// The main constructor for the Tag class which takes
            /// a group id and element id to create a Tag object.
            /// </summary>
            /// <param name="group">the string of the group id for this tag</param>
            /// <param name="element">the string of the element id for this tag</param>
            public Tag(string group, string element)
            {
                this.group = group;
                this.element = element;
            }

            public Tag(string id)
            {
                this.group = id.Substring(0, 4);
                this.element = id.Substring(4, 4);
            }

            /// <summary>
            /// A constructor which takes the raw bytes and converts them into a tag object, pulling
            /// out the group id and element id from the bytes.
            /// </summary>
            /// <param name="tagBytes">the raw (four) bytes for this tag</param>
            /// <param name="isLittleEndian"> A boolean that indicates whether or not the bytes are written in little or big endian.</param>
            public Tag(byte[] tagBytes, bool isLittleEndian)
            {
                if (isLittleEndian)
                {
                    this.group = ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[1] }) +
                        ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[0] });
                    this.element = ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[3] }) +
                        ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[2] });
                }
                else
                {
                    this.group = ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[0] }) +
                       ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[1] });
                    this.element = ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[2] }) +
                        ByteHelper.ByteArrayToHexString(new byte[] { tagBytes[3] });
                }
            }

            /// <summary>
            /// The group ID of the tag
            /// </summary>
            public string Group
            {
                set { if (value.Length == 4) { group = value; } }
                get { return group; }
            }

            /// <summary>
            /// The element ID of the tag
            /// </summary>
            public string Element
            {
                set { if (value.Length == 4) { element = value; } }
                get { return element; }
            }

            /// <summary>
            /// The total ID of this tag without a comma. For example the Tag (0002,0000) would be
            /// returned as 00020000.
            /// </summary>
            public string Id
            {
                get { return group + element; }
                set
                {
                    if (value.Length == 8)
                    {
                        group = value.Substring(0, 4);
                        element = value.Substring(4, 4);
                    }
                }
            }

            /// <summary>
            /// The description of this tag as defined in the DICOM dictionary.
            /// </summary>
            public string Description
            {
                set { description = value; }
                get { return description; }
            }

            /// <summary>
            /// This method returns the bytes of the tag
            /// </summary>
            /// <param name="isLittleEndian">Specifies whether or not the bytes should be in big or little endian format</param>
            /// <returns>a byte array of the tag</returns>
            public byte[] GetTagBytes(bool isLittleEndian)
            {
                byte[] bytes = Helper.ByteHelper.HexStringToByteArray(Id);
                if (isLittleEndian)
                {
                    bytes = new byte[] { bytes[1], bytes[0], bytes[3], bytes[2] };
                }
                return bytes;
            }

        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


