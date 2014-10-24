using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Data
{
    public class StringDataParser
    {
        public static Age ParseAgeString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                Age a = new Age();
                a.Number = int.Parse(data.Substring(1, 3));
                switch (data.Substring(4, 1))
                {
                    case "D": a.Units = Age.Unit.DAYS; break;
                    case "W": a.Units = Age.Unit.WEEKS; break;
                    case "M": a.Units = Age.Unit.MONTHS; break;
                    case "Y": a.Units = Age.Unit.YEARS; break;
                }
                return a;
            }
        }

        public static System.DateTime? ParseDate(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                try
                {
                    return System.DateTime.ParseExact(data, "yyyyMMdd", null);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static System.DateTime? ParseDateTime(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            else
            {
                System.DateTime? dateTime = null;
                try
                {
                    dateTime = System.DateTime.ParseExact(data, "yyyyMMddHHmmss.ffffff", null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return dateTime;
            }
        }


        public static double[] ParseDecimalString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new double[0];
            }
            else
            {
                string[] sNumbers = data.Replace(" ", "") //Remove padding
                    .Split(new char[] { '\\' });
                double[] numbers = new double[sNumbers.Length];
                for (int i = 0; i < sNumbers.Length; i++)
                {
                    double.TryParse(sNumbers[i], NumberStyles.Any, CultureInfo.InvariantCulture, out numbers[i]);
                }
                return numbers;
            }
        }


        public static int[] ParseIntegerString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new int[0];
            }
            else
            {
                string[] sNumbers = data.Replace(" ", "").Split(new char[] { '\\' });
                int[] numbers = new int[sNumbers.Length];
                for (int i = 0; i < sNumbers.Length; i++)
                {
                    int.TryParse(sNumbers[i], out numbers[i]);
                }
                return numbers;
            }
        }

        public static System.DateTime? ParseTime(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            if (data.Length >= 8)
            {
                //At least one decimal
                //Pad with zeros to achieve HHmmss.ffffff
                var zeroPadLength = 13 - data.Length;
                if (zeroPadLength > 1)
                {
                    Enumerable.Range(1, zeroPadLength).Select(r => "0").ToList().ForEach(z =>
                    {
                        data += z;
                    });
                }
            }
            if (data.Length == 13)
            {
                return System.DateTime.ParseExact(data, "HHmmss.ffffff", null);
            }
            else if (data.Length == 6)
            {
                return System.DateTime.ParseExact(data, "HHmmss", null);
            }
            else
            {
                EventLogger.Instance.RaiseToLogEvent("Time parse error. Format expected 'HHmmss.ffffff', actual is {0}", data);
                return null;
            }
        }
    }
}
