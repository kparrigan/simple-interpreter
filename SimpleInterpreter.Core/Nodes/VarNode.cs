using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class VarNode : ASTNode
    {
        public Token Token { get; private set; }
        public object Value { get; private set; }

        public VarNode(Token token)
        {
            Token = token;
            Value = token.Value;
        }
    }
}
