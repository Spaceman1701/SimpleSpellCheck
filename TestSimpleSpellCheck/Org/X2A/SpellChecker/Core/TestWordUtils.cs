using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace Org.X2A.SpellChecker.Core
{
    [TestClass]
    public class TestWordUtils
    {

        [TestMethod]
        public void TestEqual()
        {
            int distance = WordUtils.EditDistance("Hello", "Hello");
            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public void TestTransposition()
        {
            int distance = WordUtils.EditDistance("Hello", "Helol");
            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestSubstution()
        {
            int distance = WordUtils.EditDistance("Hello", "Heloo");
            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestInsertion()
        {
            int distance = WordUtils.EditDistance("Hello", "Helloo");
            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestFrontDeletion()
        {
            int distance = WordUtils.EditDistance("Hello", "ello");
            Assert.AreEqual(1, distance);
        }
    }
}
