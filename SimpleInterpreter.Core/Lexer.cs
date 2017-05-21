using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Exceptions;

namespace SimpleInterpreter.Core
{
    public class Lexer
    {
        private readonly string _text;
        private char _currentChar;
        private int _pos;
        private readonly Dictionary<string, Token> _reservedKeywords;

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

            _reservedKeywords = new Dictionary<string, Token>()
            {
                {"BEGIN", new Token(TokenType.BEGIN, "BEGIN")},
                {"END", new Token(TokenType.END, "END")}
            };
        }
        #endregion

        #region Public
        /// <summary>
        /// Responsible for breaking a sentence apart into tokens. One token at a time.
        /// </summary>
        /// <returns><see cref="Token"/>Next token from stream.</returns>
        /// <exception cref="LexingException">Thrown on errors encountered during lexing.</exception>
        public Token GetNextToken()
        {
            while (!_currentChar.IsNull())
            {
                if (char.IsLetter(_currentChar))
                {
                    return Id();
                }


                if (_currentChar == Constants.Colon && Peek() == Constants.Equal)
                {
                    Advance();
                    Advance();
                    return new Token(TokenType.ASSIGN, Constants.Assignment);
                }


                if (_currentChar == Constants.SemiColon)
                {
                    Advance();
                    return new Token(TokenType.SEMI, Constants.SemiColon);
                }

                if (_currentChar == Constants.Dot)
                {
                    Advance();
                    return new Token(TokenType.DOT, Constants.Dot);
                }

                if (char.IsWhiteSpace(_currentChar))
                {
                    SkipWhitespace();
                    continue;
                }

                if (char.IsDigit(_currentChar))
                {
                    return new Token(TokenType.INTEGER, GetNextInteger());
                }

                if (_currentChar == Constants.Plus)
                {
                    Advance();
                    return new Token(TokenType.PLUS, Constants.Plus);
                }

                if (_currentChar == Constants.Minus)
                {
                    Advance();
                    return new Token(TokenType.MINUS, Constants.Minus);
                }

                if (_currentChar == Constants.Multiply)
                {
                    Advance();
                    return new Token(TokenType.MULTIPLY, Constants.Multiply);
                }

                if (_currentChar == Constants.Divide)
                {
                    Advance();
                    return new Token(TokenType.DIVIDE, Constants.Divide);
                }

                if (_currentChar == Constants.LeftParen)
                {
                    Advance();
                    return new Token(TokenType.LEFT_PAREN, Constants.LeftParen);
                }

                if (_currentChar == Constants.RightParen)
                {
                    Advance();
                    return new Token(TokenType.RIGHT_PAREN, Constants.RightParen);
                }

                Error();
            }

            return new Token(TokenType.EOF, null);
        }
        #endregion

        #region Private

        private void Error()
        {
            throw new LexingException("Invalid character");
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

        private char Peek()
        {
            var peekPos = _pos + 1;

            if (peekPos > _text.Length - 1)
            {
                return Constants.NullChar;
            }

            return _text[peekPos];           
        }

        /// <summary>
        /// Handles identifiers and reserved keywords
        /// </summary>
        /// <returns>Token for identifier or reserved keyword.</returns>
        private Token Id()
        {
            var resultBuilder = new StringBuilder();

            while (!_currentChar.IsNull() && char.IsLetterOrDigit(_currentChar))
            {
                resultBuilder.Append(_currentChar);
                Advance();
            }

            var result = resultBuilder.ToString();
            var defaultToken = new Token(TokenType.ID, result);

            return _reservedKeywords.ContainsKey(result) 
                ? _reservedKeywords[result]
                : defaultToken;
        }
        #endregion
    }
}
