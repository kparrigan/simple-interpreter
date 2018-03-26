using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Exceptions
{
    public sealed class VariableNameException : ApplicationException
    {
        public VariableNameException(string variableName)
            : base($"{variableName} does not exist in the global scope.")
        {
        }
    }
}
