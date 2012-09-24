using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class FloatingPointDouble : AbstractElement<double?>
    {
        public FloatingPointDouble() { }

        public FloatingPointDouble(Tag tag, double? data)
        {
            Tag = tag;
            Data = data;
        }
    }
}