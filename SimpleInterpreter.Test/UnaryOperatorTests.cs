using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class UnaryOperatorTests
    {
        [TestMethod]
        public void Can_Evaluate_Unary_Operators()
        {
            var expected = -3;
            var lexer = new Lexer("- 3");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            expected = 3;
            lexer = new Lexer("+ 3");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            expected = 8;
            lexer = new Lexer("5 - - - + - 3");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            expected = 10;
            lexer = new Lexer("5 - - - + - (3 + 4) - +2");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }
    }
}
