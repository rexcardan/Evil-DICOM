using EvilDICOM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Dictionaries
{
    public class TagInfo
    {
        public string Name { get; set; }
        public string TagID { get; set; }
        public VR VR { get; set; }
    }
}
