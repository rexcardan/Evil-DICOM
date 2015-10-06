using System;

namespace EvilDICOM.Core.Helpers
{
    public class DoubleHelper
    {
        public static int GetSignificantDigitsPostDecimal(double d)
        {
            double floor = d - Math.Floor(d);
            int numDecimals = floor.ToString("#.#########").Length - 1;
            return numDecimals < 0 ? 0 : numDecimals;
        }

        public static string BuildDICOMStringFormat(double d)
        {
            if (d == 0)
            {
                return "0.0";
            }
            int sigDigit = GetSignificantDigitsPostDecimal(d);
            switch (sigDigit)
            {
                case 0:
                    return "0.#";
                case 1:
                    return "0.0";
                case 2:
                    return "0.00";
                case 3:
                    return "0.000";
                default:
                    return "0.0000e+0";
            }
        }
    }
}