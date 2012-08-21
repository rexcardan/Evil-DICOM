using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum CommandField :ushort
    {
        C_STORE_RQ = 1,
        C_STORE_RP = 8001
    }
}
