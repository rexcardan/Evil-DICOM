using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class ShortString : AbstractStringVR
        {
            public ShortString() { base.VR = "SH"; }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


