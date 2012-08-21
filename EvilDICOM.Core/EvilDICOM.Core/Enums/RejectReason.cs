using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum RejectReason_SCU : byte
    {
        NO_REASON_GIVEN = 1,
        APPLICATION_CONTEXT_NAME_NOT_SUPPORTED = 2,
        CALLING_AE_TITLE_NOT_RECOGNIZED = 3,
        RESERVED_4 = 4,
        RESERVED_5 = 5,
        RESERVED_6 = 6,
        CALLED_AE_TITLE_NOT_RECOGNIZED = 7,
        RESERVED_8 = 8,
        RESERVED_9 = 9,
        RESERVED_10 = 10
    }

    public enum RejectReason_SCP_ASCE : byte
    {
        NO_REASON_GIVEN = 1,
        PROTOCOL_VERSION_NOT_SUPPORTED = 2,
    }

    public enum RejectReason_SCP_PRESENTATION : byte
    {
        RESERVED = 0,
        TEMPORARY_CONGESTION = 1,
        LOCAL_LIMIT_EXCEEDED = 2,
        RESERVED_3 = 3,
        RESERVED_4 = 4,
        RESERVED_5 = 5,
        RESERVED_6 = 6,
        RESERVED_7 = 7
    }
}
