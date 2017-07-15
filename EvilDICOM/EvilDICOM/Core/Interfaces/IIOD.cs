#region

using System.Collections.Generic;

#endregion

namespace EvilDICOM.Core.Interfaces
{
    public interface IIOD
    {
        List<IDICOMElement> Elements { get; }
    }
}