using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Interfaces
{
    public interface IIOD
    {
        List<IDICOMElement> Elements
        {
            get;
        }
    }
}
