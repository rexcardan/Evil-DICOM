using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Network.Querying
{
    /// <summary>
    /// Class StudyResult.
    /// </summary>
    public class StudyResult
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        public string PatientId { get; set; }
        /// <summary>
        /// Gets or sets the study uid.
        /// </summary>
        /// <value>The study uid.</value>
        public string StudyUid { get; set; }
    }
}
