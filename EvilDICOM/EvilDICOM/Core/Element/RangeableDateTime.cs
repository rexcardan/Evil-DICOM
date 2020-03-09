using EvilDICOM.Core.Element;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvilDICOM.Core.Element
{
    public class RangeableDateTime : AbstractElement<System.DateTime?>
    {
        public RangeableDateTime()
        {
        }

        public RangeableDateTime(Tag tag, System.DateTime? data)
                : base(tag, data) { }

        public RangeableDateTime(Tag tag, System.DateTime?[] data)
                : base(tag, data) { }

        public bool IsRange { get; set; }

    }
}
