using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Anonymization.Anonymizers
{
    /// <summary>
    /// Removes all names from the DICOM File. If using PatientIdAnonymizer, call this first so new id is not removed
    /// </summary>
    public class NameAnonymizer : IAnonymizer
    {
        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Anonymizing names...", 0);
            foreach (var name in d.FindAll(VR.PersonName))
            {
                name.DData = "Anonymized";
            }
        }
    }
}
