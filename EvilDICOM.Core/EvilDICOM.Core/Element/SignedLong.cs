using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the SignedLong VR type
    /// </summary>
    public class SignedLong : AbstractElement<int>
    {
        public SignedLong() : base() { VR = Enums.VR.SignedLong; }

        public SignedLong(Tag tag, int data)
            : base(tag,data)
        {
            VR = Enums.VR.SignedLong;
        }

        public SignedLong(Tag tag, int[] data)
            : base(tag,data)
        {
            VR = Enums.VR.SignedLong;
        }
    }
}