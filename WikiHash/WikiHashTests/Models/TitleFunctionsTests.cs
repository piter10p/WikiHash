using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiHash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiHash.Models.Tests
{
    [TestClass()]
    public class TitleFunctionsTests
    {
        [TestMethod()]
        public void GenerateLinkTest()
        {
            var title = "Category: \"Some Title\" by Someone? 1234567890 ŁŹĆ 雷 ривет м";
            var expected = "category-some-title-by-someone-1234567890-łźć-雷-ривет-м";
            var actual = TitleFunctions.GenerateLink(title);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GenerateLinkTitleNotValidTest()
        {
            var title = "Category: \"Some Title\" by Someone? 1234567890 ŁŹĆ 雷  ривет м @#$^$^\\";
            try
            {
                TitleFunctions.GenerateLink(title);
            }
            catch(Exception e)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void ValidateTitleTest()
        {
            var validTitle = "Category: \"Some Title\" by Someone? 1234567890 ŁŹĆ 雷  ривет м";
            var unvalidTitle = "Category: \"Some Title\" by Someone? 1234567890 ŁŹĆ 雷  ривет м @#$^$^\\";

            Assert.AreEqual(true, TitleFunctions.Validate(validTitle));
            Assert.AreEqual(false, TitleFunctions.Validate(unvalidTitle));
        }
    }
}