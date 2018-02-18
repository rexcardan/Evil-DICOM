using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.DIMSE.IOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Services.Interfaces
{
    public interface IFindService<T> where T: AbstractDIMSEIOD
    {
        IEnumerable<T> RetrieveMatches(List<IDICOMElement> keys);
    }
}
