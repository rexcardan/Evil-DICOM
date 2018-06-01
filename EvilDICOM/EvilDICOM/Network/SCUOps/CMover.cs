using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.IOD;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.SCUOps
{
    public class CMover : IDisposable
    {
        private DICOMSCU _scu;
        private Entity callingEntity;

        public CMover(DICOMSCU dICOMSCU, Entity callingEntity) 
        {
            this._scu = dICOMSCU;
            this.callingEntity = callingEntity;
            this._scu.DIMSEService.CMoveResponseReceived += new DIMSEService.DIMSEResponseHandler<CMoveResponse>(this.DIMSEService_CMoveResponseReceived);
        }

        /// <summary>
        /// Emits a CMove operation to an entity which moves an image from the entity to the specified AETitle
        /// </summary>
        /// <param name="scp">the provider which will perform the move</param>
        /// <param name="sopUid">the uid of the image to be moved</param>
        /// <param name="patientId">the patient id of the image</param>
        /// <param name="toAETite">the entity title which will receive the image</param>
        /// <param name="msgId">the message id</param>
        /// <returns>the move response</returns>
        public CMoveResponse SendCMove(CFindImageIOD iod, string toAETite, ref ushort msgId)
        {
            var result = new CMoveIOD
            {
                QueryLevel = QueryLevel.IMAGE,
                SOPInstanceUID = iod.SOPInstanceUID,
                PatientId = iod.PatientId,
                StudyInstanceUID = iod.StudyInstanceUID,
                SeriesInstanceUID = iod.SeriesInstanceUID
            };
            var request = new CMoveRequest(result, toAETite, Root.STUDY, Core.Enums.Priority.MEDIUM, msgId);
            return _scu.GetResponse<CMoveResponse, CMoveRequest>(request, callingEntity, ref msgId);
        }

        public CMoveResponse SendCMove(CFindSeriesIOD iod, string toAETite, ref ushort msgId)
        {
            var result = new CMoveIOD
            {
                QueryLevel = QueryLevel.SERIES,
                PatientId = iod.PatientId,
                StudyInstanceUID = iod.StudyInstanceUID,
                SeriesInstanceUID = iod.SeriesInstanceUID
            };
            var request = new CMoveRequest(result, toAETite, Root.STUDY, Core.Enums.Priority.MEDIUM, msgId);
            return _scu.GetResponse<CMoveResponse, CMoveRequest>(request, callingEntity, ref msgId);
        }

        public CMoveResponse SendCMove(CFindStudyIOD iod, string toAETite, ref ushort msgId)
        {
            var result = new CMoveIOD
            {
                QueryLevel = QueryLevel.STUDY,
                PatientId = iod.PatientId,
                StudyInstanceUID = iod.StudyInstanceUID,
            };
            var request = new CMoveRequest(result, toAETite, Root.STUDY, Core.Enums.Priority.MEDIUM, msgId);
            return _scu.GetResponse<CMoveResponse, CMoveRequest>(request, callingEntity, ref msgId);
        }

        private void DIMSEService_CMoveResponseReceived(CMoveResponse req, Association asc)
        {
            if ((int)req.Status != 65280)
                return;
            this.OnProgressUpdated((int)req.NumberOfCompletedOps, (int)req.NumberOfRemainingOps, (int)req.NumberOfFailedOps, (int)req.NumberOfWarningOps);
        }

        public CMover.ProgressUpdatedHandler ProgressUpdated;

        private void OnProgressUpdated(int completed, int remaining, int failed, int warning)
        {
            CMover.ProgressUpdatedHandler progressUpdated = this.ProgressUpdated;
            if (progressUpdated == null)
                return;
            int completed1 = completed;
            int remaining1 = remaining;
            int failed1 = failed;
            int warning1 = warning;
            progressUpdated(completed1, remaining1, failed1, warning1);
        }

        public void Dispose()
        {
            this._scu.DIMSEService.CMoveResponseReceived -= new DIMSEService.DIMSEResponseHandler<CMoveResponse>(this.DIMSEService_CMoveResponseReceived);
        }

        public delegate void ProgressUpdatedHandler(int completed, int remaining, int failed, int warning);
    }
}
