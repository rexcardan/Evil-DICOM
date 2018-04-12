using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class NEventReportRequest : AbstractDIMSERequest
    {
        public NEventReportRequest()
        {
            this.CommandField = (ushort)N_EVENT_REPORT_RQ;
        }

        public NEventReportRequest(DICOMObject d)
        {
            CommandField = (ushort)N_EVENT_REPORT_RQ;
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            AffectedSOPInstanceUID = sel.AffectedSOPInstanceUID.Data;
            CommandField = sel.CommandField.Data;
            MessageID = sel.MessageID.Data;
            DataSetType = sel.CommandDataSetType.Data;
            EventTypeId = sel.EventTypeID.Data;
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
                    _messageId,
                    _dataSetType,
                    _affectedSOPInstanceUID,
                    _eventTypeId
                };
            }
        }
    }
}
