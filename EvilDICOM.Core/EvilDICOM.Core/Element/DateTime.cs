using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the DateTime VR type
    /// </summary>
    public class DateTime : AbstractElement<System.DateTime?>
    {
        public DateTime() { }

        public DateTime(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseDateTime(data);
            VR = Enums.VR.DateTime;
        }
    }
}