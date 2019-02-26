using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiHash.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiHash.Models.Articles.Tests
{
    [TestClass()]
    public class ArticlesPathGeneratorTests
    {
        [TestMethod()]
        public void GenerateTest()
        {
            var expected = "Content/Articles/link-example.txt";
            var actual = ArticlesPathGenerator.Generate("link-example");

            Assert.AreEqual(expected, actual);
        }
    }
}