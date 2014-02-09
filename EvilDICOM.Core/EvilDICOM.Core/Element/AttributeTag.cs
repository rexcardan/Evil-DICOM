using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Dictionaries;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the AttributeTag VR type
    /// </summary>
    public class AttributeTag : AbstractElement<Tag>
    {
        public AttributeTag() : base() { VR = Enums.VR.AttributeTag; }

        public AttributeTag(Tag tag, Tag data)
            : base(tag, data)
        {
            VR = Enums.VR.AttributeTag;
        }
    }

    /// <summary>
    /// A small helper class help work set and get the tag ids for DICOM elements.
    /// </summary>
    public class Tag
    {
        public Tag(string group, string element)
        {
            this.Group = DataRestriction.EnforceLengthRestriction(4, group);
            this.Element = DataRestriction.EnforceLengthRestriction(4, element);
        }

        public Tag(string completeID)
        {
            this.CompleteID = DataRestriction.EnforceLengthRestriction(8, completeID);
        }

        /// <summary>
        /// The group id of the element
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// The element id of the element
        /// </summary>
        public string Element { get; set; }

        /// <summary>
        /// The complete id, containing both the group id GGGG and the element id EEEE as GGGGEEEE
        /// </summary>
        public string CompleteID
        {
            get
            {
                return Group + Element;
            }
            set
            {
                string completeID = DataRestriction.EnforceLengthRestriction(8, value);
                Group = completeID.Substring(0, 4);
                Element = completeID.Substring(4, 4);
            }
        }

        public override string ToString()
        {
            return string.Format("({0},{1}) : {2}", Group, Element, TagDictionary.GetDescription(this));
        }
    }
}
