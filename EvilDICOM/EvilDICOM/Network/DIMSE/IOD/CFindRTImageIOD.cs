using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindRTImageIOD : CFindInstanceIOD
    {
        public CFindRTImageIOD() : base()
        {
            ReferencedBeamNumber = null;
            ReferencedPlan = (string.Empty, string.Empty);
        }

        public CFindRTImageIOD(DICOMObject dcm) : base(dcm)
        {

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

        public int? ReferencedBeamNumber
        {
            get { return _sel.ReferencedBeamNumber != null ? _sel.ReferencedBeamNumber.Data : int.MinValue; }
            set
            {
                var convertedValue = value.HasValue ? new int[value.Value] : new int[0];
                _sel.Forge(DF.ReferencedBeamNumber(convertedValue));
            }
        }
    }
}
