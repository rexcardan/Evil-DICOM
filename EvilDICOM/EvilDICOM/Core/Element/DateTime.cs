using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the DateTime VR type
    /// </summary>
    public class DateTime : AbstractElement<System.DateTime?>
    {
        public DateTime()
        {
            VR = VR.DateTime;
        }

        public DateTime(Tag tag, string data)
            : base(tag, StringDataParser.ParseDateTime(data))
        {
            VR = VR.DateTime;
        }

        public DateTime(Tag tag, System.DateTime? data)
            : base(tag, data)
        {
            VR = VR.DateTime;
        }
    }
}