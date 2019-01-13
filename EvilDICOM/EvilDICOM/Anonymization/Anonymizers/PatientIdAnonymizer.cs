#region

using EvilDICOM.Core;
using EvilDICOM.Core.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Replaces patient identifier with specified name and id
    /// </summary>
    public class PatientIdAnonymizer : IAnonymizer
    {
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
            EvilLogger.Instance.Log("Anonymizing patient identity...", 0);
            //PATIENTS NAME
            var name = DICOMForge.PatientName();
            name.FirstName = FirstName;
            name.LastName = LastName;
            d.ReplaceOrAdd(name);

            //PATIENT ID
            var id = DICOMForge.PatientID();
            id.Data = Id;
            d.ReplaceOrAdd(id);
        }
    }
}