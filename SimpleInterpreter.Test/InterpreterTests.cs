﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

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
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Subtraction_Expression()
        {
            const int expected = 2;
            var lexer = new Lexer("7-5");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Multiplication_Expression()
        {
            const int expected = 35;
            var lexer = new Lexer("7*5");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Division_Expression()
        {
            const int expected = 2;
            var lexer = new Lexer("4/2");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Handle_Multi_Digit_Tokens()
        {
            const int expected = 15;
            var lexer = new Lexer("12+3");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Handle_WhiteSpace()
        {
            const int expected = 15;
            var lexer = new Lexer("12 + 3 ");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        [ExpectedException(typeof(InterpretationException))]
        public void Can_Handle_Invalid_Expression()
        {
            var lexer = new Lexer("+12 3");
            var interpreter = new Interpreter(lexer);
            interpreter.Expression();

            lexer = new Lexer("1 + ( 2 * 2");
            interpreter = new Interpreter(lexer);
            interpreter.Expression();

            lexer = new Lexer("1 + 2 * 2)");
            interpreter = new Interpreter(lexer);
            interpreter.Expression();
        }

        [TestMethod]
        public void Can_Handle_Arbitrary_Length_Expression()
        {
            const int expected = 18;
            var lexer = new Lexer("9 - 5 + 3 + 11");
            var interpreter = new Interpreter(lexer);

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Operator_Precendence()
        {
            const int expected = 17;
            var lexer = new Lexer("14 + 2 * 3 - 6 / 2");
            var interpreter = new Interpreter(lexer);
            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Parens()
        {
            var expected = 5;
            var lexer = new Lexer("(2 * 2) + 1");
            var interpreter = new Interpreter(lexer);
            Assert.AreEqual(expected, interpreter.Expression());

            lexer = new Lexer("1 + (2 * 2)");
            interpreter = new Interpreter(lexer);
            Assert.AreEqual(expected, interpreter.Expression());

            expected = 22;
            lexer = new Lexer("7 + 3 * (10 / (12 / (3 + 1) - 1))");
            interpreter = new Interpreter(lexer);
            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Can_Handle_Null_Lexer()
        {
            var interpreter = new Interpreter(null);
        }
    }
}
