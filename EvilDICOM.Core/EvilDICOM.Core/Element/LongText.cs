using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the LongText VR type
    /// </summary>
    public class LongText : AbstractElement<string>
    {
        public new string Data
        {
            get { return base.Data; }
            set { base.Data = DataRestriction.EnforceLengthRestriction(10240, value); }
        }

        public LongText() { }

        public LongText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.LongText;
        }
    }
}