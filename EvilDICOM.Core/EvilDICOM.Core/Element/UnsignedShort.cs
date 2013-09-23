using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the UnsignedShort VR type
    /// </summary>
    public class UnsignedShort : AbstractElement<ushort>
    {
        public UnsignedShort() : base() { }

        public UnsignedShort(Tag tag, int data)
            : base(tag,(ushort)data)
        {
            VR = Enums.VR.UnsignedShort;
        }

        public UnsignedShort(Tag tag, ushort data)
            : base(tag,data)
        {
            VR = Enums.VR.UnsignedShort;
        }

        public UnsignedShort(Tag tag, ushort[] data)
            : base(tag,data)
        {
            VR = Enums.VR.UnsignedShort;
        }
    }
}