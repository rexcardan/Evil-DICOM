using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOMTests.Properties;
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
        public void WriteMultipleFl()
        {
            var vssd = DICOMForge.VirtualSourceAxisDistances();
            vssd.Data_ = new System.Collections.Generic.List<float>() { 2538.4199f, 2541.00f };
            byte[] elBytes = null;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    DICOMElementWriter.Write(dw, DICOMWriteSettings.DefaultExplicit(), vssd);
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

        [TestMethod]
        public void WriteBigEndian()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var els = dcm.Elements.Count;
            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(ms))
                {
                    var settings = new DICOMWriteSettings() { TransferSyntax = Enums.TransferSyntax.EXPLICIT_VR_BIG_ENDIAN, DoWriteIndefiniteSequences = false };
                    DICOMObjectWriter.Write(dw, settings, dcm);
                }
                bytes = ms.ToArray();
            }

            var dcm2 = DICOMFileReader.Read(bytes);
            Assert.AreEqual(dcm2.Elements.Count, els);
        }
    }
}
