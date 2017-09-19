using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Org.X2A.SpellChecker.Core
{
    [TestClass]
    public class TestWordTree
    {

        [TestMethod]
        public void TestAddWord()
        {
            WordTree tree = new WordTree();
            tree.Add("test");
            tree.Add("testt");
            tree.Add("tst");
        }

        [TestMethod]
        public void GetBestMatchEquals()
        {
            WordTree tree = new WordTree();
            tree.Add("test");
            tree.Add("testt");
            tree.Add("tst");

            List<string> matches = tree.GetBestMatch("tst");
            Assert.AreEqual(3, matches.Count);
            Assert.AreEqual("tst", matches[0]);
        }

        [TestMethod]
        public void TestGetCorrection()
        {
            WordTree tree = new WordTree(new string[] {"hell","help","shell","smell",
                           "fell","felt","oops","pop","oouch","halt"});

            List<string> matches = tree.GetBestMatch("ops", tolerance: 2);
            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual("oops", matches[0]);
        }
    }
}
