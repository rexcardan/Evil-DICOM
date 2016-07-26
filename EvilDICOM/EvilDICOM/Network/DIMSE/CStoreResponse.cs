using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;
using EvilDICOM.Core.Selection;

namespace EvilDICOM.Network.DIMSE
{
    public class CStoreResponse : AbstractDIMSEResponse, IIOD
    {
        private readonly UniqueIdentifier _affectedSOPInstanceUID = new UniqueIdentifier
        {
            Tag = TagHelper.AFFECTED_SOPINSTANCE_UID
        };

        private ushort _dataSet = 257;

        public CStoreResponse()
        {
        }

        public CStoreResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort)C.C_STORE_RP;
        }

        public CStoreResponse(CStoreRequest req, Status status)
        {
            AffectedSOPClassUID = req.AffectedSOPClassUID;
            CommandField = (ushort) C.C_STORE_RP;
            MessageIDBeingRespondedTo = req.MessageID;
            DataSetType = _dataSet;
            AffectedSOPInstanceUID = req.AffectedSOPInstanceUID;
            Status = (ushort) status;
            GroupLength = (uint) GroupWriter.WriteGroupBytes(new DICOMObject(Elements.Skip(1).Take(6).ToList()),
                new DICOMWriteSettings(), "0000").Length;
        }

        #region PROPERTIES

        public string AffectedSOPInstanceUID
        {
            get { return _affectedSOPInstanceUID.Data; }
            set { _affectedSOPInstanceUID.Data = value; }
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
                    _messageIdBeingRespondedTo,
                    _dataSetType,
                    _affectedSOPInstanceUID,
                    _status
                };
            }
        }
    }
}