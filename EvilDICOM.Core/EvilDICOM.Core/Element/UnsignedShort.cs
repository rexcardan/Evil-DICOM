using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class UnsignedShort : AbstractElement
    {
        public ushort? Data { get; set; }

        public UnsignedShort() { }

        public UnsignedShort(Tag tag, ushort? data)
        {
            Tag = tag;
            Data = data;
        }
    }
}