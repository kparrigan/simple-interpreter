using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class UnaryOperatorNode : ASTNode
    {
        public ASTNode Expression { get; }
        public Token Op { get; }
        private Token _token;

        #region Constructor

        public UnaryOperatorNode(Token op, ASTNode expression)
        {
            _token = Op = op;
            Expression = expression;
        }
        #endregion
    }
}
