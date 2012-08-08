using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {

        public class CodeString : AbstractStringVR
        {
            public CodeString() { base.VR = "CS"; }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


