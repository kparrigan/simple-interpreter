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
        ///     expr   : term((PLUS | MINUS) term)*
        ///     term   : factor((MUL | DIV) factor)*
        ///     factor : INTEGER | LPAREN expr RPAREN
        /// </remarks>
        /// <exception cref="InterpretationException">Thrown on errors during interpretation.</exception>
        public int Expression()
        {
            var result = Term();

            while (_currentToken.Type == TokenType.PLUS || _currentToken.Type == TokenType.MINUS)
            {
                var token = _currentToken;
                switch (token.Type)
                {
                    case TokenType.PLUS:
                        Eat(TokenType.PLUS);
                        result += Term();
                        break;
                    case TokenType.MINUS:
                        Eat(TokenType.MINUS);
                        result -= Term();
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
        /// <remarks>factor : INTEGER | LPAREN expr RPAREN</remarks>
        private int Factor()
        {
            int result;
            var token = _currentToken;

            if (token.Type == TokenType.INTEGER)
            {
                Eat(TokenType.INTEGER);
                result = (int)token.Value;
            }
            else
            {
                Eat(TokenType.LEFT_PAREN);
                result = Expression();
                Eat(TokenType.RIGHT_PAREN);
            }

            return result;
        }

        /// <summary>
        /// Performs computation for multiply/divide operator production rule.
        /// </summary>
        /// <returns>Integer value of operation.</returns>
        /// <remarks>term : factor ((MUL | DIV) factor)</remarks>
        private int Term()
        {
            var result = Factor();

            while (_currentToken.Type == TokenType.MULTIPLY || _currentToken.Type == TokenType.DIVIDE)
            {
                var token = _currentToken;
                switch(token.Type)
                {
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
