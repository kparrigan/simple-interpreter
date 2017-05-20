using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Nodes;

namespace SimpleInterpreter.Core.Visitors
{
    public sealed class LISPNotationTranslator : NodeVisitor
    {
        private readonly Parser _parser;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LISPNotationTranslator"/> class.
        /// </summary>
        /// <param name="parser">Expression parser.</param>
        public LISPNotationTranslator(Parser parser)
        {
            _parser = parser;
        }
        #endregion

        #region Public
        /// <summary>
        /// Interprets the expression.
        /// </summary>
        /// <returns>
        /// String - LISP-style parenthesized prefix notation version of the expression.
        /// </returns>
        public string Interpret()
        {
            var tree = _parser.Parse();
            return Visit(tree).ToString();
        }
        #endregion

        #region Private

        private string VisitBinaryOperatorNode(BinaryOperatorNode node)
        {
            return "(" + node.Op.Value + " " + Visit(node.Left) + " " + Visit(node.Right) + ")";
        }

        private string VisitNumberNode(NumberNode node)
        {
            return node.Value.ToString();
        }
        #endregion
    }
}
