using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class Interpreter
    {
        private readonly string _text;
        private int _pos;
        private Token _currentToken;

        #region Constructor
        public Interpreter(string text)
        {
            _text = text;
            _pos = 0;
            _currentToken = null;
        }
        #endregion

        #region Public
        public int Expression()
        {
            //set current token to the first token taken from the input
            _currentToken = GetNextToken();

            //we expect the current token to be an integer
            var left = _currentToken;
            Eat(TokenType.INTEGER);

            //middle token should be operation.
            var op = _currentToken.Type;
            Eat(_currentToken.Type);

            //we expect the current token to be an integer
            var right = _currentToken;
            Eat(TokenType.INTEGER);

            switch (op)
            {
                case TokenType.PLUS:
                    return (int)left.Value + (int)right.Value;
                case TokenType.MINUS:
                    return (int)left.Value - (int)right.Value;
                default:
                    return (int)left.Value + (int)right.Value;
            }
        }
        #endregion

        #region Private 
        /// <summary>
        /// Responsible for breaking a sentence apart into tokens. One token at a time.
        /// </summary>
        /// <returns><see cref="Token"/></returns>
        private Token GetNextToken()
        {
            // return EOF if we're past the last token.
            if (_pos > _text.Length - 1)
            {
                return new Token(TokenType.EOF, null);
            }

            var current = _text[_pos];

            if (current == '+')
            {
                _pos++;
                return new Token(TokenType.PLUS, current);
            }

            if (current == '-')
            {
                _pos++;
                return new Token(TokenType.MINUS, current);
            }

            //scan from our current token to find the next non-integer token
            var i = _pos + 1;            
            for (var len = _text.Length; i < len; i++)
            {
                if (IsOpSymbol(_text[i]))
                {
                    break;
                }
            }

            var diff = i - _pos;
            var tokenText = _text.Substring(_pos, diff);
            _pos += diff;
            return new Token(TokenType.INTEGER, int.Parse(tokenText));
        }

        private void Eat(TokenType type)
        {
            if (_currentToken.Type == type)
            {
                _currentToken = GetNextToken();
            }
            else
            {
                throw new ParseException();
            }
        }

        private static bool IsOpSymbol(char c)
        {
            return c == '+' || c == '-';
        }
        #endregion
    }
}
