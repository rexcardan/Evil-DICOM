using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.DIMSE
{
    public class CFindRequest : AbstractDIMSERequest
    {
        #region PRIVATE

        private readonly UnsignedShort _priority = new UnsignedShort { Tag = TagHelper.PRIORITY };

        #endregion

        internal CFindRequest(AbstractDIMSEIOD query, Root root, ushort priority = (ushort) Core.Enums.Priority.MEDIUM, ushort messageId = 1)
        {
            Query = query;
            MessageID = messageId;
            switch (root)
            {
                case Root.PATIENT:
                    AffectedSOPClassUID = AbstractSyntax.PATIENT_FIND;
                    break;
                case Root.STUDY:
                    AffectedSOPClassUID = AbstractSyntax.STUDY_FIND;
                    break;
            }
            Priority = priority;
            CommandField = (ushort)C.C_FIND_RQ;
            Data = new DICOMObject(query.Elements);
        }

        public CFindRequest(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = (ushort)C.C_FIND_RQ;
            MessageID = sel.MessageID.Data;
            Priority = sel.Priority.Data;
            DataSetType = sel.CommandDataSetType.Data;
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
                    _dataSetType
                };
            }
        }


        #region PROPERTIES

        public ushort Priority
        {
            get { return _priority.Data; }
            set { _priority.Data = value; }
        }

        public AbstractDIMSEIOD Query { get; set; }

        #endregion

    }
}