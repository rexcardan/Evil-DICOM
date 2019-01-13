using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;
using EvilDICOMTests.Properties;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class DICOMManipulateTests
    {
        [TestMethod]
        public void ReplaceElementTest()
        {
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            var directChildren = dcm.Elements;
            var allDescendants = dcm.AllElements;

            //Finds the first instance of the Group Length element (0002,0000)
            var firstInstance = dcm.FindFirst("00020000");

            //Finds all instances of the Group Length element (0002,0000)
            var allInstances = dcm.FindAll("00020000");

            //Finds all Code Value (0008,0100) elements that are children of Procedure Code Sequence Elements (0008,1032)
            var specificTree = dcm.FindAll(new Tag[]{ TagHelper.ProcedureCodeSequence, TagHelper.CodeValue });

            //Finds all elements that are of VR type PersonName
            var allPersonsNameElements = dcm.FindAll(Enums.VR.PersonName);

            //Whatever the referring physicians real name was, it is now Fred Flinstone     
            var refName = new PersonName{
                FirstName = "Fred",
                LastName = "Flinstone",
                Tag = TagHelper.ReferringPhysicianName
            };
            dcm.Replace(refName);

            //Even if it doesn't exist yet, add it
            dcm.ReplaceOrAdd(refName);

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
            var dcm = DICOMFileReader.Read(Resources.explicitLittleEndian);
            //Remove elements by tag
            dcm.Remove("00020000");
            dcm.Remove(TagHelper.SegmentNumber);
        }
    }
}
