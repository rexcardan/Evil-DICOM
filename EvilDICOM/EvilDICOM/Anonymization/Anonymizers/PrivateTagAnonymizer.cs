#region

using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes private tags from DICOM object which may or may not contain identifiable information
    /// </summary>
    public class PrivateTagAnonymizer : IAnonymizer
    {
        ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<PrivateTagAnonymizer>();
        public void Anonymize(DICOMObject d)
        {
            _logger.LogInformation("Removing private tags...", 0);

            d.RemoveRange(d.AllElements.Where(e => e.Tag.IsPrivate()).Select(x=>x.Tag));

            //foreach (var priv in d.AllElements.Where(e => e.Tag.IsPrivate()))
            //    d.Remove(priv.Tag);
        }
    }
}