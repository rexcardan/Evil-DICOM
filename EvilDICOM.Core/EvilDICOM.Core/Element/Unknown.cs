using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class Unknown : AbstractElement
    {
        public byte[] Data { get; set; }

        public Unknown() { }

        public Unknown(Tag tag, byte[] data)
        {
            Tag = tag;
            Data = data;
        }
    }
}
