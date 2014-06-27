using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    //ps 3.7 Annex C Status Type Encoding(Normative)
    public enum Status : ushort
    {
        SUCCESS = 0,
        WARNING = 1,
        FAILURE = 10,
        CANCEL = 65024,
        PENDING = 65280
    }
}
