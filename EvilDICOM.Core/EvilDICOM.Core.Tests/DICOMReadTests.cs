using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DICOMReadTests
    {
        [TestMethod]
        public void ReadExplicityLittleEndian()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var elemCount = dcm.AllElements.Count;
            Assert.AreEqual(elemCount, 72);
        }

        [TestMethod]
        public void ReadMultipleFL()
        {
            var dcm = DICOMFileReader.Read(Resources.MultpleFL);
            var vmGreaterThan1 = dcm.FindAll("300A030A");
            Assert.IsTrue((vmGreaterThan1[0] as AbstractElement<float[]>).Data.Length > 1);
        }

        [TestMethod]
        public void ReadImplicitLittleEndian()
        {
            var dcm = DICOMFileReader.Read(Resources.implicitLittleEndian);
            var elemCount = dcm.AllElements.Count;
            Assert.AreEqual(elemCount, 47);
        }

        [TestMethod]
        public void ReadBigEndian()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitBigEndian);
            var elemCount = dcm.AllElements.Count;
            Assert.AreEqual(elemCount, 44);
        }

        [TestMethod]
        public void ReadJPEG()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleJPEG);
            var elemCount = dcm.AllElements.Count;
            Assert.AreEqual(elemCount, 80);
        }

    }
}
