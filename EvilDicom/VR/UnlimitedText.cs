using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class UnlimitedText : AbstractStringVR
        {
            public UnlimitedText() { base.VR = "UT"; }
        } 
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


