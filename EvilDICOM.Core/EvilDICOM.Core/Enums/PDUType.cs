using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    /// <summary>
    /// Protocal Data Unit (PDU) Type. The first byte in a PDU message conveys the purpose of the message. The PDU type enum
    /// holds the different possible bytes and their meanings.
    /// </summary>
    public enum PDUType : byte
    {
        A_ASSOC_REQUEST = 0x01,
        A_ASSOC_ACCEPT = 0x02,
        A_ASSOC_REJECT = 0x03,
        P_DATA_TRANSFER = 0x04,
        A_RELEASE_REQUEST = 0x05,
        A_RELEASE_RESPONSE = 0x06,
        A_ABORT = 0x07 
    }
}
