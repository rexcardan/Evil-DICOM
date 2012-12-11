using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the UnlimitedText VR type
    /// </summary>
    public class UnlimitedText : AbstractElement<string>
    {
        public new string Data
        {
            get { return base.Data; }
            set { base.Data = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value); }
        }

        public UnlimitedText() { }

        public UnlimitedText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.UnlimitedText;
        }
    }
}