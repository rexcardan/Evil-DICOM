using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Interfaces
{
    public interface IDICOMElement
    {
        Tag Tag { get; set; }
        Type DataType { get; }
        object UntypedData { get; set; }
    }
}
