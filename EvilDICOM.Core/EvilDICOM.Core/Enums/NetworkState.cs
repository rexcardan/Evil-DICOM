using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum NetworkState
    {
        IDLE = 1,
        TRANSPORT_CONNECTION_OPEN = 2,
        AWAITING_LOCAL_ASSOCIATE_RESPONSE = 3,
        AWAITING_TRANSPORT_CONNECTION_OPENING = 4,
        AWAITING_ACCEPT_OR_REJECT = 5,
        ASSOCIATION_ESTABLISHED_WAITING_ON_DATA = 6,
        AWAITING_RELEASE_RESPONSE = 7,
        AWAITING_RELEASE = 8,
    }
}
