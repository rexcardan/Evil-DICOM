using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

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
                    case Age.Unit.DAYS: ageString += "D"; break;
                    case Age.Unit.WEEKS: ageString += "W"; break;
                    case Age.Unit.MONTHS: ageString += "M"; break;
                    case Age.Unit.YEARS: ageString += "Y"; break;
                }
                return ageString;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ComposeDate(System.DateTime? data)
        {
            if (data != null)
            {
                System.DateTime date = (System.DateTime)data;
                return date.ToString("yyyyMMdd");
            }
            else { return string.Empty; }
        }

        public static string ComposeDateTime(System.DateTime? data)
        {
            if (data != null)
            {
                System.DateTime date = (System.DateTime)data;
                return date.ToString("yyyyMMddHHmmss.ffffff");
            }
            else { return string.Empty; }

        }

        public static string ComposeDecimalString(double[] data)
        {
            if (data != null)
            {
                return String.Join("\\", data.Select(d => d.ToString()).ToArray());
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ComposeIntegerString(int[] data)
        {
            if (data != null)
            {
                return String.Join("\\", data.Select(d => d.ToString()).ToArray());
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ComposeTime(System.DateTime? data)
        {
            if (data != null)
            {
                System.DateTime date = (System.DateTime)data;
                if (data.Value.Millisecond > 0)
                {
                    return date.ToString("HHmmss.ffffff");
                }
                else
                {
                    return date.ToString("HHmmss");
                }
            }
            else { return string.Empty; }

        }
    }
}
