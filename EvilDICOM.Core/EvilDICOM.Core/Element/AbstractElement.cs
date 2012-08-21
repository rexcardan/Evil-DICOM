using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    public class AbstractElement : IDICOMElement
    {
         public override string ToString()
        {
            return string.Format("VR = {0}, Tag = {1},{2}", VR.ToString(), Tag.Group, Tag.Element);
        }

         public Tag Tag
         {
             get;
             set;
         }

         public VR VR
         {
             get;
             set;
         }
    }
}
