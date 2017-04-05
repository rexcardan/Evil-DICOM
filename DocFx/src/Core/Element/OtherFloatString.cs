using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the OtherFloatString VR type
    /// </summary>
    public class OtherFloatString : AbstractElement<byte>
    {
        public OtherFloatString()
        {
            VR = VR.OtherFloatString;
        }

        public OtherFloatString(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.OtherFloatString;
        }
    }
}