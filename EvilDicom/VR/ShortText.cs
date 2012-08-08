using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class ShortText : AbstractStringVR
        {
            public ShortText() { base.VR = "ST"; }
        }   
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


