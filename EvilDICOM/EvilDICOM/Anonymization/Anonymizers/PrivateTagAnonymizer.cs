using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core;
using EvilDICOM.Core.Logging;

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes private tags from DICOM object which may or may not contain identifiable information
    /// </summary>
    public class PrivateTagAnonymizer :IAnonymizer
    {

        /// <summary>
        /// Anonymizes the specified dcom object.
        /// </summary>
        /// <param name="d">The dicom object.</param>
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Removing private tags...", 0);
            foreach (var priv in d.AllElements.Where(e => e.Tag.IsPrivate()))
            {
                d.Remove(priv.Tag);
            }
        }
    }
}
