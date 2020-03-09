using OpenCvSharp;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.CV.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.IO
{
    public class DICOM2ImageMatrix : DICOM2Matrix
    {
        public static ImageMatrix ParseDICOM(IEnumerable<string> dcmFiles)
        {
            try
            {
                var matrix = new ImageMatrix();
                var fileImageNumbers = new List<(string fileName, int imageSlice, double z)>();

                foreach (var file in dcmFiles)
                {
                    var dcm = DICOMObject.Read(file);
                    var sel = dcm.GetSelector();
                    var seriesUID = sel.SeriesInstanceUID.Data;
                    var num = sel.InstanceNumber.Data;
                    var z = dcm.GetSelector().ImagePositionPatient.Data_[2];
                    fileImageNumbers.Add((file, num, z));
                }

                var ordered = fileImageNumbers.OrderBy(f => f.z);
                matrix.DimensionZ = ordered.Count();

                var values = new List<float>();
                //Load Values
                foreach (var o in ordered)
                {
                    var dcm = DICOMObject.Read(o.fileName);
                    var minSliceNum = ordered.Min(or => or.imageSlice);
                    if (o.imageSlice == minSliceNum)
                    {
                        FillMetadata(matrix, dcm);
                        matrix.ZRes = dcm.GetSelector().SliceThickness.Data;
                    }
                    var voxels = SliceReader.ReadVoxels(o.fileName);
                    values.AddRange(voxels);
                }

                //Set origin to minimum Z
                matrix.Origin = new Vector3(matrix.Origin.X, matrix.Origin.Y, ordered.First().z);

                matrix.CreateMatrix(values.ToArray());

                return matrix;
            }

            catch (Exception e)
            {
                return null;
            }

        }
    }
}
