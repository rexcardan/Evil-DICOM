using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.Extensions
{
    public static class IIODExtensions
    {
        public static string GetLogString(this IIOD iod)
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Empty);
            sb.AppendLine("-----------------------------------");
            Type t = iod.GetType();
            sb.AppendLine(t.Name);
            foreach (PropertyInfo pi in t.GetTypeInfo().GetProperties())
            {
                string name = pi.Name;
                if (pi.Name != "Elements" && pi.Name != "LogString")
                {
                    object value = pi.GetValue(iod, null);
                    string valueString = string.Empty;
                    if (pi.Name == "Status")
                    {
                        valueString = Enum.GetName(typeof(Status), value);
                    }
                    else
                    {
                        valueString = value != null ? value.ToString() : "null";
                    }
                    sb.AppendLine(string.Format("{0} : {1}", pi.Name, valueString));
                }
            }
            sb.AppendLine("-----------------------------------");
            return sb.ToString();
        }
    }
}
