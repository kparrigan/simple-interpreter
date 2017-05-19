using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;
using SimpleInterpreter.Core.Exceptions;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Can_Handle_Null_Lexer()
        {
            var parser = new Parser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Can_Handle_Invalid_Expression()
        {
            var lexer = new Lexer("+12 3");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            interpreter.Interpret();

            lexer = new Lexer("1 + ( 2 * 2");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);
            interpreter.Interpret();

            lexer = new Lexer("1 + 2 * 2)");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);
            interpreter.Interpret();
        }
    }
}
