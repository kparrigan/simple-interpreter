using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core.Exceptions;
using SimpleInterpreter.Core.Nodes;
using SimpleInterpreter.Test.TestHelpers;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class NodeVisitorTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidNodeTypeException))]
        public void Can_Handle_Invalid_Node_Type()
        {
            var visitor = new TestingVisitor();
            visitor.Visit(new BarNode());
        }

        [TestMethod]
        public void Can_Visit_Node()
        {
            var visitor = new TestingVisitor();
            var result = (bool)visitor.Visit(new FooNode());
            Assert.IsTrue(result);
        }
    }
}
