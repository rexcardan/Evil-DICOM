using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the OtherByteString VR type
    /// </summary>
    public class OtherByteString : AbstractElement<byte>
    {
        public OtherByteString()
        {
            VR = VR.OtherByteString;
        }

        public OtherByteString(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.OtherByteString;
        }
    }
}