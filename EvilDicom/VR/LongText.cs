using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class LongText : AbstractStringVR
        {
            public LongText() { base.VR = "LT"; }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


