using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the UnsignedLong VR type
    /// </summary>
    public class UnsignedLong : AbstractElement<uint>
    {
        public UnsignedLong() : base() { VR = Enums.VR.UnsignedLong; }

        public UnsignedLong(Tag tag, uint data)
            : base(tag,data)
        {
            VR = Enums.VR.UnsignedLong;
        }

        public UnsignedLong(Tag tag, uint[] data)
            : base(tag,data)
        {
            VR = Enums.VR.UnsignedLong;
        }
    }
}