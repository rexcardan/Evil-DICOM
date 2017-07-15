#region

using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes private tags from DICOM object which may or may not contain identifiable information
    /// </summary>
    public class PrivateTagAnonymizer : IAnonymizer
    {
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Removing private tags...", 0);
            foreach (var priv in d.AllElements.Where(e => e.Tag.IsPrivate()))
                d.Remove(priv.Tag);
        }
    }
}