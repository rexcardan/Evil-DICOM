using OpenCvSharp;
using EvilDICOM.Core;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.Helpers;
using EvilDICOM.CV.RT;
using EvilDICOM.RT.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.IO
{
    public class DICOM2DoseMatrix : DICOM2Matrix
    {
        public static DoseMatrix ParseDICOM(string dcmFile, double? prescribedDoseGy = null, bool convertToRelative = true)
        {
            var dcm = DICOMObject.Read(dcmFile);

            var matrix = new DoseMatrix();
            matrix.PrescriptionDoseGy = prescribedDoseGy;

            FillMetadata(matrix, dcm);

            //SET METADATA
            var sel = dcm.GetSelector();
            matrix.ZRes = sel.GridFrameOffsetVector.Data_[1] - sel.GridFrameOffsetVector.Data_[0];
            var offsets = sel.GridFrameOffsetVector.Data_;
            matrix.DoseUnit = sel.Dose​Units.Data == "GY" ? DoseUnit.ABSOLUTE :
                DoseUnit.RELATIVE;
            var sumType = sel.DoseSummationType.Data;
            matrix.SumType = sumType == "PLAN" ? DoseSumType.PLAN : DoseSumType.BEAM;
            matrix.PlanUID = dcm.GetSelector().ReferencedRTPlanSequence?.Items.FirstOrDefault()?
                .GetSelector().ReferencedSOPInstanceUID.Data;
            matrix.DimensionZ = sel.NumberOfFrames.Data;

            //FILL VOXELS
            Func<BinaryReader, float> valueConverter = null;
            switch (matrix.BytesAllocated)
            {
                case 1: valueConverter = (br) => (int)br.ReadByte(); break;
                case 2: valueConverter = (br) => br.ReadInt16(); break;
                case 4: valueConverter = (br) => br.ReadInt32(); break;
                case 8: valueConverter = (br) => br.ReadInt64(); break;
            }

            var m = dcm.GetSelector().DoseGridScaling.Data;
            if (convertToRelative && matrix.DoseUnit != DoseUnit.RELATIVE && prescribedDoseGy.HasValue)
            {
                m = m / prescribedDoseGy.Value;
                matrix.DoseUnit = DoseUnit.RELATIVE;
            }

            var values = new List<float>();
            using (BinaryReader br = new BinaryReader(dcm.GetPixelStream()))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                    values.Add((float)(m * valueConverter(br)));
            }

            //CALCULATE DOSE STATS
            matrix.MaxDose = values.Max();
            var indexOfMax = values.IndexOf(matrix.MaxDose);
            var (_, _, z) = IndexHelper.IndexToLatticeXYZ(indexOfMax, matrix.DimensionX, matrix.DimensionY);
            matrix.MaxDoseSlice = z;
            matrix.CreateMatrix(values.ToArray());
            return matrix;
        }
    }
}
