using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the SignedShort VR type
    /// </summary>
    public class SignedShort : AbstractElement<short>
    {
        public SignedShort() : base() { VR = Enums.VR.SignedShort; }
        
        public SignedShort(Tag tag, short data)
            : base(tag,data)
        {
            VR = Enums.VR.SignedShort;
        }

        public SignedShort(Tag tag, short[] data)
            : base(tag,data)
        {
            VR = Enums.VR.SignedShort;
        }

    }
}