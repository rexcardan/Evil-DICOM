using EvilDICOM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Helpers
{
    public class SOPClassHelper
    {
        public static Dictionary<string, SOPClass> dict;

        public static Enums.SOPClass FromUID(string sopClassUid)
        {
            if (dict == null) { Initialize(); }

            if (dict.ContainsKey(sopClassUid))
            {
                return dict[sopClassUid];
            }
            else
            {
                return SOPClass.Unknown;
            }
        }

        private static void Initialize()
        {          
            dict = new Dictionary<string, SOPClass>();
            var keys = new List<KeyValuePair<string, SOPClass>>(){
            new KeyValuePair<string,SOPClass>(AbstractSyntax.CR_IMAGE_STORAGE, SOPClass.CR),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.CT_IMAGE_STORAGE, SOPClass.CT),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.RT_DOSE_STORAGE, SOPClass.RT_Dose),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.RT_IMAGE_STORAGE, SOPClass.RT_Image),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.RT_PLAN_STORAGE, SOPClass.RT_Plan),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.RT_STRUCTURE_STORAGE, SOPClass.RT_Structure),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.VERIFICATION, SOPClass.Verification),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.MR_IMAGE_STORAGE, SOPClass.MR),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.NMR_IMAGE_STORAGE, SOPClass.NMR),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.SPATIAL_IMAGE_STORAGE, SOPClass.RT_Reg),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.PATIENT_FIND, SOPClass.FIND),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.PATIENT_GET, SOPClass.GET),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.PATIENT_MOVE, SOPClass.MOVE),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.PET_IMAGE_STORAGE, SOPClass.PET),
            new KeyValuePair<string,SOPClass>(AbstractSyntax.RT_ION_PLAN_STORAGE, SOPClass.Ion_Plan),     
            new KeyValuePair<string,SOPClass>(AbstractSyntax.ULTRASOUND_IMAGE_STORAGE, SOPClass.Ultrasound_Image),     
            };


            foreach (var key in keys)
            {
                dict.Add(key.Key, key.Value);
            }
        }
    }
}
