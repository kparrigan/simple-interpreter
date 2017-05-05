using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInterpreter.Core;

namespace SimpleInterpreter.Test
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Can_Strip_White_Space()
        {
            const string actual = "the quick brown fox";
            const string expected = "thequickbrownfox";
            Assert.AreEqual(actual.StripWhiteSpace(), expected);
        }
    }
}
