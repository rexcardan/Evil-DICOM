#region

using System.Linq;
using EvilDICOM.Anonymization.Helpers;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;

#endregion

namespace EvilDICOM.Anonymization.Anonymizers
{
    public class DateAnonymizer : IAnonymizer
    {
        private readonly DateSettings dateSettings;

        public DateAnonymizer(DateSettings dateSettings)
        {
            this.dateSettings = dateSettings;
        }

        public void Anonymize(DICOMObject d)
        {
            EvilLogger.Instance.Log("Anonymizing dates...");

            if (dateSettings == DateSettings.KEEP_ALL_DATES)
            {
            }
            else
            {
                if (dateSettings == DateSettings.PRESERVE_AGE)
                    PreserveAndAnonymize(d);
                else if (dateSettings == DateSettings.NULL_AGE)
                    NullAndAnonymize(d);
                else if (dateSettings == DateSettings.MAKE_89)
                    Make89AndAnonymize(d);
                else
                    Randomize(d);
            }
        }

        public void PreserveAndAnonymize(DICOMObject d)
        {
            var dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                var oldest = (Date) dates.OrderBy(da => (da as Date).Data).ToList()[0];
                foreach (var el in dates)
                {
                    var da = el as Date;
                    var date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
        }

        public void NullAndAnonymize(DICOMObject d)
        {
            var dob = d.FindFirst(TagHelper.PatientBirthDate) as Date;
            dob.Data = null;

            var dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                var oldest = (Date) dates
                    .Where(da => (da as Date).Data != null)
                    .OrderBy(da => (da as Date).Data)
                    .ToList()[0];
                foreach (var el in dates)
                {
                    var da = el as Date;
                    var date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
        }

        public void Make89AndAnonymize(DICOMObject d)
        {
            var dob = d.FindFirst(TagHelper.PatientBirthDate) as Date;
            var dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                var oldest = (Date) dates
                    .Where(da => (da as Date).Data != null &&
                                 da.Tag.CompleteID != TagHelper.PatientBirthDate.CompleteID)
                    .OrderBy(da => (da as Date).Data)
                    .ToList()[0];
                var oldestDate = (System.DateTime) oldest.Data;
                dob.Data = new System.DateTime(oldestDate.Year - 89, oldestDate.Month, oldestDate.Day);

                oldest = dob;
                foreach (var el in dates)
                {
                    var da = el as Date;
                    var date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
        }

        public void Randomize(DICOMObject d)
        {
            var dates = d.FindAll(VR.Date);

            foreach (var el in dates)
            {
                var da = el as Date;
                da.Data = DateHelper.RandomDate;
            }
        }
    }
}