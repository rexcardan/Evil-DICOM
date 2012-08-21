using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class Time : AbstractElement
    {
        public System.DateTime? Data { get; set; }

        public Time() { }

        public Time(Tag tag, string data)
        {
            Tag = tag;
            Data = StringDataParser.ParseTime(data);
        }
    }
}