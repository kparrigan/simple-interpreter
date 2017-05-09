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
        private readonly string _text;
        private int _pos;
        private Token _currentToken;
        private char _currentChar;

        #region Constructor

        public Interpreter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ParseException();
            }

            _text = text;
            _pos = 0;
            _currentToken = null;
            _currentChar = _text[_pos];
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
                    return (int) left.Value + (int) right.Value;
                case TokenType.MINUS:
                    return (int) left.Value - (int) right.Value;
                case TokenType.MULTIPLY:
                    return (int)left.Value * (int)right.Value;
                case TokenType.DIVIDE:
                    return (int)left.Value / (int)right.Value;
                default:
                    return (int) left.Value + (int) right.Value;
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
            while (!_currentChar.IsNull())
            {
                if (char.IsWhiteSpace(_currentChar))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit(_currentChar))
                {
                    return new Token(TokenType.INTEGER, GetNextInteger());
                }

                if (_currentChar == '+')
                {
                    Advance();
                    return new Token(TokenType.PLUS, '+');
                }

                if (_currentChar == '-')
                {
                    Advance();
                    return new Token(TokenType.MINUS, '-');
                }

                if (_currentChar == 'x')
                {
                    Advance();
                    return new Token(TokenType.MULTIPLY, 'x');
                }

                if (_currentChar == '/')
                {
                    Advance();
                    return new Token(TokenType.DIVIDE, '/');
                }

                throw new ParseException();
            }

            return new Token(TokenType.EOF, null);
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

        private void Advance()
        {
            _pos++;

            if (_pos > _text.Length - 1)
            {
                _currentChar = Constants.NullChar;
            }
            else
            {
                _currentChar = _text[_pos];
            }
        }

        private void SkipWhitespace()
        {
            while (!_currentChar.IsNull() && char.IsWhiteSpace(_currentChar))
            {
                Advance();
            }
        }

        private int GetNextInteger()
        {
            var temp = "";
            while (!_currentChar.IsNull() && char.IsDigit(_currentChar))
            {
                temp += _currentChar;
                Advance();
            }

            return int.Parse(temp);
        }

        #endregion
    }
}
