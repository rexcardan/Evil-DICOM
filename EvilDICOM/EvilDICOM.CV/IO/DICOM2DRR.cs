using OpenCvSharp;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.Helpers;
using EvilDICOM.CV.Drawing.Renderers;
using EvilDICOM.CV.RT;
using EvilDICOM.RT.Extensions;
using System;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.CV.IO
{
    public class DICOM2DRR
    {
        public static DRR ParseDICOM(string dcmFile)
        {
            var drr = new DRR();
            var dcm = DICOMObject.Read(dcmFile);

            if (!dcm.IsDRR()) { throw new Exception("DICOM object not DRR"); }

            var sel = dcm.GetSelector();

            //READ VOXELS TO MAT
            var pixels = SliceReader.ReadVoxels(dcm);

            var window = sel.WindowWidth.Data;
            var level = sel.WindowCenter.Data;

            using (var im = Mat.FromPixelData((int)sel.Rows.Data, (int)sel.Columns.Data, MatType.CV_32FC1, pixels.ToArray()))
            {
                drr.Image = im.WindowAndLevel(level, window).CvtColor(ColorConversionCodes.GRAY2BGR);
            }

            var orient = PatientPosition.FromAbbreviation(sel.PatientPosition.Data);

            FieldRenderer.Render(sel, drr);

            //DRAW ISO
            var iso = sel.IsocenterPosition?.Data_;
            if (iso != null)
            {
                var identity = MatMaker.Identity(4, 4);
                var iso3d = iso.ToPoint3f();
                var gantryAngle = sel.GantryAngle.Data;
                var collAngle = sel.BeamLimitingDeviceAngle.Data;
                var tableAngle = sel.PatientSupportAngle.Data;
                var dcm2IEC = Transform.DICOM2IEC(orient.Orientation);
                var tx = Transform.GantryTransform(gantryAngle);

                // iso3d = Transform.IECToDICOM(orient.Orientation).TransformPoint3f(isoDICOM_BEVCoord);
                var sid = sel.RTImageSID.Data;
                var mag = 1.0f;// (float)(sid / (sid - isoDICOM_BEVCoord.Z));

                var origin = new Point2f((float)sel.RTImagePosition.Data_[0], (float)sel.RTImagePosition.Data_[0]);

                var spacing = sel.ImagePlanePixelSpacing.Data_;

                // var isoDICOM_BEV = Transform.IECToDICOM(orient.Orientation).TransformPoint3f(isoIEC_BEV);
                GraticuleRenderer.Render(drr.Image, collAngle, spacing[0]);
                //drr.Image.Line(iso2D, pt2,
                //  new Scalar(255, 255, 0), 1, LineTypes.Link8);
                drr.Label = sel.RTImageLabel?.Data;

            }





            return drr;
        }
    }
}
