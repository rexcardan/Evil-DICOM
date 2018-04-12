using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.DIMSE.Actions;
using EvilDICOM.Network.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EvilDICOM.Network.Services.DIMSEService;

namespace EvilDICOM.Network.SCUOps
{
    public class StorageVerifier
    {
        private DICOMSCU _scu;
        private Entity callingEntity;
        ushort _messageId = 1;

        public StorageVerifier(DICOMSCU dICOMSCU, Entity callingEntity)
        {
            this._scu = dICOMSCU;
            this.callingEntity = callingEntity;
        }

        public List<bool> VerifyStorage(Dictionary<string, string> dictionary)
        {
            var results = new List<bool>();
            var request = CreateRequest(dictionary);
            request.MessageID = _messageId;
            _messageId += 3;
            System.DateTime lastContact = System.DateTime.Now;
            int msWait = 10000;

            var mr = new ManualResetEvent(false);
            NEventReportRequest req = null;
            var cr = new Services.DIMSEService.DIMSERequestHandler<NEventReportRequest>((res, asc) =>
            {
                lastContact = System.DateTime.Now;
                var origTxId = request.Data.GetSelector().TransactionUID.Data;
                if (res.Data.GetSelector().TransactionUID.Data == origTxId)
                {
                    var refSeq = res.Data.GetSelector().ReferencedSOPSequence;
                    Dictionary<string, string> refSeqDictionary = new Dictionary<string, string>();
                    refSeq.Items.ToList().ForEach(i =>
                    {
                        refSeqDictionary.Add(i.GetSelector().ReferencedSOPClassUID.Data,
                            i.GetSelector().ReferencedSOPInstanceUID.Data);
                    });
                    foreach (var entry in dictionary)
                    {
                        results.Add(refSeqDictionary.Contains(entry));
                    }
                }
                req = res;
                mr.Set();
            });

            _scu.DIMSEService.Subscribe(cr);
            _scu.SendMessage(request, callingEntity);
            mr.WaitOne(msWait);
            _scu.DIMSEService.Unsubscribe(cr);
            return results;

        }

        private static StorageCommitmentRequest CreateRequest(Dictionary<string, string> dictionary)
        {
            var scr = new StorageCommitmentRequest();
            var refSeq = DICOMForge.ReferencedPerformedProcedureStepSequence(
                new DICOMObject(DICOMForge.ReferencedSOPClassUID("1.2.840.10008.3.1.2.3.3"),
                DICOMForge.ReferencedSOPInstanceUID(UIDHelper.GenerateUID())));
            var txId = DICOMForge.TransactionUID(UIDHelper.GenerateUID());
            var seqItems = new List<DICOMObject>();
            foreach (var entry in dictionary)
            {
                seqItems.Add(new DICOMObject(DICOMForge.ReferencedSOPClassUID(entry.Key),
                    DICOMForge.ReferencedSOPInstanceUID(entry.Value)));
            }
            var refSeq2 = DICOMForge.ReferencedSOPSequence(seqItems.ToArray());
            scr.Data = new DICOMObject(refSeq, txId, refSeq2);
            return scr;
        }
    }
}
