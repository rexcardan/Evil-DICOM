using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.DIMSE.Actions
{
    public class StorageCommitmentRequest : NActionRequest
    {
        public StorageCommitmentRequest()
        {
            this.ActionTypeID = 1;
            this.AffectedSOPClassUID = "1.2.840.10008.1.20.1";
            this.RequestedSOPClassUID = "1.2.840.10008.1.20.1";
            this.RequestedSOPInstanceUID = "1.2.840.10008.1.20.1.1";
        }

        protected UniqueIdentifier _transactionUID = new UniqueIdentifier { Tag = TagHelper.TransactionUID };

        public string TransactionUID
        {
            get { return _transactionUID.Data; }
            set { _transactionUID.Data = value; }
        }

        protected Sequence _referencedSOPSequence = new Sequence { Tag = TagHelper.ReferencedSOPSequence };

        public Dictionary<string, string> ReferenedSOPs
        {
            get
            {
                Dictionary<string, string> sops = new Dictionary<string, string>();
                _referencedSOPSequence.Items.ToList().ForEach(i =>
                     {
                         var sel = i.GetSelector();
                         sops.Add(sel.ReferencedSOPClassUID.Data, sel.ReferencedSOPInstanceUID.Data);
                     });
                return sops;
            }
            set
            {
                _referencedSOPSequence.Items.Clear();
                foreach (var entry in value)
                {
                    var dcm = new DICOMObject();
                    dcm.Add(DICOMForge.ReferencedSOPClassUID(entry.Value));
                    dcm.Add(DICOMForge.ReferencedSOPInstanceUID(entry.Key));
                    _referencedSOPSequence.Items.Add(dcm);
                }
            }
        }

        public override List<IDICOMElement> Elements
        {
            get
            {
                var actionAttributes = new List<IDICOMElement>();
                actionAttributes.Add(_transactionUID);
                actionAttributes.Add(_referencedSOPSequence);
                return base.Elements.Concat(actionAttributes).ToList();
            }
        }
    }
}
