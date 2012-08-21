using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class LongString : AbstractElement, IDICOMString
    {
        public string Data
        {
            get { return _data; }
            set { _data = DataRestriction.EnforceLengthRestriction(64, value); }
        }

        public LongString() { }

        public LongString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }

        private string _data;
    }
}
