#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using EvilDICOM.Network.Enums;
using C = EvilDICOM.Network.Enums.CommandField;
using EvilDICOM.Core.IO.Writing;
using System.Linq;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public class CMoveResponse : AbstractDIMSEResponse, IIOD
    {
        private readonly UnsignedShort _numCompletedOps = new UnsignedShort
        {
            Tag = TagHelper.NumberOfCompletedSuboperations
        };

        private readonly UnsignedShort _numFailedOps = new UnsignedShort
        {
            Tag = TagHelper.NumberOfFailedSuboperations
        };

        private readonly UnsignedShort _numRemainingOps = new UnsignedShort
        {
            Tag = TagHelper.NumberOfRemainingSuboperations
        };

        private readonly UnsignedShort _numWarningOps = new UnsignedShort
        {
            Tag = TagHelper.NumberOfWarningSuboperations
        };

        public CMoveResponse()
        {
            CommandField = (ushort) C.C_MOVE_RP;
        }

        public CMoveResponse(DICOMObject d)
            : base(d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = (ushort) C.C_MOVE_RP;
            MessageIDBeingRespondedTo = sel.MessageIDBeingRespondedTo.Data;
            DataSetType = sel.CommandDataSetType.Data;
            NumberOfFailedOps = sel.NumberOfFailedSuboperations != null
                ? sel.NumberOfFailedSuboperations.Data
                : (ushort) 0;
            NumberOfRemainingOps = sel.NumberOfRemainingSuboperations != null
                ? sel.NumberOfRemainingSuboperations.Data
                : (ushort) 0;
            NumberOfCompletedOps = sel.NumberOfCompletedSuboperations != null
                ? sel.NumberOfCompletedSuboperations.Data
                : (ushort) 0;
            NumberOfWarningOps = sel.NumberOfWarningSuboperations != null
                ? sel.NumberOfWarningSuboperations.Data
                : (ushort) 0;
            Status = sel.Status.Data;
        }

        public CMoveResponse(CMoveRequest req, Status status)
        {
            this.CommandField = (ushort)C.C_MOVE_RP;
            this.AffectedSOPClassUID = req.AffectedSOPClassUID;
            this.MessageIDBeingRespondedTo = req.MessageID;
            this.DataSetType = (ushort)257;
            this.Status = (ushort)status;
            this.GroupLength = (uint)GroupWriter.WriteGroupBytes(new DICOMObject(this.Elements.Skip<IDICOMElement>(1).Take<IDICOMElement>(6).ToList<IDICOMElement>()), new DICOMIOSettings(), "0000").Length;
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
                    _status,
                    _numRemainingOps,
                    _numCompletedOps,
                    _numFailedOps,
                    _numWarningOps
                };
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} {2}/{3}", GetType().Name, (Status) Status, NumberOfCompletedOps,
                NumberOfRemainingOps);
        }

        #region PROPERTIES

        public ushort NumberOfRemainingOps
        {
            get { return _numRemainingOps.Data; }
            set { _numRemainingOps.Data = value; }
        }

        public ushort NumberOfCompletedOps
        {
            get { return _numCompletedOps.Data; }
            set { _numCompletedOps.Data = value; }
        }

        public ushort NumberOfFailedOps
        {
            get { return _numFailedOps.Data; }
            set { _numFailedOps.Data = value; }
        }

        public ushort NumberOfWarningOps
        {
            get { return _numWarningOps.Data; }
            set { _numWarningOps.Data = value; }
        }

        #endregion
    }
}