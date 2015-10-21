using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core;

namespace EvilDICOMTests
{
    [TestClass]
    public class ReadTests
    {
        [TestMethod]
        public void ReadBigEndian()
        {
            var dcm = DICOMObject.Read(Properties.Resources.explicitBigEndian);
            var clss = dcm.SOPClass.ToString();
            var dcmSt = dcm.ToString();
            Console.Write("");
        }
    }
}
