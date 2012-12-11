using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the DecimalString VR type
    /// </summary>
    public class DecimalString : AbstractElement<double[]>
    {
        public DecimalString(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseDecimalString(data);
            VR = Enums.VR.DecimalString;
        }
    }
}