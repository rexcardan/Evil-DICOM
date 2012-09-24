using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class DateTime : AbstractElement<System.DateTime?>
    {
        public System.DateTime? Data { get; set; }

        public DateTime() { }

        public DateTime(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseDateTime(data);
        }
    }
}