#region

using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the SignedLong VR type
    /// </summary>
    public class SignedLong : AbstractElement<int>
    {
        public SignedLong()
        {
            VR = VR.SignedLong;
        }

        public SignedLong(Tag tag, int data)
            : base(tag, data)
        {
            VR = VR.SignedLong;
        }

        public SignedLong(Tag tag, int[] data)
            : base(tag, data)
        {
            VR = VR.SignedLong;
        }
    }
}