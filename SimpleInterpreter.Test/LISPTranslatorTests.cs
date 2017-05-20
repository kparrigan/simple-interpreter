using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;
using SimpleInterpreter.Core.Visitors;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class LISPTranslatorTests
    {
        [TestMethod]
        public void Can_Translate_To_LISP()
        {
            var expected = "(+ 2 3)";
            var lexer = new Lexer("2 + 3");
            var parser = new Parser(lexer);
            var interpreter = new LISPNotationTranslator(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            expected = "(+ 2 (* 3 5))";
            lexer = new Lexer("(2 + 3 * 5)");
            parser = new Parser(lexer);
            interpreter = new LISPNotationTranslator(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }
    }
}
