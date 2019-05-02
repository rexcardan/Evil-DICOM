using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    public class Unsigned64bitVeryLong : AbstractElement<ulong>
    {
        public Unsigned64bitVeryLong()
        {
            VR = VR.Unsigned64BitVeryLong;
        }

        public Unsigned64bitVeryLong(Tag tag, ulong data)
            : base(tag, data)
        {
            VR = VR.Unsigned64BitVeryLong;
        }

        public Unsigned64bitVeryLong(Tag tag, ulong[] data)
            : base(tag, data)
        {
            VR = VR.Unsigned64BitVeryLong;
        }
    }
}
