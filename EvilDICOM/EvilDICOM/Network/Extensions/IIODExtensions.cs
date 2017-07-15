#region

using System;
using System.Reflection;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.Extensions
{
    public static class IIODExtensions
    {
        public static string GetLogString(this IIOD iod)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Empty);
            sb.AppendLine("-----------------------------------");
            var t = iod.GetType();
            sb.AppendLine(t.Name);
            foreach (var pi in t.GetTypeInfo().GetProperties())
            {
                var name = pi.Name;
                if (pi.Name != "Elements" && pi.Name != "LogString")
                {
                    var value = pi.GetValue(iod, null);
                    var valueString = string.Empty;
                    if (pi.Name == "Status")
                        valueString = Enum.GetName(typeof(Status), value);
                    else
                        valueString = value != null ? value.ToString() : "null";
                    sb.AppendLine(string.Format("{0} : {1}", pi.Name, valueString));
                }
            }
            sb.AppendLine("-----------------------------------");
            return sb.ToString();
        }
    }
}