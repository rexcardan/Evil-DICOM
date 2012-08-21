using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class Date : AbstractElement
    {
        public System.DateTime? Data { get; set; }

        public Date() { }

        public Date(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseDate(data);
        }
    }
}