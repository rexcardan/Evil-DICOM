using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class DecimalString : AbstractElement<double[]>
    {
        public DecimalString(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseDecimalString(data);
        }
    }
}