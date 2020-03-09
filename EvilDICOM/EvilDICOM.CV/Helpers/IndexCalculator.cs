using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using EvilDICOM.CV.Drawing;
using EvilDICOM.CV.Extensions;
using EvilDICOM.CV.RT;
using EvilDICOM.CV.RT.Meta;

namespace EvilDICOM.CV.Helpers
{
    public class IndexCalculator
    {

        public static TargetIndices CalculateIndices(StructureMeta gtv, DoseMatrix dd, double prescriptionDose)
        {
            var cropped = dd.Clone();
            cropped.CropMatrixToStructure(gtv, 20);

            var presVol = cropped.ConvertIsodoseLevelToMesh(prescriptionDose).CalculateVolumeCC();
            var halfVol = cropped.ConvertIsodoseLevelToMesh(prescriptionDose / 2).CalculateVolumeCC();

            var dvh = cropped.CalcDVH();
            var points = dvh.GetDVHData().Points;
            var test = points.GetVolumeAtDose(prescriptionDose);
            var dosevol = dvh.GetDVHData().Points.GetVolumeAtDose(prescriptionDose) / 100 * cropped.VolumeCC();
            var gradVol = dvh.GetDVHData().Points.GetVolumeAtDose(prescriptionDose / 2) / 100 * cropped.VolumeCC();
            var isodoseDef = new IsodoseLevel() { Value = prescriptionDose, Color = new Scalar(0, 0, 255) };
            var isoStructure = cropped.Find2DIsodoseLines(isodoseDef).First();
            isoStructure.Look = StructureLooks.Purple;
            //var vol = isoStructure.CalculateVolumeCC();
            //var gtvVol = gtv.CalculateVolumeCC();
            var ci = presVol / 5;// gtvVol;
            //var ci2 = tvol / gtvVol;
            var gi = halfVol / dosevol;
            return new TargetIndices()
            {
                Gradient = gi,
                RTOGCI = ci,

            };
        }
    }
}
