using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class CGetRequest : AbstractDIMSERequest, IIOD
    {

        public CGetRequest(CFindImageIOD iod, Root root = Root.STUDY,
     Priority priority = Core.Enums.Priority.MEDIUM,
     ushort messageId = 1)
        {
            switch (root)
            {
                case Root.PATIENT:
                    AffectedSOPClassUID = AbstractSyntax.PATIENT_GET;
                    break;
                case Root.STUDY:
                    AffectedSOPClassUID = AbstractSyntax.STUDY_GET;
                    break;
            }
            CommandField = (ushort)C.C_GET_RQ;
            Data = new DICOMObject(iod.Elements);
            Priority = (ushort)priority;
            MessageID = messageId;
        }

        public CGetRequest(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = (ushort)C.C_MOVE_RQ;
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
                    _dataSetType,
                };
            }
        }

        #region PROPERTEIS
        private readonly UnsignedShort _priority = new UnsignedShort { Tag = TagHelper.Priority };
        public ushort Priority
        {
            get { return _priority.Data; }
            set { _priority.Data = value; }
        }
        #endregion
    }
}
