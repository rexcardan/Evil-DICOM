using System;
using System.Linq;
using EvilDICOM.Core.Element;
using DateTime = System.DateTime;

namespace EvilDICOM.Core.IO.Data
{
    public class StringDataComposer
    {
        public static string ComposeAgeString(Age data)
        {
            if (data != null)
            {
                string ageString = String.Format("{0:00#}", data.Number);
                switch (data.Units)
                {
                    case Age.Unit.DAYS:
                        ageString += "D";
                        break;
                    case Age.Unit.WEEKS:
                        ageString += "W";
                        break;
                    case Age.Unit.MONTHS:
                        ageString += "M";
                        break;
                    case Age.Unit.YEARS:
                        ageString += "Y";
                        break;
                }
                return ageString;
            }
            return string.Empty;
        }

        public static string ComposeDate(DateTime? data)
        {
            if (data != null)
            {
                var date = (DateTime)data;
                return date.ToString("yyyyMMdd");
            }
            return string.Empty;
        }

        public static string ComposeDateTime(DateTime? data)
        {
            if (data != null)
            {
                var date = (DateTime)data;
                return date.ToString("yyyyMMddHHmmss.ffffff");
            }
            return string.Empty;
        }

        public static string ComposeDecimalString(double[] data)
        {
            if (data != null)
            {
                return String.Join("\\", data.Select(d => d.ToString()).ToArray());
            }
            return string.Empty;
        }

        public static string ComposeIntegerString(int[] data)
        {
            if (data != null)
            {
                return String.Join("\\", data.Select(d => d.ToString()).ToArray());
            }
            return string.Empty;
        }

        public static string ComposeTime(DateTime? data)
        {
            if (data != null)
            {
                var date = (DateTime)data;
                return date.ToString("HHmmss.ffffff");
            }
            return string.Empty;
        }
    }
}