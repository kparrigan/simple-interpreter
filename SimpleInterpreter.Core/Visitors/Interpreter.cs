using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Exceptions;
using SimpleInterpreter.Core.Nodes;
using SimpleInterpreter.Core.Visitors;

namespace SimpleInterpreter.Core
{
    public class Interpreter : NodeVisitor
    {
        private readonly Parser _parser;

        #region Constructor

        public Interpreter(Parser parser)
        {
            _parser = parser;
        }
        #endregion

        #region Public

        public int Interpret()
        {
            var tree = _parser.Parse();
            return (int)Visit(tree);
        }
        #endregion

        #region Private

        private int VisitBinaryOperatorNode(BinaryOperatorNode node)
        {
            switch (node.Op.Type)
            {
                case TokenType.PLUS:
                    return (int)Visit(node.Left) + (int)Visit(node.Right);
                case TokenType.MINUS:
                    return (int)Visit(node.Left) - (int)Visit(node.Right);
                case TokenType.MULTIPLY:
                    return (int)Visit(node.Left) * (int)Visit(node.Right);
                case TokenType.DIVIDE:
                    return (int)Visit(node.Left) / (int)Visit(node.Right);
                default:
                    throw new InterpretationException($"Invalid node token type: {node.Op.Type}" );

            }
        }

        private int VisitNumberNode(NumberNode node)
        {
            return node.Value;
        }
        #endregion
    }
}
