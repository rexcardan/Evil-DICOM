using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    public class Signed64bitVeryLong : AbstractElement<long>
    {
        public Signed64bitVeryLong()
        {
            VR = VR.Signed64BitVeryLong;
        }

        public Signed64bitVeryLong(Tag tag, long data)
            : base(tag, data)
        {
            VR = VR.Signed64BitVeryLong;
        }

        public Signed64bitVeryLong(Tag tag, long[] data)
            : base(tag, data)
        {
            VR = VR.Signed64BitVeryLong;
        }
    }
}
