using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Core
{
    public class Lexer
    {
        private readonly string _text;
        private char _currentChar;
        private int _pos;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Lexer"/> class.
        /// </summary>
        /// <param name="text">Text for lexical analysis.</param>
        /// <exception cref="ArgumentException">Thrown if text is null, empty, or whitespace.</exception>
        public Lexer(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"{nameof(text)} must have a value.", nameof(text));    
            }

            _text = text;
            _pos = 0;
            _currentChar = _text[_pos];
        }
        #endregion

        #region Public
        /// <summary>
        /// Responsible for breaking a sentence apart into tokens. One token at a time.
        /// </summary>
        /// <returns><see cref="Token"/>Next token from stream.</returns>
        /// <exception cref="InterpretationException">Thrown on errors encountered during lexing.</exception>
        public Token GetNextToken()
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

                if (_currentChar == '*')
                {
                    Advance();
                    return new Token(TokenType.MULTIPLY, '*');
                }

                if (_currentChar == '/')
                {
                    Advance();
                    return new Token(TokenType.DIVIDE, '/');
                }

                if (_currentChar == '(')
                {
                    Advance();
                    return new Token(TokenType.LEFT_PAREN, '(');
                }

                if (_currentChar == ')')
                {
                    Advance();
                    return new Token(TokenType.RIGHT_PAREN, ')');
                }

                Error();
            }

            return new Token(TokenType.EOF, null);
        }
        #endregion

        #region Private

        private void Error()
        {
            throw new InterpretationException("Invalid character");
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
