using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.RT.Extensions
{
    public static class DICOMRTObjectExtensions
    {
        public static bool IsDRR(this DICOMObject dcm)
        {
            return (dcm.SOPClass == EvilDICOM.Core.Enums.SOPClass.RTImageStorage)
               && dcm.GetSelector().ImageType!=null 
               && dcm.GetSelector().ImageType.Data_.Contains("DRR");
        }
    }
}
