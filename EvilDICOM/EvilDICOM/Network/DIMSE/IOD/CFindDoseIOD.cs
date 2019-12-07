
using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindDoseIOD : CFindInstanceIOD
    {
        public CFindDoseIOD() : base()
        {
            ContentDate = null;
            ContentTime = null;
            DoseSummationType = string.Empty;
            ReferencedPlan = (string.Empty, string.Empty);
        }

        public CFindDoseIOD(DICOMObject dcm) : base(dcm)
        {
        }

        public S.DateTime? Content​Date
        {
            get { return _sel.Content​Date != null ? _sel.Content​Date.Data : null; }
            set { _sel.Forge(DF.Content​Date(value)); }
        }

        public S.DateTime? ContentTime
        {
            get { return _sel.ContentTime != null ? _sel.ContentTime.Data : null; }
            set { _sel.Forge(DF.ContentTime(value)); }
        }

        public string DoseSummationType
        {
            get { return _sel.DoseSummationType != null ? _sel.DoseSummationType.Data : null; }
            set { _sel.Forge(DF.DoseSummationType(value)); }
        }

        public (string sopClassUID, string sopInstanceUID) ReferencedPlan
        {
            get
            {
                return _sel.ReferencedRTPlanSequence != null ?
                  (_sel.ReferencedRTPlanSequence.Select(seq => seq.ReferencedSOPClassUID.Data),
                 _sel.ReferencedRTPlanSequence.Select(seq => seq.ReferencedSOPInstanceUID.Data))
                  : (string.Empty, string.Empty);
            }
            set
            {
                _sel.Forge(DF.ReferencedRTPlanSequence(new DICOMObject(
                   DF.ReferencedSOPClassUID(value.sopClassUID),
                   DF.ReferencedSOPInstanceUID(value.sopInstanceUID)
              )));
            }
        }
    }
}
