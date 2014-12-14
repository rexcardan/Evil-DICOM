using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.IO.Writing;
using System.Linq;
using EvilDICOM.Core.Element;
using System.IO;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DICOMWriteTests
    {
        [TestMethod]
        public void WriteLittleEndian()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);

            //Writing a file out
            DICOMFileWriter.WriteLittleEndian("myPath.dcm", dcm);
        }

        [TestMethod]
        public void WriteMultipleFl()
        {
            var vssd = DICOMForge.VirtualSourceAxisDistances;
            vssd.Data_ = new System.Collections.Generic.List<float>() { 2538.4199f, 2541.00f };
            byte[] elBytes = null;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    DICOMElementWriter.Write(dw, DICOMWriteSettings.Default(), vssd);
                    elBytes = stream.ToArray();
                }
            }

            AbstractElement<float> readVssd = null;
            using (var dr = new DICOMBinaryReader(elBytes))
            {
                readVssd = DICOMElementReader.ReadElementExplicitLittleEndian(dr) as AbstractElement<float>;
            }

            Assert.AreEqual(readVssd.Data_.Count, 2);
            Assert.AreEqual(readVssd.Data_[0], 2538.4199f);
            Assert.AreEqual(readVssd.Data_[1], 2541.00f);
        }
    }
}
