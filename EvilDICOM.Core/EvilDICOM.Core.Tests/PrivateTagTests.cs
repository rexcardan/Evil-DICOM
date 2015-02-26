using System;
using System.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.IO.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class PrivateTagTests
    {
        [TestMethod]
        public void ReadUnknownFP()
        {
            var dcm = new DICOMObject();
            var fs = new FloatingPointSingle(new Tag("30091047"), 25.0f);
            dcm.Add(fs);
            dcm.Write("test.dcm", new EvilDICOM.Core.IO.Writing.DICOMWriteSettings() { TransferSyntax = EvilDICOM.Core.Enums.TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN });


            dcm = DICOMObject.Read("test.dcm");
            var found = dcm.GetUnknownTagAs<FloatingPointSingle>("30091047");
          

            Assert.IsTrue(found.First() is FloatingPointSingle);
        }

        [TestMethod]
        public void ReadUnknownFP2()
        {
            var dcm = new DICOMObject();
            var fs = new FloatingPointSingle(new Tag("30091047"), 25.0f);
            dcm.Add(fs);
            dcm.Write("test.dcm", new EvilDICOM.Core.IO.Writing.DICOMWriteSettings() { TransferSyntax = EvilDICOM.Core.Enums.TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN });

            TagDictionary.AddEntry<FloatingPointSingle>("30091047", "MyCustomType");
            dcm = DICOMObject.Read("test.dcm");
            var found = dcm.FindAll("30091047");
           
            Assert.IsTrue(found.First() is FloatingPointSingle);
        }
    }
}
