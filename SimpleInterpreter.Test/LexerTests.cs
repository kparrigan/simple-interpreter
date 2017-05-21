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

        [TestMethod]
        public void Can_Get_Assignment_Statement()
        {
            const string varName = "myVar";
            const string assign = ":=";
            const int value = 1;
            const char semi = ';';

            var lexer = new Lexer($"{varName} {assign} {value}{semi}");
            var token1 = lexer.GetNextToken();
            var token2 = lexer.GetNextToken();
            var token3 = lexer.GetNextToken();
            var token4 = lexer.GetNextToken();

            Assert.AreEqual(TokenType.ID, token1.Type);
            Assert.AreEqual(varName, token1.Value.ToString());

            Assert.AreEqual(TokenType.ASSIGN, token2.Type);
            Assert.AreEqual(assign, token2.Value.ToString());

            Assert.AreEqual(TokenType.INTEGER, token3.Type);
            Assert.AreEqual(value, (int)token3.Value);

            Assert.AreEqual(TokenType.SEMI, token4.Type);
            Assert.AreEqual(semi, (char)token4.Value);
        }

        [TestMethod]
        public void Can_Get_Empty_Compound_Statement()
        {
            const string begin = "BEGIN";
            const string end = "END";
            const char dot = '.';

            var lexer = new Lexer($"{begin} {end}{dot}");
            var token1 = lexer.GetNextToken();
            var token2 = lexer.GetNextToken();
            var token3 = lexer.GetNextToken();

            Assert.AreEqual(TokenType.BEGIN, token1.Type);
            Assert.AreEqual(begin, token1.Value.ToString());

            Assert.AreEqual(TokenType.END, token2.Type);
            Assert.AreEqual(end, token2.Value.ToString());

            Assert.AreEqual(TokenType.DOT, token3.Type);
            Assert.AreEqual(dot, (char)token3.Value);
        }
    }
}
