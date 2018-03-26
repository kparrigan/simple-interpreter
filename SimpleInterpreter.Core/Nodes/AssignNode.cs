using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class AssignNode : ASTNode
    {
        public VarNode Left { get; }
        public ASTNode Right { get; }
        public Token Op { get; }

        public AssignNode(VarNode left, Token op, ASTNode right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

    }
}
