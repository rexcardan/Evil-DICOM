using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Anonymization;
using EvilDICOM.Anonymization.Settings;
using EvilDICOM.Core.IO.Writing;
using System.IO;

namespace EvilDICOMTests
{
    [TestClass]
    public class AnonymizationTests
    {
        [TestMethod]
        public void WriteUnknownTest()
        {
            var dcm = new DICOMObject();
            dcm.Add(new LongString(new Tag("32751000"), "Online3D"));

            var dcmBytes = new byte[0]; //Start empty
            using (var ms = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(ms))
                {
                    //Explicity
                    DICOMObjectWriter.Write(dw, DICOMIOSettings.DefaultExplicit(), dcm);
                }
                dcmBytes = ms.ToArray(); //Store to read back
            }

            var dcmBack = DICOMObject.Read(dcmBytes);
            //This passes
            Assert.IsTrue(dcmBack.FindFirst("32751000").VR == EvilDICOM.Core.Enums.VR.LongString);

            using (var ms = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(ms))
                {
                    //Implicit
                    DICOMObjectWriter.Write(dw, DICOMIOSettings.Default(), dcm);
                }
                dcmBytes = ms.ToArray(); //Store to read back
            }

            dcmBack = DICOMObject.Read(dcmBytes);
            //This fails
            Assert.IsFalse(dcmBack.FindFirst("32751000").VR == EvilDICOM.Core.Enums.VR.LongString);
        }
    }
}
