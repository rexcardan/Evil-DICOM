using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Anonymization.Helpers
{

    /// <summary>
    /// Class DateHelper.
    /// </summary>
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
            return YoungerThan89(ageTest);
        }

        /// <summary>
        /// Checks to see if the patient age is less than 89 years old
        /// </summary>
        /// <param name="d">DICOM object</param>
        /// <returns>boolean indication test</returns>
        public static bool YoungerThan89(DICOMObject d)
        {
            //Check patient age - if 89 or older set anonymization options to 
            //Not conserve patient age
            var sel = new DICOMSelector(d);
            Date dob = sel.PatientBirthDate;
            if (dob == null || !dob.Data.HasValue) { return false; }

            var latestDate = GetLatestDate(d);
            int age = CalculateAge(dob, latestDate);
            return age <= 89;
        }

        /// <summary>
        /// Calculates the latest (most recent) date in a DICOM file
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static System.DateTime GetLatestDate(DICOMObject d)
        {
            var dates = GetAllDates(d);
            //If there are no actual dates just return today
            if (!dates.Any(da => da.Data.HasValue)) { return System.DateTime.Now; }
            var latest = (System.DateTime)(dates
                .Where(da => da.Data.HasValue)
                .OrderBy(da => da.Data)
                .Last().Data);
            return latest;
        }

        /// <summary>
        /// Calculates the oldest date in a DICOM file
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static System.DateTime GetOldestDate(DICOMObject d)
        {
            var dates = GetAllDates(d);
            //If there are no actual dates just return today
            if (!dates.Any(da => da.Data.HasValue)) { return System.DateTime.Now; }
            var oldest = (System.DateTime)(dates
                .Where(da => da.Data.HasValue)
                .OrderBy(da => da.Data)
                .First().Data);
            return oldest;
        }

        /// <summary>
        /// Gets all dates.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>IEnumerable&lt;AbstractElement&lt;System.Nullable&lt;System.DateTime&gt;&gt;&gt;.</returns>
        public static IEnumerable<AbstractElement<System.DateTime?>> GetAllDates(DICOMObject d)
        {
            var dates = d.FindAll(VR.Date).Concat(d.FindAll(VR.DateTime)).Select(da => da as AbstractElement<System.DateTime?>);
            return dates;
        }

        /// <summary>
        /// Calculates the patient age based on todays date
        /// </summary>
        /// <param name="dob">the IDICOM element containing the DateTime of patient birth</param>
        /// <returns>the patient's age in years</returns>
        private static int CalculateAge(Date dob, System.DateTime latestDate)
        {
            if (dob != null && dob.Data != null)
            {
                System.DateTime dt = (System.DateTime)dob.Data;
                return latestDate.Year - dt.Year;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// Gets the base date.
        /// </summary>
        /// <value>The base date.</value>
        public static System.DateTime BaseDate
        {
            get
            {
                return new System.DateTime(1901, 1, 1);
            }
        }


        /// <summary>
        /// Gets the random date.
        /// </summary>
        /// <value>The random date.</value>
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

        /// <summary>
        /// Adds the time difference between the original date and reference date to the base date 01/01/1901. 
        /// This allows all dates to maintain a relative relationship to each other, but birthdate is obfuscated
        /// </summary>
        /// <param name="original"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public static System.DateTime DateRelativeBaseDate(System.DateTime? original, System.DateTime? reference)
        {
            if (original != null && reference != null)
            {
                System.DateTime date = (System.DateTime)original;
                System.DateTime refDate = (System.DateTime)reference;
                TimeSpan deltaDate = date.Subtract(refDate);
                System.DateTime newDate = BaseDate.Add(deltaDate);
                return newDate;
            }
            else { return BaseDate; }
        }
    }
}
