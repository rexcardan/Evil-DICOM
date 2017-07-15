using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the SignedShort VR type
    /// </summary>
    public class SignedShort : AbstractElement<short>
    {
        public SignedShort()
        {
            VR = VR.SignedShort;
        }

        public SignedShort(Tag tag, short data)
            : base(tag, data)
        {
            VR = VR.SignedShort;
        }

        public SignedShort(Tag tag, short[] data)
            : base(tag, data)
        {
            VR = VR.SignedShort;
        }
    }
}