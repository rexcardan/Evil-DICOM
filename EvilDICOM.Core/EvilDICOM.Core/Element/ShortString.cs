using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class ShortString : AbstractElement<string>
    {
        public new string Data
        {
            get { return base.Data; }
            set { base.Data = DataRestriction.EnforceLengthRestriction(16, value); }
        }

        public ShortString() { }

        public ShortString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.ShortString;
        }
    }
}
