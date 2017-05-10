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
            var currentOp = TokenType.PLUS;
            var value = 0;
            var tokenCount = 1;

            _currentToken = GetNextToken();

            if (!int.TryParse(_currentToken.Value.ToString(), out value)) //balls
            {
                throw new ParseException();
            }

            Eat(TokenType.INTEGER);

            while (_currentToken.Type != TokenType.EOF)
            {
                tokenCount++;

                if (tokenCount % 2 == 0)
                {
                    currentOp = _currentToken.Type;
                    Eat(currentOp);
                }
                else
                {
                    value = ComputeValue(currentOp, (int)_currentToken.Value, value);
                    Eat(TokenType.INTEGER);
                }
            }

            return value;
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

        /// <remarks>This is obviously wrong from an order of operations standpoint.</remarks>
        private int ComputeValue(TokenType op, int tokenValue, int currentValue)
        {
            switch (op)
            {
                case TokenType.PLUS:
                    return currentValue + tokenValue;
                case TokenType.MINUS:
                    return currentValue - tokenValue;
                case TokenType.MULTIPLY:
                    return currentValue * tokenValue;
                case TokenType.DIVIDE:
                    return currentValue / tokenValue;
                default:
                    return currentValue + tokenValue;
            }
        }
        #endregion
    }
}
