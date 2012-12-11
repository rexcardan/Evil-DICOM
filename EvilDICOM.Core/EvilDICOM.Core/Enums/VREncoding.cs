using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    /// <summary>
    /// An enum that contains the different types of VR encoding in DICOM
    /// </summary>
    public enum VREncoding
    {
        Implicit,
        ExplicitShort,
        ExplicitLong
    }
}
