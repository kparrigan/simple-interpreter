using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class Token
    {
        public TokenType Type { get; set; }
        public object Value { get; set; }

        public Token(TokenType type, object value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"Token({Type}, { Value})";
        }
    }
}
