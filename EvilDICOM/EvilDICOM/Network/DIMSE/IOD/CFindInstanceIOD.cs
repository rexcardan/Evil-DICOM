using EvilDICOM.Network.DIMSE.IOD;
using System;
using System.Collections.Generic;
using System.Text;
using S = System;
using DF = EvilDICOM.Core.DICOMForge;
using EvilDICOM.Network.Enums;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Network.DIMSE.IOD
{
    public class CFindInstanceIOD : CFindRequestIOD
    {
        public CFindInstanceIOD()
        {
            QueryLevel = QueryLevel.IMAGE;
            SOPInstanceUID = string.Empty;
            SOPClassUID = string.Empty;
        }

        public CFindInstanceIOD(DICOMObject dcm) : base(dcm)
        {
        }
     

        public string SOPClassUID
        {
            get { return _sel.SOPClassUID != null ? _sel.SOPClassUID.Data : null; }
            set { _sel.Forge(DF.SOPClassUID(value)); }
        }

        public string SOPInstanceUID
        {
            get { return _sel.SOP​Instance​UID != null ? _sel.SOP​Instance​UID.Data : null; }
            set { _sel.Forge(DF.SOP​Instance​UID(value)); }
        }
    }
}
