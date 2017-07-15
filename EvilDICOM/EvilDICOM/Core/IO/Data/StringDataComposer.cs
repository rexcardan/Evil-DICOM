#region

using System.Globalization;
using System.Linq;
using EvilDICOM.Core.Element;
using DateTime = System.DateTime;

#endregion

namespace EvilDICOM.Core.IO.Data
{
    public class StringDataComposer
    {
        public static string ComposeAgeString(Age data)
        {
            if (data != null)
            {
                var ageString = string.Format(CultureInfo.InvariantCulture, "{0:00#}", data.Number);
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
                var date = (DateTime) data;
                return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        public static string ComposeDateTime(DateTime? data)
        {
            if (data != null)
            {
                var date = (DateTime) data;
                return date.ToString("yyyyMMddHHmmss.ffffff", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        public static string ComposeDecimalString(double[] data)
        {
            if (data != null)
                return string.Join("\\", data.Select(d => d.ToString(CultureInfo.InvariantCulture)).ToArray());
            return string.Empty;
        }

        public static string ComposeIntegerString(int[] data)
        {
            if (data != null)
                return string.Join("\\", data.Select(d => d.ToString(CultureInfo.InvariantCulture)).ToArray());
            return string.Empty;
        }

        public static string ComposeTime(DateTime? data)
        {
            if (data != null)
            {
                var date = (DateTime) data;
                return date.ToString("HHmmss.ffffff", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }
    }
}