using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class Other64BitVeryLong : AbstractElement<byte>
    {
        public Other64BitVeryLong()
        {
            VR = VR.Other64BitVeryLongString;
        }

        public Other64BitVeryLong(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.Other64BitVeryLongString;
        }
    }
}
