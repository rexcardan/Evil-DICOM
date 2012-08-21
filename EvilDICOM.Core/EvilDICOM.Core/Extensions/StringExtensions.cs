using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperUnderscore(this string camelCase){
            StringBuilder build = new StringBuilder();
            for (int i = 0; i < camelCase.Length; i++)
            {
                char current = camelCase.ElementAt(i);
                build.Append(char.ToUpper(current));
                char next = i + 1 < camelCase.Length ? camelCase.ElementAt(i + 1) : camelCase.ElementAt(i);
                if (char.IsLower(current) && char.IsUpper(next))
                {
                    build.Append("_");
                }
            }
            return build.ToString();
        }
    }
}
