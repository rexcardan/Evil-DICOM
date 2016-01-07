using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvilDICOM.Core.Tests
{
    
    [TestClass]
    public class ElementReadTests
    {
        /// <summary>
        ///     The first is a private element
        /// </summary>
        [TestMethod]
        public void ReadFirstElement()
        {
            var dcm = DICOMObject.Read(EvilDICOMTests.Properties.Resources.PrivateElements);
            var expected = dcm.FindFirst(TagHelper.COLUMNS);
            Assert.AreEqual(expected.DData.ToString(), "128");
        }

        /// <summary>
        ///     filter private elements
        /// </summary>
        [TestMethod]
        public void ReadFirstPublicElement()
        {
            var dcm = DICOMObject.Read(EvilDICOMTests.Properties.Resources.PrivateElements);
            var expected = dcm.FindPublicFirst(TagHelper.COLUMNS);
            Assert.AreEqual(expected.DData.ToString(), "512");
        }
    }
}
