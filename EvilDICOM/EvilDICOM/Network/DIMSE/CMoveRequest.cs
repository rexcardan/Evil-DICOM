using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class CMoveRequest : AbstractDIMSERequest, IIOD
    {
        private readonly ApplicationEntity _moveDestination = new ApplicationEntity { Tag = TagHelper.MOVE_DESTINATION };
        private readonly UnsignedShort _priority = new UnsignedShort { Tag = TagHelper.PRIORITY };

        public CMoveRequest(CMoveIOD iod, string moveToAeTitle, Root root = Root.STUDY, Priority priority = Core.Enums.Priority.MEDIUM,
            ushort messageId = 1)
        {
            switch (root)
            {
                case Root.PATIENT:
                    AffectedSOPClassUID = AbstractSyntax.PATIENT_MOVE;
                    break;
                case Root.STUDY:
                    AffectedSOPClassUID = AbstractSyntax.STUDY_MOVE;
                    break;
            }
            CommandField = (ushort)C.C_MOVE_RQ;
            Data = new DICOMObject(iod.Elements);
            MoveDestination = moveToAeTitle;
            Priority = (ushort)priority;
            MessageID = messageId;
        }

        public CMoveRequest(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = (ushort)C.C_MOVE_RQ;
            MessageID = sel.MessageID.Data;
            Priority = sel.Priority.Data;
            DataSetType = sel.CommandDataSetType.Data;
            MoveDestination = sel.MoveDestination.Data;
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
                    _priority,
                    _dataSetType,
                    _moveDestination
                };
            }
        }

        #region PROPERTIES

        public ushort Priority
        {
            get { return _priority.Data; }
            set { _priority.Data = value; }
        }

        public string MoveDestination
        {
            get { return _moveDestination.Data; }
            set { _moveDestination.Data = value; }
        }

        #endregion
    }
}