using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class Sequence : AbstractElement
    {
        public List<DICOMObject> Items { get; set; }
        public List<DICOMObject> Data
        {
            get
            {
                return Items;
            }
        }

    }
}
