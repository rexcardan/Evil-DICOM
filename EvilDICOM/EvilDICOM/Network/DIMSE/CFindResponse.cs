using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.DIMSE.IOD;
using System;
using System.Collections.Generic;
using C = EvilDICOM.Network.Enums.CommandField;

namespace EvilDICOM.Network.DIMSE
{
    public class CFindResponse : AbstractDIMSEResponse
    {
        public CFindResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort) C.C_FIND_RQ;
        }

        public T GetIOD<T>() where T:AbstractDIMSEIOD
        {
            if (this.Data != null)
            {
                if (typeof(T) == typeof(CFindImageIOD))
                {
                    return new CFindImageIOD(new DICOMObject(this.Data.Elements)) as T;
                }
                if (typeof(T) == typeof(CFindStudyIOD))
                {
                    return new CFindStudyIOD(new DICOMObject(this.Data.Elements)) as T;
                }
                if (typeof(T) == typeof(CFindSeriesIOD))
                {
                    return new CFindSeriesIOD(new DICOMObject(this.Data.Elements)) as T;
                }
                if (typeof(T) == typeof(CFindPlanIOD))
                {
                    return new CFindPlanIOD(new DICOMObject(this.Data.Elements)) as T;
                }
            }
            return null;
        }
    }
}