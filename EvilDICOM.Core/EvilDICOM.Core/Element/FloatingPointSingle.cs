using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class FloatingPointSingle : AbstractElement
    {
        public float? Data { get; set; }

        public FloatingPointSingle() { }

        public FloatingPointSingle(Tag tag, float? data)
        {
            Tag = tag;
            Data = data;
        }
    }
}