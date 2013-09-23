using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the OtherWordString VR type
    /// </summary>
    public class OtherWordString : AbstractElement<byte>
    {
        public OtherWordString() : base() { VR = Enums.VR.OtherWordString; }

        public OtherWordString(Tag tag, byte[] data)
            : base(tag,data)
        {
            VR = Enums.VR.OtherWordString;
        }
    }
}