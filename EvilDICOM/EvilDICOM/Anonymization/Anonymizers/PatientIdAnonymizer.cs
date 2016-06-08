using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Replaces patient identifier with specified name and id
    /// </summary>
    public class PatientIdAnonymizer : IAnonymizer
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientIdAnonymizer"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="id">The identifier.</param>
        public PatientIdAnonymizer(string firstName, string lastName, string id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        /// <summary>
        /// Anonymizes the specified dicom object.
        /// </summary>
        /// <param name="d">The dicom object.</param>
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Anonymizing patient identity...", 0);
            //PATIENTS NAME
            var name = DICOMForge.PatientName;
            name.FirstName = FirstName;
            name.LastName = LastName;
            d.ReplaceOrAdd(name);

            //PATIENT ID
            var id = DICOMForge.PatientID;
            id.Data = Id;
            d.ReplaceOrAdd(id);
        }

    }
}
