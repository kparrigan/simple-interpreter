using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core;
using SimpleInterpreter.Core.Nodes;
using SimpleInterpreter.Core.Visitors;

namespace SimpleInterpreter.Test.TestHelpers
{
    public class TestingVisitor : NodeVisitor
    {
        private bool VisitFooNode(ASTNode node)
        {
            return true;
        }
    }
}
