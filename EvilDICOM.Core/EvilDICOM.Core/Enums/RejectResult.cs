using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum RejectResult : byte
    {
        REJECTED_PERMANENT = 1,
        REJECTED_TRANSIENT = 2,
    }
}
