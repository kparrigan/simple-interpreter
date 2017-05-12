using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class Interpreter
    {
        private readonly Lexer _lexer;
        private Token _currentToken;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Interpreter"/> class.
        /// </summary>
        /// <param name="lexer">Performs lexical analysis.</param>
        /// <exception cref="ArgumentException">Thrown if lexer is null.</exception>
        public Interpreter(Lexer lexer)
        {
            _lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
            _currentToken = _lexer.GetNextToken();
        }

        #endregion

        #region Public
        /// <summary>
        /// Parses/interprets an arithmetic expression.
        /// </summary>
        /// <returns>int - results of expression</returns>
        /// <remarks>
        /// expr       : factor ((ADD | SUB | MUL | DIV) factor)*
        /// factor     : INTEGER
        /// </remarks>
        /// <exception cref="InterpretationException">Thrown on errors during interpretation.</exception>
        public int Expression()
        {
            var result = Factor();

            while (_currentToken.Type == TokenType.PLUS || _currentToken.Type == TokenType.MINUS ||
                   _currentToken.Type == TokenType.MULTIPLY || _currentToken.Type == TokenType.DIVIDE)
            {
                var token = _currentToken;
                switch (token.Type)
                {
                    case TokenType.PLUS:
                        Eat(TokenType.PLUS);
                        result += Factor();
                        break;
                    case TokenType.MINUS:
                        Eat(TokenType.MINUS);
                        result -= Factor();
                        break;
                    case TokenType.MULTIPLY:
                        Eat(TokenType.MULTIPLY);
                        result *= Factor();
                        break;
                    case TokenType.DIVIDE:
                        Eat(TokenType.DIVIDE);
                        result /= Factor();
                        break;
                    default:
                        Error();
                        break;
                }
            }

            return result;
        }

        #endregion

        #region Private 
        private void Error()
        {
            throw new InterpretationException("Invalid syntax");
        }

        /// <summary>
        /// Returns the value of the current integer token.
        /// </summary>
        /// <returns>Integer value of current token.</returns>
        /// <remarks>factor : INTEGER</remarks>
        private int Factor()
        {
            var token = _currentToken;
            Eat(TokenType.INTEGER);
            return (int)token.Value;
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
        #endregion
    }
}
