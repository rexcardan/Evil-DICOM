using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using C = EvilDICOM.Network.Enums.CommandField;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.DIMSE
{
    public class CStoreRequest : AbstractDIMSERequest, IIOD
    {
        private readonly UniqueIdentifier _affectedSOPInstanceUID = new UniqueIdentifier
        {
            Tag = TagHelper.AffectedSOPInstanceUID
        };

        private readonly ApplicationEntity _moveOrigAETitle = new ApplicationEntity
        {
            Tag = TagHelper.MoveOriginatorApplicationEntityTitle
        };

        private readonly UnsignedShort _moveOrigMessageID = new UnsignedShort
        {
            Tag = TagHelper.MoveOriginatorMessageID
        };

        private readonly UnsignedShort _priority = new UnsignedShort { Tag = TagHelper.Priority };

        public CStoreRequest()
        {
            CommandField = (ushort)C.C_STORE_RQ;
        }

        public CStoreRequest(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = sel.CommandField.Data;
            MessageID = sel.MessageID.Data;
            Priority = sel.Priority.Data;
            DataSetType = sel.CommandDataSetType.Data;
            AffectedSOPInstanceUID = sel.AffectedSOPInstanceUID.Data;
            MoveOrigAETitle = sel.MoveOriginatorApplicationEntityTitle != null
                ? sel.MoveOriginatorApplicationEntityTitle.Data
                : "";
            MoveOrigMessageID = sel.MoveOriginatorMessageID != null ? sel.MoveOriginatorMessageID.Data : default(ushort);
        }


        #region PROPERTIES

        public ushort Priority
        {
            get { return _priority.Data; }
            set { _priority.Data = value; }
        }

        public string AffectedSOPInstanceUID
        {
            get { return _affectedSOPInstanceUID.Data; }
            set { _affectedSOPInstanceUID.Data = value; }
        }

        public string MoveOrigAETitle
        {
            get { return _moveOrigAETitle.Data; }
            set { _moveOrigAETitle.Data = value; }
        }

        public ushort MoveOrigMessageID
        {
            get { return _moveOrigMessageID.Data; }
            set { _moveOrigMessageID.Data = value; }
        }

        #endregion

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
                    _priority,
                    _dataSetType,
                    _affectedSOPInstanceUID,
                    _moveOrigAETitle,
                    _moveOrigMessageID
                };
            }
        }
    }
}