using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Dictionaries;

namespace EvilDICOM.Core.Tests
{
    /// <summary>
    /// Summary description for TagTests
    /// </summary>
    [TestClass]
    public class TagTests
    {
        [TestMethod]
        public void ToStringTest()
        {
           var tag = new Tag("00020012");
           var asString = tag.ToString();
           var expected = string.Format("({0},{1}) : {2}", tag.Group, tag.Element, "ImplementationClassUID");
           Assert.AreEqual(expected, tag.ToString());
        }
    }
}
