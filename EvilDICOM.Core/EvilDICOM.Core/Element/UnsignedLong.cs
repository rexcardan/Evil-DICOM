using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class UnsignedLong : AbstractElement<uint?>
    {
        public UnsignedLong() { }

        public UnsignedLong(Tag tag, uint? data)
        {
            Tag = tag;
            Data = data;
        }
    }
}