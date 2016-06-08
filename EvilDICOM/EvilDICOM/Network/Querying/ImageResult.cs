using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Querying
{

    /// <summary>
    /// Class ImageResult.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Querying.SeriesResult" />
    public class ImageResult : SeriesResult
    {
        /// <summary>
        /// Gets or sets the sop instance uid.
        /// </summary>
        /// <value>The sop instance uid.</value>
        public string SopInstanceUid { get; set; }
    }
}
