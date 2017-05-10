using System;
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
            var interpreter = new Interpreter("3+5");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Subtraction_Expression()
        {
            const int expected = 2;
            var interpreter = new Interpreter("7-5");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Multiplication_Expression()
        {
            const int expected = 35;
            var interpreter = new Interpreter("7x5");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Evaluate_Division_Expression()
        {
            const int expected = 2;
            var interpreter = new Interpreter("4/2");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Handle_Multi_Digit_Tokens()
        {
            const int expected = 15;
            var interpreter = new Interpreter("12+3");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        public void Can_Handle_WhiteSpace()
        {
            const int expected = 15;
            var interpreter = new Interpreter("12 + 3 ");

            Assert.AreEqual(expected, interpreter.Expression());
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void Can_Handle_Invalid_Expression()
        {
            var interpreter = new Interpreter("+12 3");
            interpreter.Expression();
        }

        [TestMethod]
        [ExpectedException(typeof(ParseException))]
        public void Can_Handle_Empty_Expression()
        {
            var interpreter = new Interpreter("");
            interpreter.Expression();
        }

        [TestMethod]
        public void Can_Handle_Arbitrary_Length_Expression()
        {
            const int expected = 18;
            var interpreter = new Interpreter("9 - 5 + 3 + 11");

            Assert.AreEqual(expected, interpreter.Expression());
        }
    }
}
