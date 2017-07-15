using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the UnsignedLong VR type
    /// </summary>
    public class UnsignedLong : AbstractElement<uint>
    {
        public UnsignedLong()
        {
            VR = VR.UnsignedLong;
        }

        public UnsignedLong(Tag tag, uint data)
            : base(tag, data)
        {
            VR = VR.UnsignedLong;
        }

        public UnsignedLong(Tag tag, uint[] data)
            : base(tag, data)
        {
            VR = VR.UnsignedLong;
        }
    }
}