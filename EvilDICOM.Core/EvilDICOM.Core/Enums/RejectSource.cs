using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum RejectSource : byte
    {
        DICOM_UL_SERVICE_USER = 1,
        DICOM_UL_SERVICE_PROVIDER_ACSE = 2,
        DICOM_UL_SERVICE_PROVIDER_PRESENTATION = 3,
    }
}
