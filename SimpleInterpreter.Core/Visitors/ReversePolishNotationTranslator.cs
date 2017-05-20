using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Exceptions;
using SimpleInterpreter.Core.Nodes;

namespace SimpleInterpreter.Core.Visitors
{
    public sealed class ReversePolishNotationTranslator : NodeVisitor
    {
        private readonly Parser _parser;

        #region Constructor

        public ReversePolishNotationTranslator(Parser parser)
        {
            _parser = parser;
        }
        #endregion

        #region Public

        public string Interpret()
        {
            var tree = _parser.Parse();
            return Visit(tree).ToString();
        }
        #endregion

        #region Private

        private string VisitBinaryOperatorNode(BinaryOperatorNode node)
        {
            return Visit(node.Left) + " " + Visit(node.Right) + " " + node.Op.Value;
        }

        private string VisitNumberNode(NumberNode node)
        {
            return node.Value.ToString();
        }
        #endregion
    }
}
