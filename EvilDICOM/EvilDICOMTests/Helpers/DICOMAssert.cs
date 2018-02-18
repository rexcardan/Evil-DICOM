using EvilDICOM.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOMTests.Helpers
{
    public class DICOMAssert
    {
        public static void AreEqual(DICOMObject dcm1, DICOMObject dcm2)
        {
            Assert.AreEqual(dcm1.Elements.Count, dcm2.Elements.Count);

            for (int i = 0; i < dcm1.Elements.Count; i++)
            {
                var el1 = dcm1.Elements[i];
                var el2 = dcm2.Elements[i];
                Assert.AreEqual(el1.Tag, el2.Tag);
                Assert.AreEqual(el1.DData, el2.DData);
            }
        }
    }
}
