using System;
using System.Collections.Generic;
using System.Text;

namespace Evil_DICOM.Network.Messaging
{
    public class SendStatus
    {
        public bool DidConnect { get; set; }
        public bool WasRejected { get; set; }
        public bool WasAccepted { get { return DidConnect && !WasRejected; } }
        public string Reason { get; set; }
    }
}
