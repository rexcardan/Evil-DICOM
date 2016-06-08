using System;
using System.Collections;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Interfaces
{
    /// <summary>
    /// Interface IDICOMElement
    /// </summary>
    public interface IDICOMElement
    {
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        Tag Tag { get; set; }
        /// <summary>
        /// Gets the type of the dat.
        /// </summary>
        /// <value>The type of the dat.</value>
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