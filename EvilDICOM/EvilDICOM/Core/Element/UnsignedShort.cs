using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the UnsignedShort VR type
    /// </summary>
    public class UnsignedShort : AbstractElement<ushort>
    {
        public UnsignedShort()
        {
        }

        public UnsignedShort(Tag tag, int data)
            : base(tag, (ushort) data)
        {
            VR = VR.UnsignedShort;
        }

        public UnsignedShort(Tag tag, ushort data)
            : base(tag, data)
        {
            VR = VR.UnsignedShort;
        }

        public UnsignedShort(Tag tag, ushort[] data)
            : base(tag, data)
        {
            VR = VR.UnsignedShort;
        }
    }
}