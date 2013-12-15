using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the Date VR type
    /// </summary>
    public class Date : AbstractElement<System.DateTime?>
    {
        public Date() :base() { }

        public Date(Tag tag, string data)
            : base(tag,StringDataParser.ParseDate(data))
        {
            VR = Enums.VR.Date;
        }

        public Date(Tag tag, System.DateTime? data)
            : base(tag,data)
        {
            VR = Enums.VR.Date;
        }
    }
}