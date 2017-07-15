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
                else if (dateSettings == DateSettings.NULL_AGE)
                {
                    NullAndAnonymize(d);
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

        public void PreserveAndAnonymize(DICOMObject d)
        {
            List<IDICOMElement> dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                Date oldest = (Date)dates.OrderBy(da => (da as Date).Data).ToList()[0];
                foreach (IDICOMElement el in dates)
                {
                    Date da = el as Date;
                    System.DateTime? date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
        }

        public void NullAndAnonymize(DICOMObject d)
        {
            Date dob = d.FindFirst(TagHelper.Patient​Birth​Date) as Date;
            dob.Data = null;

            List<IDICOMElement> dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                Date oldest = (Date)dates
                    .Where(da => (da as Date).Data != null)
                    .OrderBy(da => (da as Date).Data)
                    .ToList()[0];
                foreach (IDICOMElement el in dates)
                {
                    Date da = el as Date;
                    System.DateTime? date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
        }

        public void Make89AndAnonymize(DICOMObject d)
        {
            Date dob = d.FindFirst(TagHelper.Patient​Birth​Date) as Date;
            List<IDICOMElement> dates = d.FindAll(VR.Date);
            if (dates.Count > 0)
            {
                Date oldest = (Date)dates
                    .Where(da => (da as Date).Data != null && da.Tag.CompleteID != TagHelper.Patient​Birth​Date.CompleteID)
                    .OrderBy(da => (da as Date).Data)
                    .ToList()[0];
                System.DateTime oldestDate = (System.DateTime)oldest.Data;
                dob.Data = new System.DateTime(oldestDate.Year - 89, oldestDate.Month, oldestDate.Day);

                oldest = dob;
                foreach (IDICOMElement el in dates)
                {
                    Date da = el as Date;
                    System.DateTime? date = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                    da.Data = DateHelper.DateRelativeBaseDate(da.Data, oldest.Data);
                }
            }
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
