using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization.Helpers
{
    public class DateHelper
    {
        /// <summary>
        /// Checks to see if the patient age is less than 89 years old
        /// </summary>
        /// <param name="file">Path to DICOM file containing patient information</param>
        /// <returns>boolean indication test</returns>
        public static bool YoungerThan89(string file)
        {
            //Check patient age - if 89 or older set anonymization options to 
            //Not conserve patient age
            DICOMObject ageTest = DICOMFileReader.Read(file);
            Date dob = ageTest.FindFirst(TagHelper.Patient​Birth​Date.CompleteID) as Date;
            if (dob != null)
            {
                int age = CalculateAge(dob);
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
            System.DateTime now = System.DateTime.Today;

            if (dob != null && dob.Data != null)
            {
                System.DateTime dt = (System.DateTime)dob.Data;
                return now.Year - dt.Year;
            }
            else
            {
                return 0;
            }
        }


        public static System.DateTime BaseDate
        {
            get
            {
                return new System.DateTime(1901, 1, 1);
            }
        }


        public static System.DateTime RandomDate
        {
            get
            {
                Random r = new Random();
                int year = r.Next(1920, System.DateTime.Now.Year);
                int month = r.Next(1, 12);
                int day = r.Next(1, 28);
                return new System.DateTime(year, month, day);
            }
        }


        public static System.DateTime? DateRelativeBaseDate(System.DateTime? original, System.DateTime? reference)
        {
            if (original != null && reference != null)
            {
                System.DateTime date = (System.DateTime)original;
                System.DateTime refDate = (System.DateTime)reference;
                TimeSpan deltaDate = date.Subtract(refDate);
                System.DateTime newDate = BaseDate.Add(deltaDate);
                return newDate;
            }
            else { return null; }
        }
    }
}
