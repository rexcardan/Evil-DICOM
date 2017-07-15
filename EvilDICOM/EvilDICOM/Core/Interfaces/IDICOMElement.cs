using System;
using System.Collections;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Interfaces
{
    public interface IDICOMElement
    {
        Tag Tag { get; set; }
        Type DatType { get; }

        /// <summary>
        ///     The dynamic single value data in the element of the first datum in the array (in the case of multiple datum)
        /// </summary>
        object DData { get; set; }

        /// <summary>
        ///     The dynamic data in the element stored in a list of type T
        /// </summary>
        ICollection DData_ { get; set; }
    }
}