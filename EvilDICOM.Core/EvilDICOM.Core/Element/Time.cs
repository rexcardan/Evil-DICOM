using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the Time VR type
    /// </summary>
    public class Time : AbstractElement<System.DateTime?>
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
    }
}