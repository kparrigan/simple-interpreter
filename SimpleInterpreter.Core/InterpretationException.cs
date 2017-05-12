﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class InterpretationException : Exception
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="InterpretationException"/> class.
        /// </summary>
        /// <param name="message">Message of exception.</param>
        public InterpretationException(string message)
           : base (message) 
        {
        }
        #endregion
    }
}
