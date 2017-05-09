using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class CharExtensionTests
    {
        [TestMethod]
        public void Can_Evaluate_Null()
        {
            Assert.AreEqual(true, '\0'.IsNull());
            Assert.AreEqual(false, '1'.IsNull());
        }
    }
}
