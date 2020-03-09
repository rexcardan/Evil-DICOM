using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Selection;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = EvilDICOM.RT.Data;
using EvilDICOM.RT.Data;
using EvilDICOM.CV.Image;
using EvilDICOM.CV.RT.Meta;
using EvilDICOM.CV.Geometry;
using EvilDICOM.CV.Geometry.Isosurfaces;

namespace EvilDICOM.CV.RT
{
    public class DoseMatrix : Matrix
    {
        internal DoseMatrix() { }

        public float MaxDose { get; set; }
        public int MaxDoseSlice { get; set; }
        public string PlanUID { get; set; }
        public DoseSumType SumType { get; set; }
        public DoseUnit DoseUnit { get; set; }

        public double? PrescriptionDoseGy { get; set; }

        public void ConvertUnits(DoseUnit desiredUnits)
        {
            if (!PrescriptionDoseGy.HasValue) { return; }

            if (desiredUnits == D.DoseUnit.ABSOLUTE && DoseUnit == D.DoseUnit.RELATIVE)
            {
                _mat = PrescriptionDoseGy.Value * _mat;
                MaxDose *= (float)PrescriptionDoseGy.Value;
                DoseUnit = D.DoseUnit.ABSOLUTE;
            }
            else if (desiredUnits == D.DoseUnit.RELATIVE && DoseUnit == D.DoseUnit.ABSOLUTE)
            {
                _mat = 1 / PrescriptionDoseGy.Value * _mat;
                MaxDose /= (float)PrescriptionDoseGy.Value;
                DoseUnit = D.DoseUnit.RELATIVE;
            }
        }

        /// <summary>
        /// Zeros all voxels outside of structure contours
        /// </summary>
        /// <param name="sm"></param>
        //public DoseMatrix MaskMatrixToStructure(StructureMeta sm)
        //{
        //    var minZSlice = sm.SliceContours.Select(s => s.Z).Min();
        //    var maxZSlice = sm.SliceContours.Select(s => s.Z).Max();
        //    var sliceThickness = sm.GetSliceThickness();
        //    for (int z = 0; z < DimensionZ; z++)
        //    {
        //        var slice = GetZPlaneBySlice(z);
        //        using (var mask = new Mat(slice.Rows, slice.Cols, MatType.CV_8UC1, new Scalar(0)))
        //        {
        //            if(sm.SliceContours.Any(s=>s.Z==z))
        //            //This method will mask and exclude holes and include fills
        //            contour.MaskImageFast(mask, dm.PatientTransformMatrix, 255, scale);
        //            dvh.AddSliceToDVH(zDose, mask);
        //        }
        //        SetZPlaneBySlice(slice, z);
        //    }
        //}

        public DVH CalcDVH()
        {
            double min, max;
            _mat.MinMaxIdx(out min, out max);
            var voxelCC = XRes / 10 * YRes / 10 * ZRes / 10;
            var doseUnit = DoseUnit == DoseUnit.ABSOLUTE ? "Gy" : "%";
            var dvh = new DVH(max, voxelCC, doseUnit);

            for (int z = 0; z < DimensionZ; z++)
            {
                var slice = GetZPlaneBySlice(z);
                dvh.AddSliceToDVH(slice, null);
            }
          
            return dvh;
        }

        public DoseMatrix Clone()
        {
            var dm = new DoseMatrix();
            dm._mat = _mat.Clone();
            dm.XRes = XRes;
            dm.YRes = YRes;
            dm.ZRes = ZRes;
            dm.MaxDose = MaxDose;
            dm.MaxDoseSlice = MaxDoseSlice;
            dm.ImageOrientation = ImageOrientation;
            dm.BytesAllocated = BytesAllocated;
            dm.DimensionX = DimensionX;
            dm.DimensionY = DimensionY;
            dm.DimensionZ = DimensionZ;
            dm.DoseUnit = DoseUnit;
            dm.Origin = Origin;
            dm.PlanUID = PlanUID;
            dm.PrescriptionDoseGy = PrescriptionDoseGy;
            dm.SumType = SumType;
            dm.CalculatePatientTransformMatrix();
            dm.CalculateBounds();
            return dm;
        }
    }
}


