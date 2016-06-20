using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum SOPClass
    {
        Unknown,
        Verification,
        Storage_Commitment_Push_Model,
        CT,
        CR,
        Ultrasound_Image,
        //RT
        RT_Image,
        RT_Dose,
        RT_Reg,
        RT_Structure,
        RT_Plan,
        RT_Brachy_Record,
        RT_Treatment_Record,
        RT_IonPlan,
        RT_IonBeam_Record,
        MR,
        NMR,
        FIND,
        MOVE,
        GET,
        PET,
        Ion_Plan
    }
}
