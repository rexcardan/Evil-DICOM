#region

using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;
using C = EvilDICOM.Network.Enums.CommandField;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public class CCancel : AbstractDIMSEBase
    {
        protected UnsignedShort _messageIdBeingRespondedTo = new UnsignedShort
        {
            Tag = TagHelper.MessageIDBeingRespondedTo
        };

        public CCancel(AbstractDIMSERequest req)
        {
            AffectedSOPClassUID = req.AffectedSOPClassUID;
            MessageIDBeingResponsedTo = req.MessageID;
            DataSetType = 257; // No data
            CommandField = (ushort) C.C_CANCEL;
            GroupLength = (uint) GroupWriter.WriteGroupBytes(new DICOMObject(Elements.Skip(1).Take(5).ToList()),
                    new DICOMIOSettings {TransferSyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN}, "0000")
                .Length;
        }

        public ushort MessageIDBeingResponsedTo
        {
            get { return _messageIdBeingRespondedTo.Data; }
            set { _messageIdBeingRespondedTo.Data = value; }
        }

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
                    _dataSetType
                };
            }
        }
    }
}