using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindIonBeamsRecord : CFindTreatmentRecordIOD
    {
        public CFindIonBeamsRecord() : base()
        {

        }

        public CFindIonBeamsRecord(DICOMObject dcm) : base(dcm)
        {

        }

        public int ReferencedBeamNumber
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.ReferencedBeamNumber.Data)
                  : int.MinValue;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedBeamNumber(value));
            }
        }

        public int CurrentFractionNumber
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.CurrentFractionNumber.Data)
                  : int.MinValue;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedBeamNumber(value));
            }
        }

        public string TreatmentDeliveryType
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.TreatmentDeliveryType.Data)
                  : string.Empty;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentDeliveryType(value));
            }
        }

        public string TreatmentTerminationStatus
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.TreatmentTerminationStatus.Data)
                  : string.Empty;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentTerminationStatus(value));
            }
        }

        public double DeliveredPrimaryMeterset
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.DeliveredPrimaryMeterset.Data)
                  : double.NaN;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.DeliveredPrimaryMeterset(value));
            }
        }

        public DateTime? TreatmentDateSQ
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.TreatmentDate.Data)
                  : null;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentDate(value));
            }
        }

        public DateTime? TreatmentTimeSQ
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq => seq.TreatmentTime.Data)
                  : null;
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.TreatmentTime(value));
            }
        }

        public (int doseReferenceNumber, double doseValue) CalculatedDose
        {
            get
            {
                return _sel.TreatmentSessionIonBeamSequence?
                    .Select(s => s.ReferencedCalculatedDoseReferenceSequence) != null ?
                  _sel.TreatmentSessionIonBeamSequence.Select(seq =>
                        seq.ReferencedCalculatedDoseReferenceSequence
                         .Select(s => (s.ReferencedCalculatedDoseReferenceNumber.Data,
                          s.CalculatedDoseReferenceDoseValue.Data)))
                  : (int.MinValue, double.NaN);
            }
            set
            {
                CreateBaseSequenceIfNotExists();

                _sel.TreatmentSessionIonBeamSequence.Items
                    .FirstOrDefault()
                    .ReplaceOrAdd(DF.ReferencedCalculatedDoseReferenceSequence(
                        new DICOMObject(
                        DF.ReferencedCalculatedDoseReferenceNumber(value.doseReferenceNumber),
                         DF.CalculatedDoseReferenceDoseValue(value.doseValue))));
            }
        }


        private void CreateBaseSequenceIfNotExists()
        {
            if (_sel.TreatmentSessionIonBeamSequence?.Items
                .FirstOrDefault() == null)
            {
                _sel.TreatmentSessionIonBeamSequence = DF.TreatmentSessionIonBeamSequence(new DICOMObject());
            }
        }
    }
}
