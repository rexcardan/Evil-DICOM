using EvilDICOM.Core;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using EvilDICOMTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOMTests
{
    [TestClass]
    public class XmlTests
    {
        [TestMethod]
        public void ToXML()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var xml = dcm.ToXML();
            var dcm2 = DICOMObject.FromXML(xml);

            Assert.AreEqual(dcm.Elements.Count, dcm2.Elements.Count);
            Assert.AreEqual(dcm.AllElements.Count, dcm2.AllElements.Count);

            for (int i = 0; i < dcm.Elements.Count; i++)
            {
                var el1 = dcm.Elements[i];
                var el2 = dcm2.Elements[i];
                CollectionAssert.AreEquivalent(el1.DData_, el2.DData_);
            }

        }
    }
}
