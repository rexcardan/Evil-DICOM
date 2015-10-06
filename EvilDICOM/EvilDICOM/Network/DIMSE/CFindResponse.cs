using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.DIMSE.IOD;
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

        public CFindIOD GetIOD()
        {
            if (this.Data != null)
            {
                return new CFindIOD(new DICOMObject(this.Data.Elements));
            }
            return null;
        }
    }
}