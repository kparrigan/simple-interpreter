using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Can_Handle_Empty_Expression()
        {
            var lexer = new Lexer("");
        }

        [TestMethod]
        public void Can_Get_Next_Token()
        {
            var lexer = new Lexer("12 + 1");
            var token1 = lexer.GetNextToken();
            var token2 = lexer.GetNextToken();
            var token3 = lexer.GetNextToken();

            Assert.AreEqual(TokenType.INTEGER, token1.Type);
            Assert.AreEqual(12, (int)token1.Value);

            Assert.AreEqual(TokenType.PLUS, token2.Type);
            Assert.AreEqual('+', (char)token2.Value);

            Assert.AreEqual(TokenType.INTEGER, token3.Type);
            Assert.AreEqual(1, (int)token3.Value);
        }
    }
}
