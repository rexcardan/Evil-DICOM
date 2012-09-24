using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class AttributeTag : AbstractElement<Tag>
    {
        public AttributeTag() { }

        public AttributeTag(Tag tag, Tag data)
        {
            Tag = tag;
            Data = data;
        }
    }

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

        public string Group { get; set; }
        public string Element { get; set; }
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
    }
}
