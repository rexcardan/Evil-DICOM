using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core
{
    public abstract class DICOMObjectWrapper
    {
        protected DICOMObject _dcm;
        public DICOMObjectWrapper(DICOMObject dicom)
        {
            this._dcm = dicom;
        }
    }
}
