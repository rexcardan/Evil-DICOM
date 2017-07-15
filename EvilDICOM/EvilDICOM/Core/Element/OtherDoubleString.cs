using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the OtherFloatString VR type
    /// </summary>
    public class OtherDoubleString : AbstractElement<byte>
    {
        public OtherDoubleString()
        {
            VR = VR.OtherDoubleString;
        }

        public OtherDoubleString(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.OtherDoubleString;
        }
    }
}