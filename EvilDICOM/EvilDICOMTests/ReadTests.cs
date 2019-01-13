using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOMTests
{
    [TestClass]
    public class ReadTests
    {
        [TestMethod]
        public void ReadBigEndian()
        {
            var dcm = DICOMObject.Read(Properties.Resources.explicitBigEndian);
            Assert.AreEqual(dcm.SOPClass, SOPClass.UltrasoundImageStorage);
            Assert.AreEqual(dcm.Elements.Count, 44);
        }

        [TestMethod]
        public void ReadXLittleEndian()
        {
            var dcm = DICOMObject.Read(Properties.Resources.explicitLittleEndian);
            Assert.AreEqual(dcm.Elements.Count, 72);
            Assert.AreEqual(dcm.GetSelector().SliceThickness.Data, 10);
        }

        [TestMethod]
        public void ReadMetaXLittleEndian()
        {
            var dcm = DICOMFileReader.ReadFileMetadata(Properties.Resources.explicitLittleEndian);
            Assert.AreEqual(dcm.Elements.Count, 7);
        }
    }
}
