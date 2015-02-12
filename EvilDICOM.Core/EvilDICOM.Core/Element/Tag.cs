using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     A small helper class help work set and get the tag ids for DICOM elements.
    /// </summary>
    public class Tag
    {
        public Tag(string group, string element)
        {
            Group = DataRestriction.EnforceLengthRestriction(4, group);
            Element = DataRestriction.EnforceLengthRestriction(4, element);
        }

        public Tag(string completeID)
        {
            CompleteID = DataRestriction.EnforceLengthRestriction(8, completeID);
        }

        /// <summary>
        ///     The group id of the element
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///     The element id of the element
        /// </summary>
        public string Element { get; set; }

        /// <summary>
        ///     The complete id, containing both the group id GGGG and the element id EEEE as GGGGEEEE
        /// </summary>
        public string CompleteID
        {
            get { return Group + Element; }
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

        #region OPERATORS
        public override bool Equals(object obj)
        {
            var tag = obj as Tag;
            if ((object)tag != null)
            {
                return tag.CompleteID == this.CompleteID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.CompleteID.GetHashCode();
        }

        public static bool operator ==(Tag t1, Tag t2)
        {
            if ((object)t1 == null) return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tag t1, Tag t2)
        {
            if ((object)t1 == null) return false;
            return !t1.Equals(t2);
        }
        #endregion
    }
}
