using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class CEchoResponse : AbstractDIMSEResponse, IIOD
    {
        private ushort _dataSet = 257;

        /// <summary>
        ///     Used to generate a new Echo Response from an Echo Request
        /// </summary>
        /// <param name="req">the request that is being responded to</param>
        /// <param name="status">the status of the echo</param>
        /// <param name="presContext">the presentation context with which to write the data</param>
        public CEchoResponse(CEchoRequest req, Status status)
        {
            AffectedSOPClassUID = req.AffectedSOPClassUID;
            CommandField = (ushort)C.C_ECHO_RP;
            MessageIDBeingRespondedTo = req.MessageID;
            DataSetType = _dataSet;
            Status = (ushort)status;
            GroupLength = (uint)GroupWriter.WriteGroupBytes(new DICOMObject(Elements.Skip(1).Take(5).ToList()),
                new DICOMWriteSettings { TransferSyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN }, "0000").Length;
        }

        public CEchoResponse(DICOMObject d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            if (sel.AffectedSOPClassUID != null)
            {
                AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            };
            CommandField = (ushort)C.C_ECHO_RP;
            MessageIDBeingRespondedTo = sel.MessageIDBeingRespondedTo.Data;
            DataSetType = sel.CommandDataSetType.Data;
            Status = sel.Status.Data;
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
                    _status
                };
            }
        }
    }
}