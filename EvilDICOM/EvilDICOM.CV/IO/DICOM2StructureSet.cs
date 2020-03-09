using OpenCvSharp;
using EvilDICOM.Core;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.RT.Meta;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using EvilDICOM.Core.Logging;

namespace EvilDICOM.CV.IO
{
    public class DICOM2StructureSet
    {
        static ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<DICOM2StructureSet>();
        public static StructureSetMeta ParseDICOM(string file)
        {
            var sm = new StructureSetMeta();
            var dcm = DICOMObject.Read(file);
            var sel = dcm.GetSelector();

            var metas = sel.StructureSetROISequence.Items.Select(i =>
            {
                var meta = new StructureMeta();
                meta.StructureId = i.GetSelector().ROIName?.Data;
                meta.ROINumber = i.GetSelector().ROINumber.Data;
                return meta;
            });

            foreach (var meta in metas)
            {
                try
                {
                    var comatch = sel.ROIContourSequence.Items.FirstOrDefault(i => i.GetSelector().ReferencedROINumber.Data == meta.ROINumber);
                    var romatch = sel.RTROIObservationsSequence.Items.FirstOrDefault(i => i.GetSelector().ReferencedROINumber.Data == meta.ROINumber);

                    var colorValues = comatch.GetSelector().ROIDisplayColor.Data_;
                    var color = new Vec3b((byte)colorValues[0], (byte)colorValues[1], (byte)colorValues[2]);
                    var dicomType = romatch.GetSelector().RTROIInterpretedType.Data;
                    var name = romatch.GetSelector().ROIObservationLabel.Data;
                    meta.StructureName = name;
                    meta.Color = new Scalar(colorValues[0], colorValues[1], colorValues[2]);

                    var hasContours = comatch.GetSelector().ContourSequence != null;
                    if (!hasContours) { continue; }

                    //HAS CONTOURS - SET COLOR BYTES IN MATRIX
                    foreach (var slice in comatch.GetSelector().ContourSequence.Items)
                    {
                        var contours = slice.GetSelector().ContourData.Data_;
                        if (contours.Count % 3 != 0)
                        {
                            _logger.LogWarning($"Slice for structure {meta.StructureId} has {contours.Count} contour points. Not divisible by 3! Can't process."); continue;
                        }
                        try
                        {
                            var contour = new SliceContourMeta();
                            for (int i = 0; i < contours.Count; i += 3)
                            {
                                var contourPt = new OpenCvSharp.Point3f((float)contours[i + 0], (float)contours[i + 1], (float)contours[i + 2]);
                                contour.AddPoint(contourPt);
                            }
                            meta.SliceContours.Add(contour);
                            meta.DICOMType = dicomType;
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.ToString());
                        }
                    }

                    //OrganizeContours - contours containing other contours (holes and fills) will be organized
                    //into children. All other contours are outermost contours and not children of any other
                    var slices = meta.SliceContours.GroupBy(s => s.Z).ToList();
                    foreach (var slice in slices)
                    {
                        var sliceContours = slice.OrderByDescending(s => s.CalculateArea()).ToList();
                        ContourHelper.OrganizeIntoChildren(sliceContours[0], sliceContours.Skip(1));
                    }
                    sm.Structures.Add(meta.StructureId, meta);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Could not add structure {meta.StructureId}");
                }

            }
            return sm;
        }
    }
}
