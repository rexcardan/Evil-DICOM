using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using C = EvilDICOM.Network.Enums.CommandField;


namespace EvilDICOM.Network.DIMSE
{
    public class CEchoRequest : AbstractDIMSERequest, IIOD
    {
        public CEchoRequest(ushort messageId = 1)
        {
            MessageID = messageId;
            AffectedSOPClassUID = AbstractSyntax.VERIFICATION;
            CommandField = (ushort) C.C_ECHO_RQ;
        }

        public CEchoRequest(DICOMObject d) : base(d)
        {
            CommandField = (ushort) C.C_ECHO_RQ;
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
                    _dataSetType
                };
            }
        }
    }
}