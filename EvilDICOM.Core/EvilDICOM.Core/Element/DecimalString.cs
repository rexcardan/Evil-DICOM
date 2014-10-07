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
    public class DecimalString : AbstractElement<double>
    {
        public DecimalString() { }
        public DecimalString(Tag tag, string data)
            : base(tag, StringDataParser.ParseDecimalString(data))
        {      
            VR = Enums.VR.DecimalString;
        }

        public DecimalString(Tag tag, double data)
            : base(tag,data)
        {
            VR = Enums.VR.DecimalString;
        }

        public DecimalString(Tag tag, double[] data)
            : base(tag,data)
        {
            VR = Enums.VR.DecimalString;
        }
    }
}