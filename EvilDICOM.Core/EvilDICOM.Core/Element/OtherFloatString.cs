using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the OtherFloatString VR type
    /// </summary>
    public class OtherFloatString : AbstractElement<byte>
    {
        public OtherFloatString() : base() { VR = Enums.VR.OtherFloatString; }

        public OtherFloatString(Tag tag, byte[] data)
            : base(tag,data)
        {
            VR = Enums.VR.OtherFloatString;
        }
    }
}