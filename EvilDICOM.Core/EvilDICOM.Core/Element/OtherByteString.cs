using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class OtherByteString : AbstractElement
    {
        public byte[] Data { get; set; }

        public OtherByteString() { }

        public OtherByteString(Tag tag, byte[] data)
        {
            Tag = tag;
            Data = data;
        }
    }
}