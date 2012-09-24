using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class IntegerString : AbstractElement<int[]>
    {
        public IntegerString() { }

        public IntegerString(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseIntegerString(data);
        }
    }
}