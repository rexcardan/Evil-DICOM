using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the OtherWordString VR type
    /// </summary>
    public class OtherWordString : AbstractElement<byte>
    {
        public OtherWordString()
        {
            VR = VR.OtherWordString;
        }

        public OtherWordString(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.OtherWordString;
        }
    }
}