using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class NumberNode : ASTNode
    {
        private readonly Token _token;
        public int Value { get; }

        #region Constructor

        public NumberNode(Token token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Value = (int)token.Value;
        }
        #endregion
    }
}
