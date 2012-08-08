using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class DateTimeVR : AbstractStringVR
        {
            public DateTimeVR() { VR = "DT"; }

            public new System.DateTime Data
            {
                //Loss of precision in reading and writing. This class only allows 
                //1 thousandth of a second record instead of 1 millionth
                //I chose this so I could use the native C# DateTime object
                //TODO: Extend a new class that can handle a millionth of a sec
                get { return System.DateTime.ParseExact(base.Data, "yyyyMMddHHmmss.fff", null); }
                set { base.Data = value.ToString("yyyyMMddHHmmss.fff"); }
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


