using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class LongString : AbstractStringVR
        {
            public LongString() { base.VR = "LO"; }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


