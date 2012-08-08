using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class UniqueIdentifier : AbstractStringVR
        {
            public UniqueIdentifier() { base.VR = "UI"; }
        }
    }

}


//Copyright © 2012 Rex Cardan, Ph.D


