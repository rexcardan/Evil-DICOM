using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the FloatingPointSingle VR type
    /// </summary>
    public class FloatingPointSingle : AbstractElement<float>
    {
        public FloatingPointSingle() : base() { VR = Enums.VR.FloatingPointSingle; }

        public FloatingPointSingle(Tag tag, float data)
            : base(tag,data)
        {
            VR = Enums.VR.FloatingPointSingle;
        }
        public FloatingPointSingle(Tag tag, float[] data)
            : base(tag, data)
        {
            VR = Enums.VR.FloatingPointSingle;
        }
    }
}