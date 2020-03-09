using EvilDICOM.Core;
using EvilDICOM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvilDICOM.Core.IO.Reading
{
    public class SliceReader
    {
        public static List<float> ReadVoxels(DICOMObject dcm)
        {
            var sel = dcm.GetSelector();
            var rows = sel.Rows.Data;
            var cols = sel.Columns.Data;

            var bitsAllocated = sel.BitsAllocated.Data;
            var bitsStored = sel.BitsStored.Data;
            var bytesAllocated = bitsAllocated / 8;

            var voxels = new List<float>();

            Func<byte[], float> valueConverter = null;
            switch (bytesAllocated)
            {
                case 1: valueConverter = (bytes) => (int)bytes[0]; break;
                case 2: valueConverter = (bytes) => BitConverter.ToInt16(bytes, 0); break;
                case 4: valueConverter = (bytes) => BitConverter.ToInt32(bytes, 0); break;
                case 8: valueConverter = (bytes) => BitConverter.ToInt64(bytes, 0); break;
            }

            var m = (float)sel.RescaleSlope.Data; //Slope
            var b = (float)sel.RescaleIntercept.Data; //Intercept

            using (BinaryReader br = new BinaryReader(dcm.GetPixelStream()))
            {
                var bytes = new byte[bytesAllocated];
                while (br.Read(bytes, 0, bytesAllocated) != 0)
                {
                    var val = m * valueConverter(bytes) + b;
                    voxels.Add(val);
                }
            }

            return voxels;
        }
        public static List<float> ReadVoxels(string filePath)
        {
            var dcm = DICOMObject.Read(filePath);
            return ReadVoxels(dcm);
        }
    }
}
