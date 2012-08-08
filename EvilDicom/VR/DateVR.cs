using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDicom
{
    namespace VR
    {
        public class DateVR : AbstractStringVR
        {
            public DateVR() { VR = "DA"; }

            public new System.DateTime Data
            {
                get { return System.DateTime.ParseExact(base.Data, "yyyyMMdd", null); }
                set { base.Data = value.ToString("yyyyMMdd"); }
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


