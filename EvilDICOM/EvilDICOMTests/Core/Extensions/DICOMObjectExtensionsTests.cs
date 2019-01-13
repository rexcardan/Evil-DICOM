using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Extensions.Tests
{
    [TestClass()]
    public class DICOMObjectExtensionsTests
    {
        [TestMethod()]
        public void RemoveMetaHeaderTest()
        {
            var dcm = new DICOMObject();
            dcm.Elements.Add(DICOMForge.FileMetaInformationGroupLength(0));
            dcm.Elements.Add(DICOMForge.FileMetaInformationVersion(0));
            dcm.Elements.Add(DICOMForge.PatientID("123456"));

            dcm.RemoveMetaHeader();
            var meta = dcm.Elements.Where(e => e.Tag.Group == "0002").ToList();
            Assert.AreEqual(0, meta.Count);
        }
    }
}