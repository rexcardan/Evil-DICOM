using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.Element;
using System.Linq;
using System.IO;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.IO.Reading;

namespace EvilDICOMTests
{
    [TestClass]
    public class ElementWriterTests
    {
        [TestMethod]
        public void WriteDecimalString()
        {
            var ds = new DecimalString();
            ds.DData_ = Enumerable.Range(1, 15000).Select(i => ((double)i) + 0.005).ToList();
            ds.Tag = new Tag("00082130");
            byte[] written;
            var settings = DICOMIOSettings.Default();

            using (var ms = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(ms))
                {

                    DICOMElementWriter.Write(dw, DICOMIOSettings.Default(), ds);
                }
                written = ms.ToArray();
            }

            using (var dr = new DICOMBinaryReader(written))
            {
                var read = DICOMElementReader.ReadElementImplicitLittleEndian(dr) as DecimalString;
                CollectionAssert.AreEqual(ds.DData_, read.Data_);
            }


        }
    }
}
