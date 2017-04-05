using System.Collections.Generic;

namespace EvilDICOM.Core.Interfaces
{
    public interface IIOD
    {
        List<IDICOMElement> Elements { get; }
    }
}