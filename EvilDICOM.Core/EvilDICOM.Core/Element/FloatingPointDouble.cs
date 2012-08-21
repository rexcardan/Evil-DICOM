using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class FloatingPointDouble : AbstractElement
    {
        public double? Data { get; set; }

        public FloatingPointDouble() { }

        public FloatingPointDouble(Tag tag, double? data)
        {
            Tag = tag;
            Data = data;
        }
    }
}