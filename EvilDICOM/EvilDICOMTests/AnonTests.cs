using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOMTests.Properties;
using EvilDICOM.Anonymization.Anonymizers;
using EvilDICOM.Core;

namespace EvilDICOMTests
{
    [TestClass]
    public class AnonTests
    {
        [TestMethod]
        public void PatientIdReplaceTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var sel = dcm.GetSelector();
            sel.PatientID = DICOMForge.PatientID();
            sel.PatientID.Data = "12345";
            sel.PatientName.LastName = "Flinstone";
            sel.PatientName.FirstName = "Fred";

            var anon = new PatientIdAnonymizer("Homer", "Simpson", "678910");
            anon.Anonymize(dcm);

            Assert.AreEqual(sel.PatientID.Data, "678910");
            Assert.AreEqual(sel.PatientName.LastName, "Simpson");
            Assert.AreEqual(sel.PatientName.FirstName, "Homer");
        }
    }
}
