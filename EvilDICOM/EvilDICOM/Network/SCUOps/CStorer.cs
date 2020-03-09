using EvilDICOM.Core;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network.DIMSE;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.SCUOps
{
    public class CStorer
    {
        private DICOMSCU _scu;
        private Entity callingEntity;

        public CStorer(DICOMSCU dICOMSCU, Entity callingEntity)
        {
            this._scu = dICOMSCU;
            this.callingEntity = callingEntity;
        }

        public ILogger Logger { get { return _scu.Logger; } }

        /// <summary>
        /// Emits a CStore operation to an entity which moves an image to the specified entity
        /// </summary>
        /// <param name="dcm">the DICOM object to send</param>
        /// <param name="msgId">the message Id of this message</param>
        /// <returns>a C-Store response of the operation</returns>
        public CStoreResponse SendCStore(DICOMObject dcm, ref ushort msgId)
        {
            dcm.RemoveMetaHeader();
            var request = GenerateCStoreRequest(dcm, msgId);
            return _scu.GetResponse<CStoreResponse, CStoreRequest>(request, callingEntity, ref msgId);
        }

        private CStoreRequest GenerateCStoreRequest(DICOMObject toSend, ushort messageId = 1)
        {
            var sel = toSend.GetSelector();
            var cStoreReq = new CStoreRequest();
            cStoreReq.AffectedSOPClassUID = sel.SOPClassUID.Data;
            cStoreReq.Priority = 1;
            cStoreReq.Data = toSend;
            cStoreReq.MessageID = messageId;
            cStoreReq.AffectedSOPInstanceUID = sel.SOPInstanceUID.Data;
            cStoreReq.MoveOrigAETitle = _scu.ApplicationEntity.AeTitle;
            cStoreReq.MoveOrigMessageID = messageId;
            return cStoreReq;
        }
    }
}
