using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.IO
{
    public class DICOM2Beam
    {
        public static BeamMeta ParseDICOM(DICOMObject beamDCM, DICOMObject fxDCM, double totalBeamDose)
        {
            var beamMeta = new BeamMeta();
            var beam = beamDCM.GetSelector();
            beamMeta.Id = beam.BeamName?.Data;
            beamMeta.MachineId = beam.TreatmentMachineName?.Data;
            beamMeta.MLCType = beam.BeamType?.Data;
            beamMeta.HasBlock = beam.NumberOfBlocks?.Data != null && beam.NumberOfBlocks.Data > 0;
            beamMeta.HasBolus = beam.NumberOfBoli?.Data != null && beam.NumberOfBoli.Data > 0;
            beamMeta.IsSetup = beam.TreatmentDeliveryType?.Data == "SETUP";
            beamMeta.HasWedge = beam.NumberOfWedges?.Data != null && beam.NumberOfWedges.Data > 0;
            beamMeta.Wedge = beamMeta.HasWedge ? beam.WedgeSequence.Items.First().GetSelector().WedgeID.Data : string.Empty;
            switch (beam.RadiationType?.Data)
            {
                case "PHOTON": beamMeta.ModalityAbbr = "X"; break;
                case "ELECTRON": beamMeta.ModalityAbbr = "E"; break;
                case "PROTON": beamMeta.ModalityAbbr = "P"; break;
                case "NEUTRON": beamMeta.ModalityAbbr = "N"; break;
            }
            var cp = beam.ControlPointSequence?.Items.FirstOrDefault();
            if (cp != null)
            {
                beamMeta.SSD = cp.GetSelector().SourceToSurfaceDistance == null ? double.NaN : cp.GetSelector().SourceToSurfaceDistance.Data;
                beamMeta.Energy = cp.GetSelector().NominalBeamEnergy.Data;
                beamMeta.CollimatorAngle = cp.GetSelector().BeamLimitingDeviceAngle.Data;
                beamMeta.StartingGantryAngle = cp.GetSelector().GantryAngle.Data;
                var lastCP = beam.ControlPointSequence?.Items.LastOrDefault();
                if (lastCP?.GetSelector().GantryAngle != null)
                    beamMeta.EndingGantryAngle = lastCP.GetSelector().GantryAngle.Data;
                else
                {
                    beamMeta.EndingGantryAngle = beamMeta.StartingGantryAngle;
                }
                beamMeta.TableAngle = cp.GetSelector().PatientSupportAngle.Data;
                var iso = cp.GetSelector().IsocenterPosition.Data_;
                beamMeta.Isocenter = new Vector3(iso[0], iso[1], iso[2]);

                var posSQ = cp.GetSelector().BeamLimitingDevicePositionSequence;
                if (posSQ != null)
                {
                    foreach (var item in posSQ.Items)
                    {
                        if (item.GetSelector().RTBeamLimitingDeviceType?.Data == "ASYMX")
                        {
                            beamMeta.X1 = item.GetSelector().LeafJawPositions.Data_[0];
                            beamMeta.X2 = item.GetSelector().LeafJawPositions.Data_[1];
                        }
                        else if (item.GetSelector().RTBeamLimitingDeviceType?.Data == "ASYMY")
                        {
                            beamMeta.Y1 = item.GetSelector().LeafJawPositions.Data_[0];
                            beamMeta.Y2 = item.GetSelector().LeafJawPositions.Data_[1];
                        }
                        else if (item.GetSelector().RTBeamLimitingDeviceType?.Data == "X")
                        {
                            beamMeta.X1 = beamMeta.X2 = item.GetSelector().LeafJawPositions.Data;
                        }
                        else if (item.GetSelector().RTBeamLimitingDeviceType?.Data == "Y")
                        {
                            beamMeta.Y1 = beamMeta.Y2 = item.GetSelector().LeafJawPositions.Data;
                        }
                    }
                }
            }

            if (fxDCM != null)
            {
                beamMeta.Dose = fxDCM.GetSelector().BeamDose?.Data;
                beamMeta.MU = fxDCM.GetSelector().BeamMeterset?.Data;
                if (beamMeta.Dose.HasValue)
                {
                    var noRound = beamMeta.Dose.Value / totalBeamDose;
                    var weight = Math.Round(noRound, 2, MidpointRounding.AwayFromZero);
                    beamMeta.Weight = weight;
                }

            }

            return beamMeta;
        }
    }
}

