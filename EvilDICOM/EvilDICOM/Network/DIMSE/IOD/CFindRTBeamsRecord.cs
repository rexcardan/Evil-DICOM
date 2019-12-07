using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using System.Linq;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindRTBeamsRecord : CFindTreatmentRecordIOD
    {
        public CFindRTBeamsRecord() : base()
        {

        }

        public CFindRTBeamsRecord(DICOMObject dcm) : base(dcm)
        {

        }

        public int ReferencedBeamNumber
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.ReferencedBeamNumber.Data)
                  : int.MinValue;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedBeamNumber(value));
            }
        }

        public int CurrentFractionNumber
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.CurrentFractionNumber.Data)
                  : int.MinValue;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedBeamNumber(value));
            }
        }

        public string TreatmentDeliveryType
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.TreatmentDeliveryType.Data)
                  : string.Empty;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentDeliveryType(value));
            }
        }

        public string TreatmentTerminationStatus
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.TreatmentTerminationStatus.Data)
                  : string.Empty;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentTerminationStatus(value));
            }
        }

        public double DeliveredPrimaryMeterset
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.DeliveredPrimaryMeterset.Data)
                  : double.NaN;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.DeliveredPrimaryMeterset(value));
            }
        }

        public DateTime? TreatmentDateSQ
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.TreatmentDate.Data)
                  : null;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentDate(value));
            }
        }

        public DateTime? TreatmentTimeSQ
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq => seq.TreatmentTime.Data)
                  : null;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentTime(value));
            }
        }

        public (int doseReferenceNumber, double doseValue) CalculatedDose
        {
            get
            {
                return _sel.TreatmentSessionBeamSequence?
                    .Select(s => s.ReferencedCalculatedDoseReferenceSequence) != null ?
                  _sel.TreatmentSessionBeamSequence.Select(seq =>
                        seq.ReferencedCalculatedDoseReferenceSequence
                         .Select(s => (s.ReferencedCalculatedDoseReferenceNumber.Data,
                          s.CalculatedDoseReferenceDoseValue.Data)))
                  : (int.MinValue, double.NaN);
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedCalculatedDoseReferenceSequence(
                        new DICOMObject(
                        DF.ReferencedCalculatedDoseReferenceNumber(value.doseReferenceNumber),
                         DF.CalculatedDoseReferenceDoseValue(value.doseValue))));
            }
        }


        private void CreateBaseSequenceIfNotExists()
        {
            if (_sel.TreatmentSessionBeamSequence?.Items
                .FirstOrDefault() == null)
            {
                _sel.TreatmentSessionBeamSequence = DF.TreatmentSessionBeamSequence(new DICOMObject());
            }
        }
    }
}
