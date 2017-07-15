#region

using EvilDICOM.Core;
using EvilDICOM.Network.DIMSE.IOD;
using C = EvilDICOM.Network.Enums.CommandField;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public class CFindResponse : AbstractDIMSEResponse
    {
        public CFindResponse(DICOMObject d)
            : base(d)
        {
            CommandField = (ushort) C.C_FIND_RQ;
        }

        public T GetIOD<T>() where T : AbstractDIMSEIOD
        {
            if (Data != null)
            {
                if (typeof(T) == typeof(CFindImageIOD))
                    return new CFindImageIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindStudyIOD))
                    return new CFindStudyIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindSeriesIOD))
                    return new CFindSeriesIOD(new DICOMObject(Data.Elements)) as T;
                if (typeof(T) == typeof(CFindPlanIOD))
                    return new CFindPlanIOD(new DICOMObject(Data.Elements)) as T;
            }
            return null;
        }
    }
}