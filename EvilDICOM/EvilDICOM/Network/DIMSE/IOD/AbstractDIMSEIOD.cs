#region

using System.Collections.Generic;
using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Selection;

#endregion

namespace EvilDICOM.Network.DIMSE.IOD
{
    public abstract class AbstractDIMSEIOD : IIOD
    {
        protected DICOMSelector _sel;

        public AbstractDIMSEIOD()
        {
            _sel = new DICOMSelector(new DICOMObject());
        }

        public AbstractDIMSEIOD(DICOMObject dcm)
        {
            _sel = new DICOMSelector(dcm);
        }

        public List<IDICOMElement> Elements
        {
            get { return _sel.ToDICOMObject().Elements; }
        }
    }
}