using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Querying
{

    /// <summary>
    /// Class SeriesResult.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Querying.StudyResult" />
    public class SeriesResult : StudyResult
    {
        /// <summary>
        /// Gets or sets the series uid.
        /// </summary>
        /// <value>The series uid.</value>
        public string SeriesUid { get; set; }

        /// <summary>
        /// Gets or sets the modality.
        /// </summary>
        /// <value>The modality.</value>
        public string Modality { get; set; }
    }
}
