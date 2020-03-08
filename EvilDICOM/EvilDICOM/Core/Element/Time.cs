#region

using Evil_DICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the Time VR type
    /// </summary>
    public class Time : RangeableDateTime
    {
        public Time()
        {
            VR = VR.Time;
        }

        public Time(Tag tag, string data)
            : base(tag, StringDataParser.ParseTime(data))
        {
            VR = VR.Time;
        }

        public Time(Tag tag, System.DateTime? time) : base(tag, time)
        {
            VR = VR.Time;
        }

        public Time(Tag tag, System.DateTime?[] times) : base(tag, times)
        {
            VR = VR.Time;
        }
    }
}