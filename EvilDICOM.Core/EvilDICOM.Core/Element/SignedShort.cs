using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class SignedShort : AbstractElement<short?>
    {
        public SignedShort() { }

        public SignedShort(Tag tag, short? data)
        {
            Tag = tag;
            Data = data;
        }

    }
}