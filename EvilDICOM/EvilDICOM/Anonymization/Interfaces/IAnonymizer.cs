using EvilDICOM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization
{
    /// <summary>
    /// Interface IAnonymizer
    /// </summary>
    public interface IAnonymizer
    {
        /// <summary>
        /// Anonymizes the specified d.
        /// </summary>
        /// <param name="d">The d.</param>
        void Anonymize(DICOMObject d);
    }
}
