using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Anonymization.Helpers;
using EvilDICOM.Core.Selection;

namespace EvilDICOM.Anonymization.Anonymizers
{
    public class DateAnonymizer : IAnonymizer
    {
        private DateSettings dateSettings;

        public DateAnonymizer(DateSettings dateSettings)
        {
            this.dateSettings = dateSettings;
        }

        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Anonymizing dates...");

            if (dateSettings == DateSettings.KEEP_ALL_DATES)
            {
                return;
            }
            else
            {
                if (dateSettings == DateSettings.PRESERVE_AGE)
                {
                    PreserveAndAnonymize(d);
                }
                else if (dateSettings == DateSettings.NULL_AGE_ANON)
                {
                    NullAgeAndAnonymize(d);
                }
                else if (dateSettings == DateSettings.NULL_AGE_PRESERVE)
                {
                    NullAgeAndPreserve(d);
                }
                else if (dateSettings == DateSettings.MAKE_89)
                {
                    Make89AndAnonymize(d);
                }
                else
                {
                    Randomize(d);
                }
            }
        }

        /// <summary>
        /// Preserves the oldest date (most likely patient DOB) and anonymizes all other dates relative to that
        /// </summary>
        /// <param name="d"></param>
        public void PreserveAndAnonymize(DICOMObject d)
        {
            List<IDICOMElement> dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                var oldest = DateHelper.GetOldestDate(d);
                //Calculate patient age
                if (DateHelper.YoungerThan89(d))
                {
                    foreach (IDICOMElement el in dates)
                    {
                        Date da = el as Date;
                        da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest);
                    }
                }
                else
                {
                    Make89AndAnonymize(d);
                }
            }
        }

        //Null patient age, and anonymize all other dates relative to 01/01/1901
        public void NullAgeAndAnonymize(DICOMObject d)
        {
            Date dob = d.FindFirst(TagHelper.PATIENT_BIRTH_DATE) as Date;
            System.DateTime reference = dob.Data.HasValue ? (System.DateTime)dob.Data.Value : DateHelper.RandomDate;

            //Null patient age
            if (dob != null) { dob.Data = null; }

            List<IDICOMElement> dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                foreach (IDICOMElement el in dates)
                {
                    Date da = el as Date;
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, reference);
                }
            }
        }

        /// <summary>
        /// Null the patient age, but preserve all other dates
        /// </summary>
        /// <param name="d"></param>
        public void NullAgeAndPreserve(DICOMObject d)
        {
            Date dob = d.FindFirst(TagHelper.PATIENT_BIRTH_DATE) as Date;
            dob.Data = null;
        }

        /// <summary>
        /// This method is designed to make patients older than age 89 look 88 in a DICOM file to remain HIPAA compliant
        /// </summary>
        /// <param name="d"></param>
        public void Make89AndAnonymize(DICOMObject d)
        {
            Date dob = d.FindFirst(TagHelper.PATIENT_BIRTH_DATE) as Date;
            var latest = DateHelper.GetLatestDate(d);
            var patient88DOB = latest.AddYears(-88);
            var sel = d.GetSelector();
            sel.PatientBirthDate = DICOMForge.PatientBirthDate;
            sel.PatientBirthDate.Data = patient88DOB;
            PreserveAndAnonymize(d);
        }


        public void Randomize(DICOMObject d)
        {
            List<IDICOMElement> dates = d.FindAll(VR.Date);

            foreach (IDICOMElement el in dates)
            {
                Date da = el as Date;
                da.Data = DateHelper.RandomDate;
            }
        }
    }
}
