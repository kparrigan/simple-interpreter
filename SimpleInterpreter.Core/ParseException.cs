using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class ParseException : Exception
    {
        public ParseException()
           : base ("Error Parsing Input") 
        {
        }
    }
}
