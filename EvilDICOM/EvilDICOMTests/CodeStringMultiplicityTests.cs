using EvilDICOM.Core;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.IO.Writing;
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
    public class CodeStringMultiplicityTests
    {
        [TestMethod]
        public void CanAddMultipleStringsTest()
        {
            var dcm = new DICOMObject();
            dcm.ReplaceOrAdd(DICOMForge.ImageType("Type1", "Type2", "Type3"));

            //Memory test
            var expected = 3;
            var data = dcm.GetSelector().ImageType.Data_;
            var actual = data.Count;
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("Type1", data[0]);
            Assert.AreEqual("Type2", data[1]);
            Assert.AreEqual("Type3", data[2]);


            var bytes = new byte[0];
            //Test IO
            using (var ms = new MemoryStream())
            {
                //WRITE
                using (var bw = new DICOMBinaryWriter(ms))
                {
                    DICOMObjectWriter.Write(bw, DICOMWriteSettings.Default(), dcm);
                }
                bytes = ms.ToArray();
            }

            //READ
            var dcmRead = DICOMObject.Read(bytes);
            expected = 3;
            data = dcmRead.GetSelector().ImageType.Data_;
            actual = data.Count;
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("Type1", data[0]);
            Assert.AreEqual("Type2", data[1]);
            Assert.AreEqual("Type3", data[2]);

        }
    }
}
