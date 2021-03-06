﻿using System;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="ReversePolishNotationTranslator"/> class.
        /// </summary>
        /// <param name="parser">Expression parser.</param>
        public ReversePolishNotationTranslator(Parser parser)
        {
            _parser = parser;
        }
        #endregion

        #region Public
        /// <summary>
        /// Interprets the expression.
        /// </summary>
        /// <returns>
        /// String - Reverse Polign Notation version of the expression.
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
            return Visit(node.Left) + " " + Visit(node.Right) + " " + node.Op.Value;
        }

        private string VisitNumberNode(NumberNode node)
        {
            return node.Value.ToString();
        }
        #endregion
    }
}
