using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the Unknown VR type
    /// </summary>
    public class Unknown : AbstractElement<byte>
    {
        public Unknown() : base() {}

        public Unknown(Tag tag, byte[] data)
            : base(tag,data)
        {
            VR = Enums.VR.Unknown;
        }
    }
}
