using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.CV.RT.Meta;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvilDICOM.CV.IO
{
    public class DICOM2Plan
    {
        static ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<DICOM2Plan>();

        public static PlanMeta ParseDICOM(string dicomPath)
        {
            _logger.LogTrace($"Reading plan {dicomPath}...");
            var planDCM = DICOMObject.Read(dicomPath);
            var meta = new PlanMeta();
            var sel = planDCM.GetSelector();
            _logger.LogTrace($"Reading RTPlanLabel = {sel.RTPlanLabel.Data}...");
            meta.PlanId = sel.RTPlanLabel.Data;
            _logger.LogTrace($"Reading SoftwareVersions = {sel.SoftwareVersions?.Data}...");
            meta.SoftwareVersion = sel.SoftwareVersions?.Data;
            var regex = new Regex(Regex.Escape("^"));
            _logger.LogTrace($"Reading PatientID = {sel.PatientID?.Data}...");
            meta.PatientId = sel.PatientID?.Data;
            _logger.LogTrace($"Reading Patient Name = {regex.Replace(sel.PatientName?.Data, ",", 1).Replace("^", " ")}...");
            meta.PatientName = regex.Replace(sel.PatientName?.Data, ",", 1).Replace("^", " ");
            _logger.LogTrace($"Reading NumFractions = {sel.FractionGroupSequence?.Select(d => d.NumberOfFractionsPlanned)?.Data}...");
            meta.NumFractions = sel.FractionGroupSequence?.Select(d => d.NumberOfFractionsPlanned)?.Data;
            _logger.LogTrace($"Reading SOPInstanceUID = {sel.SOPInstanceUID.Data}...");
            meta.SOPInstanceUID = sel.SOPInstanceUID.Data;
            _logger.LogTrace($"Reading SeriesUID = {sel.SeriesInstanceUID.Data}...");
            meta.SeriesUID = sel.SeriesInstanceUID.Data;

            var dose = sel.DoseReferenceSequence?.Items?.FirstOrDefault()?.GetSelector().TargetPrescriptionDose != null ?
                       sel.DoseReferenceSequence?.Items?.FirstOrDefault()?.GetSelector().TargetPrescriptionDose.Data :
                       sel.DoseReferenceSequence?.Items?.FirstOrDefault()?.GetSelector().DeliveryMaximumDose?.Data;
            _logger.LogTrace($"Reading PrescribedDose = {dose}...");
            meta.PrescribedDose = dose;

            var sset = sel.ReferencedStructureSetSequence.Items?.FirstOrDefault()?.GetSelector().ReferencedSOPInstanceUID?.Data;
            _logger.LogTrace($"Reading StructureSetUID = {sset}...");
            meta.StructureSetUID = sset;

            if (sel.BeamSequence != null)
            {
                _logger.LogTrace($"Reading BeamSequence...");
                var fxSequence = sel.FractionGroupSequence.Items.SelectMany(i => i.GetSelector().ReferencedBeamSequence?.Items);
                var totalBeamDose = fxSequence.Where(f => f.GetSelector().BeamDose != null).
                    Sum(f => f.GetSelector().BeamDose.Data);

                _logger.LogTrace($"Iterating beams...");
                foreach (var beam in sel.BeamSequence.Items)
                {
                    _logger.LogTrace($"Beam {beam.GetSelector().BeamNumber.Data}...");
                    var fx = fxSequence.FirstOrDefault(f => f.GetSelector().ReferencedBeamNumber.Data == beam.GetSelector().BeamNumber.Data);
                    meta.Beams.Add(DICOM2Beam.ParseDICOM(beam, fx, totalBeamDose));
                }
            }
            if (sel.DoseReferenceSequence != null)
            {
                _logger.LogTrace($"Reading DoseReference...");
                foreach (var pt in sel.DoseReferenceSequence.Items)
                {
                    var rpMeta = new ReferencePointMeta();
                    rpMeta.Id = pt.GetSelector().DoseReferenceDescription?.Data;
                    if (pt.GetSelector().DoseReferencePointCoordinates != null)
                    {
                        var cood = pt.GetSelector().DoseReferencePointCoordinates.Data_;
                        rpMeta.Location = new Vector3(cood[0], cood[1], cood[2]);
                    }
                    else
                    {
                        rpMeta.Location = new Vector3(double.NaN, double.NaN, double.NaN);
                    }
                    var totalDose = pt.GetSelector().TargetMaximumDose?.Data;
                    if (totalDose == null || !meta.NumFractions.HasValue) { totalDose = double.NaN; }
                    else { rpMeta.FractionDoseGy = (totalDose.Value) / meta.NumFractions.Value; }

                    if (pt.GetSelector().DoseReferenceType != null)
                    {
                        rpMeta.PointType = pt.GetSelector().DoseReferenceType.Data;
                    }
                    else
                    {
                        rpMeta.PointType = "Unknown";
                    }

                    meta.ReferencesPoints.Add(rpMeta);
                }
            }
            return meta;
        }
    }
}
