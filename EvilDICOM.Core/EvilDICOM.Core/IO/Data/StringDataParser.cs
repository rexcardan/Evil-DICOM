﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using EvilDICOM.Core.Element;
using DateTime = System.DateTime;

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
                    dateTime = DateTime.ParseExact(data, "yyyyMMddHHmmss.ffffff", null);
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
                    double.TryParse(sNumbers[i], out numbers[i]);
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
            if (data.Length == 13)
            {
                return System.DateTime.ParseExact(data, "HHmmss.ffffff", null);
            }
            else if (data.Length == 10)
            {
                return System.DateTime.ParseExact(data, "HHmmss.fff", null);
            }
            else if (data.Length == 6)
            {
                return System.DateTime.ParseExact(data, "HHmmss", null);
            }
            else
            {
                //throw new Exception("Time format is not DICOM 3.0 Compliant!");
                return null;
            }
        }
    }
}
