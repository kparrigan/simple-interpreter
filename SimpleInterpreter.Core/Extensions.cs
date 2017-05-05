using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Strips all whitespace from a string.
        /// </summary>
        /// <param name="s">String to strip whitespace from.</param>
        /// <returns>Whitespaceless string.</returns>
        /// <remarks>Returns original string if null or empty.</remarks>
        public static string StripWhiteSpace(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return s.Replace(" ", string.Empty);
        }
    }
}
