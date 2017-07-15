using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the IntegerString VR type
    /// </summary>
    public class IntegerString : AbstractElement<int>
    {
        public IntegerString()
        {
            VR = VR.IntegerString;
        }

        public IntegerString(Tag tag, string data)
            : base(tag, StringDataParser.ParseIntegerString(data))
        {
            VR = VR.IntegerString;
        }

        public IntegerString(Tag tag, int data)
            : base(tag, data)
        {
            VR = VR.IntegerString;
        }

        public IntegerString(Tag tag, int[] data)
            : base(tag, data)
        {
            VR = VR.IntegerString;
        }
    }
}