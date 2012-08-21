using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum AbortSource : byte
    {
        DICOM_UL_SERV_USER = 0x00,
        RESERVED = 0x01,
        DICOM_UL_SERV_PROVIDER = 0x02
    }
}
