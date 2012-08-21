using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class UnlimitedText : AbstractElement, IDICOMString
    {
        public string Data
        {
            get { return _data; }
            set { _data = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value); }
        }

        public UnlimitedText() { }

        public UnlimitedText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }

        private string _data;
    }
}