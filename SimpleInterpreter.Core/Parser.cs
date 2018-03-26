using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Exceptions;
using SimpleInterpreter.Core.Nodes;

namespace SimpleInterpreter.Core
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _currentToken;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Parser"/> class.
        /// </summary>
        /// <param name="lexer">Performs lexical analysis.</param>
        /// <exception cref="ArgumentException">Thrown if lexer is null.</exception>
        public Parser(Lexer lexer)
        {
            _lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
            _currentToken = _lexer.GetNextToken();
        }

        #endregion

        #region Public
        /// <summary>
        /// Parses an expression.
        /// </summary>
        /// <returns><see cref="BinaryOperatorNode"/>Root of syntax tree.</returns>
        public ASTNode Parse()
        {
            //handling single expressions as well as compound statements
            if (_currentToken.Type != TokenType.BEGIN)
            {
                return Expression();
            }
            else
            {
                var node = Program();

                if (_currentToken.Type != TokenType.EOF)
                {
                    Error();
                }

                return node;
            }
        }

        #endregion

        #region Private 
        /// <summary>
        /// Parses an expression.
        /// </summary>
        /// <returns><see cref="BinaryOperatorNode"/>Root of syntax tree.</returns>
        /// <remarks>
        ///     factor : PLUS FACTOR | MINUS FACTOR | INTEGER | LPAREN expr RPAREN | variable
        /// </remarks>
        /// <exception cref="ParsingException">Thrown on errors during parsing.</exception>
        private ASTNode Expression()
        {
            var node = Term();

            while (_currentToken.Type == TokenType.PLUS || _currentToken.Type == TokenType.MINUS)
            {
                var token = _currentToken;
                switch (token.Type)
                {
                    case TokenType.PLUS:
                        Eat(TokenType.PLUS);
                        break;
                    case TokenType.MINUS:
                        Eat(TokenType.MINUS);
                        break;
                    default:
                        Error();
                        break;
                }

                node = new BinaryOperatorNode(node, token, Term());
            }

            return node;
        }

        /// <summary>
        /// Returns the Node of the current factor.
        /// </summary>
        /// <returns><see cref="ASTNode"/>Current node for AST.</returns>
        /// <remarks>factor : (PLUS | MINUS) factor | INTEGER | LPAREN expr RPAREN</remarks>
        private ASTNode Factor()
        {
            var token = _currentToken;

            switch (token.Type)
            {
                case TokenType.PLUS:
                    Eat(TokenType.PLUS);
                    return new UnaryOperatorNode(token, Factor());
                case TokenType.MINUS:
                    Eat(TokenType.MINUS);
                    return new UnaryOperatorNode(token, Factor());
                case TokenType.INTEGER:
                    Eat(TokenType.INTEGER);
                    return new NumberNode(token);
                case TokenType.LEFT_PAREN:
                    Eat(TokenType.LEFT_PAREN);
                    var node = Expression();
                    Eat(TokenType.RIGHT_PAREN);
                    return node;
                default:
                    return Variable();
            }
        }

        /// <summary>
        /// Returns the Node of the current term.
        /// </summary>
        /// <returns><see cref="ASTNode"/>Current node for AST.</returns>
        /// <remarks>term : factor ((MUL | DIV) factor)</remarks>
        private ASTNode Term()
        {
            var node = Factor();

            while (_currentToken.Type == TokenType.MULTIPLY || _currentToken.Type == TokenType.DIVIDE)
            {
                var token = _currentToken;
                switch(token.Type)
                {
                    case TokenType.MULTIPLY:
                        Eat(TokenType.MULTIPLY);
                        break;
                    case TokenType.DIVIDE:
                        Eat(TokenType.DIVIDE);
                        break;
                    default:
                        Error();
                        break;
                }

                node = new BinaryOperatorNode(node, token, Factor());
            }

            return node;
        }

        /// <summary>
        /// Compare the current token type with the supplied token type. If they match, 'eat' the current token and 
        /// assign the next token to the current token. Otherwise, raise exception.
        /// </summary>
        /// <param name="type">Expected type of current token.</param>
        private void Eat(TokenType type)
        {
            if (_currentToken.Type == type)
            {
                _currentToken = _lexer.GetNextToken();
            }
            else
            {
                Error();
            }
        }

        private void Error()
        {
            throw new ParsingException("Invalid syntax");
        }

        private CompoundNode Program()
        {
            var node = CompoundStatement();
            Eat(TokenType.DOT);
            return node;
        }

        private CompoundNode CompoundStatement()
        {
            Eat(TokenType.BEGIN);
            var nodes = StatementList();
            Eat(TokenType.END);

            var root = new CompoundNode();
            foreach(var node in nodes)
            {
                root.Children.Add(node);
            }

            return root;
        }

        private List<ASTNode> StatementList()
        {
            var results = new List<ASTNode>();
            var node = Statement();

            results.Add(node);

            while (_currentToken.Type == TokenType.SEMI)
            {
                Eat(TokenType.SEMI);
                results.Add(Statement());
            }

            if (_currentToken.Type == TokenType.ID)
            {
                Error();
            }

            return results;
        }

        private ASTNode Statement()
        {
            if (_currentToken.Type == TokenType.BEGIN)
            {
                return CompoundStatement();
            }
            else if (_currentToken.Type == TokenType.ID)
            {
                return AssignmentStatement();
            }
            else
            {
                return Empty();
            }
        }


        private AssignNode AssignmentStatement()
        {
            var left = Variable();
            var token = _currentToken;

            Eat(TokenType.ASSIGN);
            var right = Expression();
            return new AssignNode(left, token, right);
        }

        private VarNode Variable()
        {
            var node = new VarNode(_currentToken);
            Eat(TokenType.ID);
            return node;
        }
        
        private NoOpNode Empty()
        {
            return new NoOpNode();
        }
        #endregion
    }
}
