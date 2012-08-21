using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.Element
{
    public class CodeString : AbstractElement, IDICOMString
    {
        public string Data
        {
            get { return _data; }
            set { _data = DataRestriction.EnforceLengthRestriction(50, value); }
        }

        public CodeString() { }

        public CodeString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }

        private string _data;
    }
}
