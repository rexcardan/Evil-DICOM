using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Writing;
using System.IO;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Enums;

namespace EvilDICOMTests
{
    [TestClass]
    public class LengthTests
    {
        [TestMethod]
        public void WriteBigEndianLength1()
        {
            var len = 52000;
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                var dw = new DICOMBinaryWriter(ms);
                LengthWriter.WriteBigEndian(dw, len, 4);
                bytes = ms.ToArray();
            }

            var read = LengthReader.ReadLittleEndian(bytes);
            Assert.AreNotEqual(len, read);

            read = LengthReader.ReadBigEndian(bytes);
            Assert.AreEqual(len, read);
        }
        
        [TestMethod]
        public void WriteBigEndianLength2()
        {
            var len = 2500;
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                var dw = new DICOMBinaryWriter(ms);
                LengthWriter.WriteBigEndian(dw, len, 2);
                bytes = ms.ToArray();
            }

            var read = LengthReader.ReadLittleEndian(bytes);
            Assert.AreNotEqual(len, read);

            read = LengthReader.ReadBigEndian(bytes);
            Assert.AreEqual(len, read);
        }

         [TestMethod]
        public void WriteBigEndianLength3()
        {
            var len = 10;
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                var dw = new DICOMBinaryWriter(ms);
                VR vr = VR.CodeString;
                DICOMIOSettings settings = new DICOMIOSettings() { TransferSyntax = TransferSyntax.EXPLICIT_VR_BIG_ENDIAN, DoWriteIndefiniteSequences = false };
                var data = new byte[10];
                LengthWriter.Write(dw, vr, settings, data != null ? data.Length : 0);
                bytes = ms.ToArray();
            }

            var read = LengthReader.ReadLittleEndian(bytes);
            Assert.AreNotEqual(len, read);

            read = LengthReader.ReadBigEndian(bytes);
            Assert.AreEqual(len, read);
        }

        [TestMethod]
        public void WriteLittleEndian1()
        {
            var len = 52000;
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                var dw = new DICOMBinaryWriter(ms);
                LengthWriter.WriteLittleEndian(dw, len, 4);
                bytes = ms.ToArray();
            }
            var read = LengthReader.ReadLittleEndian(bytes);
            Assert.AreEqual(len, read);
        }

        [TestMethod]
        public void WriteLittleEndian2()
        {
            var len = 2500;
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                var dw = new DICOMBinaryWriter(ms);
                LengthWriter.WriteLittleEndian(dw, len, 2);
                bytes = ms.ToArray();
            }
            var read = LengthReader.ReadLittleEndian(bytes);
            Assert.AreEqual(len, read);
        }
    }
}
