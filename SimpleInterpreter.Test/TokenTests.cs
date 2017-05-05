using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class TokenTests
    {
        [TestMethod]
        public void Can_Return_String_Representation()
        {
            const string expected = "Token(INTEGER, 99)";
            var token = new Token(TokenType.INTEGER, 99);

            Assert.AreEqual(token.ToString(), expected);
        }
    }
}
