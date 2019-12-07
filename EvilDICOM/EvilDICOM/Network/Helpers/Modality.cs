using System;
using System.Collections.Generic;
using System.Text;

namespace EvilDICOM.Network.Helpers
{
    public class Modality
    {
        public static bool IsImage(string modality)
        {
            return modality == "CT" 
                || modality == "MR" 
                || modality == "PT" 
                || modality == "RTIMAGE"
                || modality == "CR"
                || modality == "NM"
                || modality == "US";
        }
    }
}
