using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Enums
{
    public enum CommandDataSetType : ushort
    {
        EMPTY = 257, //0x101
        HAS_DATA = 0
    }
}
