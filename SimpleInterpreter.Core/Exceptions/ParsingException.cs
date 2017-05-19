using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Exceptions
{
    public sealed class ParsingException : ApplicationException
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsingException"/> class.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public ParsingException(string message)
            : base (message) 
        {
        }
        #endregion
    }
}
