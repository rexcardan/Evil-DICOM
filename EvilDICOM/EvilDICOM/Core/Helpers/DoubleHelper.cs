#region

using System;

#endregion

namespace EvilDICOM.Core.Helpers
{
    public class DoubleHelper
    {
        public static int GetSignificantDigitsPostDecimal(double d)
        {
            var floor = d - Math.Floor(d);
            var numDecimals = floor.ToString("#.#########").Length - 1;
            return numDecimals < 0 ? 0 : numDecimals;
        }

        public static string BuildDICOMStringFormat(double d)
        {
            if (d == 0)
                return "0.0";
            var sigDigit = GetSignificantDigitsPostDecimal(d);
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