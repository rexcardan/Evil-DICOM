#region

using Evil_DICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the Date VR type
    /// </summary>
    public class Date : RangeableDateTime
    {
        public Date()
        {
            VR = VR.Date;
        }

        public Date(Tag tag, string data)
            : base(tag, StringDataParser.ParseDate(data))
        {
            VR = VR.Date;
        }

        public Date(Tag tag, System.DateTime? data)
            : base(tag, data)
        {
            VR = VR.Date;
        }

        public Date(Tag tag, System.DateTime?[] data)
            : base(tag, data)
        {
            VR = VR.Date;
        }
    }
}