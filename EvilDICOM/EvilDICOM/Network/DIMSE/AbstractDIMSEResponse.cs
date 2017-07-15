#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public abstract class AbstractDIMSEResponse : AbstractDIMSE
    {
        protected UnsignedShort _messageIdBeingRespondedTo = new UnsignedShort
        {
            Tag = TagHelper.MessageIDBeingRespondedTo
        };

        protected UnsignedShort _status = new UnsignedShort {Tag = TagHelper.Status};

        public AbstractDIMSEResponse()
        {
        }

        public AbstractDIMSEResponse(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            if (sel.AffectedSOPClassUID != null)
                AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            ;
            MessageIDBeingRespondedTo = sel.MessageIDBeingRespondedTo.Data;
            DataSetType = sel.CommandDataSetType.Data;
            Status = sel.Status.Data;
        }

        public ushort MessageIDBeingRespondedTo
        {
            get { return _messageIdBeingRespondedTo.Data; }
            set { _messageIdBeingRespondedTo.Data = value; }
        }

        public ushort Status
        {
            get { return _status.Data; }
            set { _status.Data = value; }
        }

        public override List<IDICOMElement> Elements
        {
            get { return new List<IDICOMElement>(); }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", GetType().Name, (Status) Status);
        }
    }
}