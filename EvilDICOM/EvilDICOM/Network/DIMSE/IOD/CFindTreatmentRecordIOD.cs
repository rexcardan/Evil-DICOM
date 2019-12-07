using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindTreatmentRecordIOD : CFindInstanceIOD
    {
        public CFindTreatmentRecordIOD() :base()
        {
            TreatmentDate = null;
            TreatmentTime = null;
            ReferencedFractionGroupNumber = null;
            ReferencedPlan = (string.Empty, string.Empty);
        }

        public CFindTreatmentRecordIOD(DICOMObject dcm) : base(dcm)
        {

        }

        public DateTime? TreatmentDate
        {
            get { return _sel.TreatmentDate != null ? _sel.TreatmentDate.Data : null; }
            set { _sel.Forge(DF.TreatmentDate(value)); }
        }

        public DateTime? TreatmentTime
        {
            get { return _sel.TreatmentTime != null ? _sel.TreatmentTime.Data : null; }
            set { _sel.Forge(DF.TreatmentTime(value)); }
        }

        public int? ReferencedFractionGroupNumber
        {
            get { return _sel.ReferencedFractionGroupNumber != null ? _sel.ReferencedFractionGroupNumber.Data : int.MinValue; }
            set
            {
                var convertedValue = value.HasValue ? new int[value.Value] : new int[0];
                _sel.Forge(DF.ReferencedFractionGroupNumber(convertedValue));
            }
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
