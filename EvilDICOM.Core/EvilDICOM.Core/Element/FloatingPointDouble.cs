using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the FloatingPointDouble VR type
    /// </summary>
    public class FloatingPointDouble : AbstractElement<double>
    {
        public FloatingPointDouble() : base() { VR = Enums.VR.FloatingPointDouble; }

        public FloatingPointDouble(Tag tag, double data)
            : base(tag,data)
        {
            VR = Enums.VR.FloatingPointDouble;

        }
        public FloatingPointDouble(Tag tag, double[] data)
            : base(tag,data)
        {
            VR = Enums.VR.FloatingPointDouble;
        }
    }
}