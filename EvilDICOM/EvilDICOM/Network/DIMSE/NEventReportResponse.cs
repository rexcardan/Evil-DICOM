using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.Enums;
using static EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class NEventReportResponse : AbstractDIMSEResponse
    {
        public NEventReportResponse()
        {
            this.CommandField = (ushort)N_EVENT_REPORT_RP;
        }

        public NEventReportResponse(NEventReportRequest req, Status status)
        {
            this.CommandField = (ushort)N_EVENT_REPORT_RP;
            this.MessageIDBeingRespondedTo = req.MessageID;
            this.AffectedSOPClassUID = req.AffectedSOPClassUID;
            this.AffectedSOPInstanceUID = req.AffectedSOPInstanceUID;
            this.EventTypeId = req.EventTypeId;
            this.DataSetType = (ushort)CommandDataSetType.EMPTY;
            this.Status = (ushort)status;
        }

        public NEventReportResponse(DICOMObject d)
        {
            CommandField = (ushort)N_EVENT_REPORT_RP;
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            AffectedSOPInstanceUID = sel.AffectedSOPInstanceUID.Data;
            MessageIDBeingRespondedTo = sel.MessageIDBeingRespondedTo.Data;
            DataSetType = sel.CommandDataSetType.Data;
            Status = sel.Status.Data;
            if (sel.EventTypeID != null)
                EventTypeId = sel.ActionTypeID.Data;
        }

        protected UnsignedShort _eventTypeId = new UnsignedShort { Tag = TagHelper.EventTypeID };

        public ushort EventTypeId
        {
            get { return _eventTypeId.Data; }
            set { _eventTypeId.Data = value; }
        }

        protected UniqueIdentifier _affectedSOPInstanceUID = new UniqueIdentifier { Tag = TagHelper.AffectedSOPInstanceUID };
        public string AffectedSOPInstanceUID
        {
            get { return _affectedSOPInstanceUID.Data; }
            set { _affectedSOPInstanceUID.Data = value; }
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
                    _affectedSOPClassUID,
                    _commandField,
                    _messageIdBeingRespondedTo,
                    _dataSetType,
                    _status,
                    _eventTypeId,
                };
            }
        }
    }
}
