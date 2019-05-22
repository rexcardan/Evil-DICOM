#region

using System;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     A small helper class help work set and get the tag ids for DICOM elements.
    /// </summary>
    public class Tag
    {
        public Tag(string group, string element)
        {
            var grp = DataRestriction.EnforceLengthRestriction(4, group);
            var el = DataRestriction.EnforceLengthRestriction(4, element);
            CompleteID = string.Join(string.Empty, new[] { grp, el });
        }

        public Tag(string completeID)
        {
            CompleteID = completeID;
        }

        /// <summary>
        ///     The group id of the element
        /// </summary>
        public string Group { get { return CompleteID.Substring(0, 4); } }

        /// <summary>
        ///     The element id of the element
        /// </summary>
        public string Element { get { return CompleteID.Substring(4, 4); } }

        private string _completeId;
        /// <summary>
        ///     The complete id, containing both the group id GGGG and the element id EEEE as GGGGEEEE
        /// </summary>
        public string CompleteID
        {
            get { return _completeId; }
            set
            {
                var completeID = DataRestriction.EnforceLengthRestriction(8, value);
                _completeId = value;
            }
        }

        public override string ToString()
        {
            return string.Format("({0},{1}) : {2}", Group, Element, TagDictionary.GetDescription(this));
        }

        public bool IsPrivate()
        {
            var groupBytes = ByteHelper.HexStringToByteArray(Group);
            Array.Reverse(groupBytes);
            var val = BitConverter.ToInt16(groupBytes, 0);
            return val % 2 != 0; //Odd group
        }

        #region OPERATORS

        public override bool Equals(object obj)
        {
            var tag = obj as Tag;
            if ((object) tag != null)
                return tag.CompleteID == CompleteID;
            return false;
        }

        public override int GetHashCode()
        {
            return CompleteID.GetHashCode();
        }

        public static bool operator ==(Tag t1, Tag t2)
        {
            if ((object) t1 == null) return false;
            return t1.Equals(t2);
        }

        public static bool operator !=(Tag t1, Tag t2)
        {
            if ((object) t1 == null) return false;
            return !t1.Equals(t2);
        }

        #endregion
    }
}