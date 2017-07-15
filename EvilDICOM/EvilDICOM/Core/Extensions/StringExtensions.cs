using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Extensions
{
    /// <summary>
    ///     Adds useful methods to the string object
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Changes a camel case string to uppercase with underscore between the words
        /// </summary>
        /// <param name="camelCase">the camel case string</param>
        /// <returns>the uppercase with understore separator string</returns>
        public static string ToUpperUnderscore(this string camelCase)
        {
            var build = new StringBuilder();
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