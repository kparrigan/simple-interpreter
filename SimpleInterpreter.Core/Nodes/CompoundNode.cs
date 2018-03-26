using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core.Nodes
{
    public sealed class CompoundNode : ASTNode
    {
        public List<ASTNode> Children { get; private set; }

        public CompoundNode()
        {
            Children = new List<ASTNode>();
        }
    }
}
