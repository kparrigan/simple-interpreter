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
        /// Checks a char to see if it's set to null.
        /// </summary>
        /// <param name="c">Char to check.</param>
        /// <returns>bool indicating if null or not.</returns>
        public static bool IsNull(this char c)
        {
            return c.Equals('\0');
        }
    }    
}
