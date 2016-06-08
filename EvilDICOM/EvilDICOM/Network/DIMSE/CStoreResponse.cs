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
    /// <summary>
    /// Class CStoreResponse.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.DIMSE.AbstractDIMSEResponse" />
    /// <seealso cref="EvilDICOM.Core.Interfaces.IIOD" />
    public class CStoreResponse : AbstractDIMSEResponse, IIOD
    {
        /// <summary>
        /// The _affected sop instance uid
        /// </summary>
        private readonly UniqueIdentifier _affectedSOPInstanceUID = new UniqueIdentifier
        {
            Tag = TagHelper.AFFECTED_SOPINSTANCE_UID
        };

        /// <summary>
        /// The _data set
        /// </summary>
        private ushort _dataSet = 257;

        /// <summary>
        /// Initializes a new instance of the <see cref="CStoreResponse"/> class.
        /// </summary>
        public CStoreResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CStoreResponse"/> class.
        /// </summary>
        /// <param name="d">The d.</param>
        public CStoreResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort)C.C_STORE_RP;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CStoreResponse"/> class.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="status">The status.</param>
        public CStoreResponse(CStoreRequest req, Status status)
        {
            AffectedSOPClassUID = req.AffectedSOPClassUID;
            CommandField = (ushort) C.C_STORE_RP;
            MessageIDBeingResponsedTo = req.MessageID;
            DataSetType = _dataSet;
            AffectedSOPInstanceUID = req.AffectedSOPInstanceUID;
            Status = (ushort) status;
            GroupLength = (uint) GroupWriter.WriteGroupBytes(new DICOMObject(Elements.Skip(1).Take(6).ToList()),
                new DICOMWriteSettings(), "0000").Length;
        }

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the affected sop instance uid.
        /// </summary>
        /// <value>The affected sop instance uid.</value>
        public string AffectedSOPInstanceUID
        {
            get { return _affectedSOPInstanceUID.Data; }
            set { _affectedSOPInstanceUID.Data = value; }
        }

        #endregion

        /// <summary>
        /// The order of elements to send in a IIOD packet
        /// </summary>
        /// <value>The elements.</value>
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