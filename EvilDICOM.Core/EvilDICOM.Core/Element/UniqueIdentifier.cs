using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class UniqueIdentifier : AbstractElement, IDICOMString
    {
        public string Data
        {
            get { return _data; }
            set { _data = DataRestriction.EnforceLengthRestriction(64, value); }
        }

        public UniqueIdentifier()
        {
        }

        public UniqueIdentifier(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
        }

        private string _data;    
    }
}
