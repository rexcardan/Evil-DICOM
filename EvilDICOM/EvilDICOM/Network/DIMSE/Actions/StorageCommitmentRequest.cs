using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.DIMSE.Actions
{
    public class StorageCommitmentRequest : NActionRequest
    {
        public StorageCommitmentRequest()
        {
            this.ActionTypeID = 1;
            this.AffectedSOPClassUID = "1.2.840.10008.1.20.1";
            this.RequestedSOPClassUID = "1.2.840.10008.1.20.1";
            this.RequestedSOPInstanceUID = "1.2.840.10008.1.20.1.1";
            this.DataSetType = (ushort)CommandDataSetType.HAS_DATA;
        }
    }
}
