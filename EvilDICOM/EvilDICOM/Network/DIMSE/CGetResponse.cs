using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class CGetResponse : AbstractDIMSEResponse, IIOD
    {
        public CGetResponse()
        {
            CommandField = (ushort)C.C_GET_RP;
        }

        public CGetResponse(DICOMObject d)
            : base(d)
        {
            var sel = new DICOMSelector(d);
            GroupLength = sel.CommandGroupLength.Data;
            AffectedSOPClassUID = sel.AffectedSOPClassUID.Data;
            CommandField = (ushort)C.C_GET_RP;
            MessageIDBeingRespondedTo = sel.MessageIDBeingRespondedTo.Data;
            DataSetType = sel.CommandDataSetType.Data;
            NumberOfFailedOps = sel.NumberOfFailedSuboperations != null
                ? sel.NumberOfFailedSuboperations.Data
                : (ushort)0;
            NumberOfRemainingOps = sel.NumberOfRemainingSuboperations != null
                ? sel.NumberOfRemainingSuboperations.Data
                : (ushort)0;
            NumberOfCompletedOps = sel.NumberOfCompletedSuboperations != null
                ? sel.NumberOfCompletedSuboperations.Data
                : (ushort)0;
            NumberOfWarningOps = sel.NumberOfWarningSuboperations != null
                ? sel.NumberOfWarningSuboperations.Data
                : (ushort)0;
            Status = sel.Status.Data;
        }

        #region PROPERTIES
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
