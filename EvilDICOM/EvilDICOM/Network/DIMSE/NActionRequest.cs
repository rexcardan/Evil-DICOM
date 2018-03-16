using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.DIMSE
{
    public class NActionRequest : AbstractDIMSERequest
    {
        public NActionRequest()
        {
            CommandField = 0x0130;
            DataSetType = (ushort)CommandDataSetType.EMPTY;
        }

        protected UniqueIdentifier _requestedSOPClassUID = new UniqueIdentifier { Tag = TagHelper.RequestedSOPClassUID };

        public string RequestedSOPClassUID
        {
            get { return _requestedSOPClassUID.Data; }
            set { _requestedSOPClassUID.Data = value; }
        }

        protected UniqueIdentifier _requestedSOPInstanceUID = new UniqueIdentifier { Tag = TagHelper.RequestedSOPInstanceUID };

        public string RequestedSOPInstanceUID
        {
            get { return _requestedSOPInstanceUID.Data; }
            set { _requestedSOPInstanceUID.Data = value; }
        }

        protected UnsignedShort _actionTypeId = new UnsignedShort { Tag = TagHelper.ActionTypeID };

        public ushort ActionTypeID
        {
            get { return _actionTypeId.Data; }
            set { _actionTypeId.Data = value; }
        }

        /// <summary>
        ///     The order of elements to send in a IIOD packet
        /// </summary>
        public override List<IDICOMElement> Elements
        {
            get
            {
                return new List<IDICOMElement>
                {
                    _groupLength,
                    _requestedSOPClassUID,
                    _commandField,
                    _messageId,
                    _dataSetType,
                    _requestedSOPInstanceUID,
                    _actionTypeId
                };
            }
        }
    }
}
