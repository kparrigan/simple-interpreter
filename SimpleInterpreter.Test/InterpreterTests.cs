using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;
using SimpleInterpreter.Core.Exceptions;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class InterpreterTests
    {
        [TestMethod]
        public void Can_Evaluate_Addition_Expression()
        {
            const int expected = 8;
            var lexer = new Lexer("3+5");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Subtraction_Expression()
        {
            const int expected = 2;
            var lexer = new Lexer("7-5");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Multiplication_Expression()
        {
            const int expected = 35;
            var lexer = new Lexer("7*5");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Division_Expression()
        {
            const int expected = 2;
            var lexer = new Lexer("4/2");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Multi_Digit_Tokens()
        {
            const int expected = 15;
            var lexer = new Lexer("12+3");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_WhiteSpace()
        {
            const int expected = 15;
            var lexer = new Lexer("12 + 3 ");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Arbitrary_Length_Expression()
        {
            const int expected = 18;
            var lexer = new Lexer("9 - 5 + 3 + 11");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Operator_Precendence()
        {
            const int expected = 17;
            var lexer = new Lexer("14 + 2 * 3 - 6 / 2");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Parens()
        {
            var expected = 5;
            var lexer = new Lexer("(2 * 2) + 1");
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            lexer = new Lexer("1 + (2 * 2)");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);
            Assert.AreEqual(expected, interpreter.Interpret());

            expected = 22;
            lexer = new Lexer("7 + 3 * (10 / (12 / (3 + 1) - 1))");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());

            expected = 12;
            lexer = new Lexer("7 + (((3 + 2)))");
            parser = new Parser(lexer);
            interpreter = new Interpreter(parser);

            Assert.AreEqual(expected, interpreter.Interpret());
        }

        [TestMethod]
        public void Can_Evaluate_Compound_Statement()
        {
            const string statement =
                @"BEGIN
                    BEGIN
                        number := 2;
                        a := number;
                        b := 10 * a + 10 * number / 4;
                        c := a - - b
                    END;
                    x := 11;
                END.";

            var lexer = new Lexer(statement);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);

            var results = interpreter.Interpret();
            Assert.AreEqual(2, (int)interpreter.GlobalScope["a"]);
            Assert.AreEqual(11, (int)interpreter.GlobalScope["x"]);
            Assert.AreEqual(27, (int)interpreter.GlobalScope["c"]);
            Assert.AreEqual(25, (int)interpreter.GlobalScope["b"]);
            Assert.AreEqual(2, (int)interpreter.GlobalScope["number"]);
        }

    }
}
