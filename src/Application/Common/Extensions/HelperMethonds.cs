using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Common.Extensions
{
    public static class HelperMethonds
    {
        /// <summary>
        /// Converts a string of dash-separated, or underscore-separated words to a PascalCase string.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The resulting PascalCase string.</returns>
        public static string ToPascalCase(this string s)
        {
            string[] words = s.Split(new char[2] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder(words.Sum(x => x.Length));

            foreach (string word in words)
            {
                sb.Append(word[0].ToString().ToUpper() + word.Substring(1));
            }

            return sb.ToString();
        }
    }
}
