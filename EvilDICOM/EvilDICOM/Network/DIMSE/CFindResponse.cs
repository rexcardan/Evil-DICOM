#region

using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using System.Collections.Generic;
using System.Linq;
using C = EvilDICOM.Network.Enums.CommandField;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public class CFindResponse : AbstractDIMSEResponse
    {
        public CFindResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort) C.C_FIND_RP;
        }

        /// <summary>
        /// Creates a base C-Find response but more data needs to be supplied. See CFindService response methods
        /// </summary>
        /// <param name="req"></param>
        public CFindResponse(CFindRequest req, Status status)
        {
            AffectedSOPClassUID = req.AffectedSOPClassUID;
            CommandField = (ushort)C.C_FIND_RP;
            MessageIDBeingRespondedTo = req.MessageID;
            Status = (ushort)status;
        }

        public T GetIOD<T>() where T : AbstractDIMSEIOD
        {
            if (Data != null)
            {
                if (typeof(T) == typeof(CFindImageIOD))
                    return new CFindImageIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindStudyIOD))
                    return new CFindStudyIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindSeriesIOD))
                    return new CFindSeriesIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindPlanIOD))
                    return new CFindPlanIOD(new DICOMObject(Data.Elements)) as T;
            }
            return null;
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