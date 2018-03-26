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
    public sealed class Interpreter : NodeVisitor
    {
        private readonly Parser _parser;
        public Dictionary<string, object> GlobalScope { get; private set; }

        #region Constructor

        public Interpreter(Parser parser)
        {
            _parser = parser;
            GlobalScope = new Dictionary<string, object>();
        }
        #endregion

        #region Public

        public object Interpret()
        {
            var tree = _parser.Parse();
            return Visit(tree);
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

        private int VisitUnaryOperatorNode(UnaryOperatorNode node)
        {
            var opType = node.Op.Type;

            switch (opType)
            {
                case TokenType.PLUS:
                    return +(int)Visit(node.Expression);
                case TokenType.MINUS:
                    return -(int)Visit(node.Expression);
                default:
                    throw new InterpretationException($"Invalid node token type: {node.Op.Type}");
            }
        }

        private void VisitCompoundNode(CompoundNode node)
        {
            foreach (var child in node.Children)
            {
                Visit(child);
            }
        }

        private void VisitNoOpNode(NoOpNode node)
        {
            return;
        }

        private void VisitAssignNode(AssignNode node)
        {
            var varName = node.Left.Value.ToString();
            GlobalScope[varName] = Visit(node.Right);
        }

        private object VisitVarNode(VarNode node)
        {
            var varName = node.Value.ToString();

            if (!GlobalScope.ContainsKey(varName))
            {
                throw new VariableNameException(varName);
            }

            return GlobalScope[varName];
        }
        #endregion
    }
}