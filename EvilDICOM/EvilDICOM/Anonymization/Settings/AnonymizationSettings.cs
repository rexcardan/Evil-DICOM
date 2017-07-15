namespace EvilDICOM.Anonymization.Settings
{
    public class AnonymizationSettings
    {
        //OPTIONS
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
        /// Specifies whether or not to replace study ids with generic names
        /// </summary>
        public bool DoAnonymizeStudyIDs { get; set; }

        /// <summary>
        /// Specifies whether or not to replace and remap UIDs
        /// </summary>
        public bool DoAnonymizeUIDs { get; set; }

        /// <summary>
        /// Specifies how dates are to be handled on anonymization
        /// </summary>
        public DateSettings DateSettings { get; set; }

        /// <summary>
        /// Specifies if private tags are to be removed
        /// </summary>
        public bool DoRemovePrivateTags { get; set; }

        /// <summary>
        /// Specifies if all names are to be removed from the file
        /// </summary>
        public bool DoAnonymizeNames { get; set; }

        /// <summary>
        /// Specifies if the standard anonymization profile identifiers are to have data cleaned
        /// </summary>
        public bool DoDICOMProfile { get; set; }

        /// <summary>
        /// A default anonmyization schema
        /// </summary>
        public static AnonymizationSettings Default
        {
            get
            {
                return new AnonymizationSettings
                {
                    FirstName = "Anonymized",
                    LastName = "EvilDICOM",
                    Id = "DA000000001",
                    DoAnonymizeStudyIDs = true,
                    DoAnonymizeUIDs = true,
                    DateSettings = DateSettings.PRESERVE_AGE,
                    DoRemovePrivateTags = true
                };
            }
        }
    }
}