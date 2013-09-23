using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the IntegerString VR type
    /// </summary>
    public class IntegerString : AbstractElement<int>
    {
        public IntegerString() : base() { VR = Enums.VR.IntegerString; }

        public IntegerString(Tag tag, string data)
            : base(tag, StringDataParser.ParseIntegerString(data))
        {      
            VR = Enums.VR.IntegerString;
        }

        public IntegerString(Tag tag, int data)
            : base(tag,data)
        {   
            VR = Enums.VR.IntegerString;
        }

        public IntegerString(Tag tag, int[] data)
            : base(tag,data)
        {
            VR = Enums.VR.IntegerString;
        }
    }
}