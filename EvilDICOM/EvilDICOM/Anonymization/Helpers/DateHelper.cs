#region

using System;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;

#endregion

namespace EvilDICOM.Anonymization.Helpers
{
    public class DateHelper
    {
        public static System.DateTime BaseDate
        {
            get { return new System.DateTime(1901, 1, 1); }
        }


        public static System.DateTime RandomDate
        {
            get
            {
                var r = new Random();
                var year = r.Next(1920, System.DateTime.Now.Year);
                var month = r.Next(1, 12);
                var day = r.Next(1, 28);
                return new System.DateTime(year, month, day);
            }
        }

        /// <summary>
        /// Checks to see if the patient age is less than 89 years old
        /// </summary>
        /// <param name="file">Path to DICOM file containing patient information</param>
        /// <returns>boolean indication test</returns>
        public static bool YoungerThan89(string file)
        {
            //Check patient age - if 89 or older set anonymization options to 
            //Not conserve patient age
            var ageTest = DICOMFileReader.Read(file);
            var dob = ageTest.FindFirst(TagHelper.PatientBirthDate.CompleteID) as Date;
            if (dob != null)
            {
                var age = CalculateAge(dob);
                return age < 89;
            }
            return true;
        }

        /// <summary>
        /// Calculates the patient age based on todays date
        /// </summary>
        /// <param name="dob">the IDICOM element containing the DateTime of patient birth</param>
        /// <returns>the patient's age in years</returns>
        private static int CalculateAge(Date dob)
        {
            var now = System.DateTime.Today;

            if (dob != null && dob.Data != null)
            {
                var dt = (System.DateTime) dob.Data;
                return now.Year - dt.Year;
            }
            return 0;
        }


        public static System.DateTime? DateRelativeBaseDate(System.DateTime? original, System.DateTime? reference)
        {
            if (original != null && reference != null)
            {
                var date = (System.DateTime) original;
                var refDate = (System.DateTime) reference;
                var deltaDate = date.Subtract(refDate);
                var newDate = BaseDate.Add(deltaDate);
                return newDate;
            }
            return null;
        }
    }
}