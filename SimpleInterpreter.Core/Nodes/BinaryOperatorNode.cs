using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class BinaryOperatorNode : ASTNode
    {
        public ASTNode Left { get; }
        public ASTNode Right { get;  }
        public Token Op { get; }
        private Token _token;

        #region Constructor
        /// <summary>
        /// AST Node that represents a binary operation. 
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="op">Operator.</param>
        /// <param name="right">Right operand.</param>
        public BinaryOperatorNode(ASTNode left, Token op, ASTNode right)
        {
            Left = left;
            Right = right;
            _token = Op = op;
        }
        #endregion
    }
}
