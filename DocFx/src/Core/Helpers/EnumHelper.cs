using System;

namespace EvilDICOM.Core.Helpers
{
    public class EnumHelper
    {
        public static T StringToEnum<T>(string name)
        {
            return (T) Enum.Parse(typeof (T), name, false);
        }
    }
}