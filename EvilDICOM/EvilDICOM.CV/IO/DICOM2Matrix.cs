using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;
using EvilDICOM.CV.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.CV.IO
{
    public class DICOM2Matrix
    {
        protected static void FillMetadata(Matrix mat, DICOMObject dcm) 
        {
            var sel = dcm.GetSelector();
            mat.XRes = sel.PixelSpacing.Data_[0];
            mat.YRes = sel.PixelSpacing.Data_[1];
            var _origin = sel.ImagePositionPatient.Data_;
            mat.Origin = new Vector3(_origin[0], _origin[1], _origin[2]);
            mat.DimensionX = sel.Columns.Data;
            mat.DimensionY = sel.Rows.Data;
            var bitsAllocated = sel.BitsAllocated.Data;
            var bitsStored = sel.BitsStored.Data;
            mat.BytesAllocated = bitsAllocated / 8;
            var orient = sel.Image​Orientation​Patient.Data_;
            var xDir = new Vector3(orient[0], orient[1], orient[2]);
            var yDir = new Vector3(orient[3], orient[4], orient[5]);
            var zDir = xDir.CrossMultiply(yDir);
            mat.ImageOrientation = (xDir, yDir, zDir);
        }
    }
}
