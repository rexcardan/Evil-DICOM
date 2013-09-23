using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.IO.Writing;

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
    }
}
