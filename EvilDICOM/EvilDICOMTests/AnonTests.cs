using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOMTests.Properties;
using EvilDICOM.Anonymization.Anonymizers;
using EvilDICOM.Core;
using EvilDICOM.Anonymization.Helpers;
using EvilDICOM.Core.Selection;

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
            sel.PatientID = DICOMForge.PatientID;
            sel.PatientID.Data = "12345";
            sel.PatientName.LastName = "Flinstone";
            sel.PatientName.FirstName = "Fred";

            var anon = new PatientIdAnonymizer("Homer", "Simpson", "678910");
            anon.Anonymize(dcm);

            Assert.AreEqual(sel.PatientID.Data, "678910");
            Assert.AreEqual(sel.PatientName.LastName, "Simpson");
            Assert.AreEqual(sel.PatientName.FirstName, "Homer");
        }

        [TestMethod]
        public void LatestDateTest()
        {   
            var expected = DateTime.Today;

            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var sel = new DICOMSelector(dcm);
            sel.ContentDate = DICOMForge.ContentDate;
            sel.ContentDate.Data = expected;
            sel.PatientBirthDate = DICOMForge.PatientBirthDate;
            sel.PatientBirthDate.Data = expected.AddYears(-30);
            var latestDate = DateHelper.GetLatestDate(dcm);
            Assert.AreEqual(expected, latestDate);
        }

        /// <summary>
        /// Test to see if a patient is over 89 to make sure the anonymizer forces the patient to be 89 or less
        /// </summary>
        [TestMethod]
        public void PreserveAge89Test()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var sel = new DICOMSelector(dcm);
            sel.ContentDate = DICOMForge.ContentDate;
            sel.ContentDate.Data = DateTime.Today;
            sel.PatientBirthDate = DICOMForge.PatientBirthDate;
            sel.PatientBirthDate.Data = DateTime.Today.AddYears(-90);

            Assert.IsFalse(DateHelper.YoungerThan89(dcm));
            //You should not be able to preserve the age of someone over 89
            var anon = new DateAnonymizer(EvilDICOM.Anonymization.Settings.DateSettings.PRESERVE_AGE);
            anon.Anonymize(dcm);

            Assert.IsTrue(DateHelper.YoungerThan89(dcm));
        }

        [TestMethod]
        public void DateAnonymizeTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var sel = dcm.GetSelector();
            sel.ContentDate = DICOMForge.ContentDate;
            sel.ContentDate.Data = DateTime.Today;
            sel.PatientBirthDate = DICOMForge.PatientBirthDate;
            sel.PatientBirthDate.Data = DateTime.Today.AddYears(-50);
            var anon = new DateAnonymizer(dateSettings: EvilDICOM.Anonymization.Settings.DateSettings.NULL_AGE_ANON);
            anon.Anonymize(dcm);


            Assert.AreNotEqual(DateTime.Today.Date, sel.ContentDate.Data.Value.Date);

            //Assert.AreEqual(sel.PatientID.Data, "678910");
            //Assert.AreEqual(sel.PatientName.LastName, "Simpson");
            //Assert.AreEqual(sel.PatientName.FirstName, "Homer");
        }
    }
}
