#region

using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes all names from the DICOM File. If using PatientIdAnonymizer, call this first so new id is not removed
    /// </summary>
    public class NameAnonymizer : IAnonymizer
    {
        ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<NameAnonymizer>();
        public void Anonymize(DICOMObject d)
        {
            _logger.LogInformation("Anonymizing names...", 0);
            foreach (var name in d.FindAll(VR.PersonName))
                name.DData = "Anonymized";
        }
    }
}