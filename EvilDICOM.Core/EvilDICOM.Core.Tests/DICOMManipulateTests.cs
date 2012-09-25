using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Tests.Properties;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DICOMManipulateTests
    {
        [TestMethod]
        public void ReplaceElementTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var refName = new PersonName{
                FirstName = "Fred",
                LastName = "Flinstone",
                Tag = TagHelper.REFERRING_PHYSICIAN_NAME
            };
            dcm.Replace(refName);
            Assert.AreEqual(refName.Data, new PersonName
            {
                FirstName = "Fred",
                LastName = "Flinstone"
            }
            .Data);
        }

        [TestMethod]
        public void RemoveElementTest()
        {
        }
    }
}
