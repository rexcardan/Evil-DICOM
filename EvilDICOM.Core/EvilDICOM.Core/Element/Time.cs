using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the Time VR type
    /// </summary>
    public class Time : AbstractElement<System.DateTime?>
    {
        public Time() : base() { VR = Enums.VR.Time; }

        public Time(Tag tag, string data)
            : base(tag, StringDataParser.ParseTime(data))
        {
            VR = Enums.VR.Time;
        }
    }
}