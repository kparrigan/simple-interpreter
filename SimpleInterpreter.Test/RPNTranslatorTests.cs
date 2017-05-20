using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;
using SimpleInterpreter.Core.Visitors;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class RPNTranslatorTests
    {
        [TestMethod]
        public void Can_Translate_To_RPN()
        {
            const string expected = "5 3 + 12 * 3 /";
            var lexer = new Lexer("(5 + 3) * 12 / 3");
            var parser = new Parser(lexer);
            var interpreter = new ReversePolishNotationTranslator(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }
    }
}
