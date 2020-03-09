#region

using EvilDICOM.Core;
using EvilDICOM.Core.Logging;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Replaces patient identifier with specified name and id
    /// </summary>
    public class PatientIdAnonymizer : IAnonymizer
    {
        ILogger _logger = EvilLogger.LoggerFactory.CreateLogger<PatientIdAnonymizer>();
        public PatientIdAnonymizer(string firstName, string lastName, string id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        /// <summary>
        /// The first name to make the new patient Id
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name to make the new patient Id
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The new id for the anonymized file
        /// </summary>
        public string Id { get; set; }

        public void Anonymize(DICOMObject d)
        {
            _logger.LogInformation("Anonymizing patient identity...", 0);
            //PATIENTS NAME
            var name = DICOMForge.Patient​Name();
            name.FirstName = FirstName;
            name.LastName = LastName;
            d.ReplaceOrAdd(name);

            //PATIENT ID
            var id = DICOMForge.Patient​ID();
            id.Data = Id;
            d.ReplaceOrAdd(id);
        }
    }
}