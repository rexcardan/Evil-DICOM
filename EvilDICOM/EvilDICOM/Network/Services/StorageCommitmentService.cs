using System;
using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Messaging;

namespace EvilDICOM.Network.Services
{
    public class StorageCommitmentService
    {
        private DIMSEService _dms;

        public StorageCommitmentService(DIMSEService dIMSEService)
        {
            this._dms = dIMSEService;
        }

        public void OnRequestReceived(NActionRequest req, Association asc)
        {
            //Find it
            var txId = req.Data.GetSelector().TransactionUID.Data;
            var found = new List<DICOMObject>();
            var toFind = req.Data.GetSelector().ReferencedSOPSequence;
            foreach(var item in toFind.Items)
            {
                var sopClass = item.GetSelector().ReferencedSOPClassUID.Data;
                var sopInstance = item.GetSelector().ReferencedSOPInstanceUID.Data;
                //If found, add to found list
                if(Find(sopClass, sopInstance))
                {
                    found.Add(item);
                }
            }
            //Send N-EVENT-REPORT-RQ
            var nReq = new NEventReportRequest();
            nReq.AffectedSOPClassUID = req.RequestedSOPClassUID;
            nReq.MessageID = req.MessageID;
            nReq.AffectedSOPInstanceUID = req.RequestedSOPInstanceUID;
            nReq.EventTypeId = req.ActionTypeID;
            var data = new DICOMObject(DICOMForge.TransactionUID(txId),
                DICOMForge.ReferencedSOPSequence(found.ToArray()));
            nReq.Data = data;
            asc.OutboundMessages.Enqueue(nReq);
        }

        private bool Find(string sopClass, string sopInstance)
        {
            //Always find for now
            //TODO Make Real
            return true;
        }
    }
}