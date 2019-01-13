using System;
using EvilDICOM.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvilDICOM.Core.Tests
{
    [TestClass]
    public class SelectionTests
    {
        [TestMethod]
        public void SelectSequence()
        {
            var dcm = DICOMObject.Read(EvilDICOMTests.Properties.Resources.MultpleFL);
            var expected = dcm.FindFirst(TagHelper.IonBeamSequence);
            var sel = dcm.GetSelector();
            var actual = sel.IonBeamSequence_[0]; //ToSequence()
            Assert.AreEqual(expected.DData_.Count, actual.Data_.Count);
        }

        [TestMethod]
        public void SelectFirstSequence()
        {
            var dcm = DICOMObject.Read(EvilDICOMTests.Properties.Resources.MultpleFL);
            var expected = dcm.FindFirst(TagHelper.IonBeamSequence);
            var sel = dcm.GetSelector();
            var actual = sel.IonBeamSequence;
            Assert.AreEqual(expected.DData_.Count, actual.Data_.Count);
        }

        [TestMethod]
        public void AddIonSequence()
        {
            var dcm = DICOMObject.Read(EvilDICOMTests.Properties.Resources.MultpleFL);
            var expected = dcm.FindFirst(TagHelper.IonBeamSequence);
            var sel = dcm.GetSelector();
            var actual = sel.IonBeamSequence;
            var countBefore = sel.IonBeamSequence_.Count;
            dcm.Add(expected);
            var countAfter = sel.IonBeamSequence_.Count;
            Assert.AreNotEqual(countBefore, countAfter);
        }
    }
}
