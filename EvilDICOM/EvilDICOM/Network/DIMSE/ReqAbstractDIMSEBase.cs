using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.DIMSE
{
    public abstract class ReqAbstractDIMSEBase
    {
        protected UniqueIdentifier _requestedSOPClassUID = new UniqueIdentifier
        {
            Tag = TagHelper.RequestedSOPClassUID
        };

        public string RequestedSOPClassUID
        {
            get { return _requestedSOPClassUID.Data; }
            set { _requestedSOPClassUID.Data = value; }
        }
    }
}
