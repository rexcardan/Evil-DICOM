using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class TimeVR : AbstractStringVR
        {
            public TimeVR() { VR = "TM"; }

            public new System.DateTime Data
            {
                get { return System.DateTime.ParseExact(base.Data, "HHmmss.fff", null); }
                set { 
                    base.Data = value.ToString("HHmmss.fff");
                }
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


