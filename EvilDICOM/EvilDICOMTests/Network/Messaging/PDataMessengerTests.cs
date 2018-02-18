using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvilDICOM.Network.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EvilDICOM.Network.Helpers;
using EvilDICOM.Network.PDUs.Items;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Processors;
using EvilDICOM.Network.Readers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core;
using EvilDICOMTests.Helpers;

namespace EvilDICOM.Network.Messaging.Tests
{
    [TestClass()]
    public class PDataMessengerTests
    {
        [TestMethod()]
        public void ReadWriteDIMSETest()
        {
            using (var stream = new MemoryStream())
            {
                //Generate a DIMSE
                var dimse = CFind.CreateStudyQuery("123456");
                var pContext = new PresentationContext()
                {
                    AbstractSyntax = AbstractSyntax.STUDY_FIND,
                    TransferSyntaxes = new List<string>() { TransferSyntaxHelper.IMPLICIT_VR_LITTLE_ENDIAN }
                };

                PDataMessenger.WriteDimseToStream(dimse, stream, pContext);

                //Wrap stream in buffer to get to network stream
                using (var bs = new BufferedStream(stream))
                {
                    bs.Position = 0;
                    var net = new NetworkBinaryReader(bs);

                    var pdata = PDataProcessor.ReadPDataTFs(net);
                    var dcm = PDataProcessor.GetCommandObject(pdata);
                    AbstractDIMSE dimseRead;
                    var success = DIMSEReader.TryReadDIMSE(dcm, out dimseRead);

                    Assert.AreEqual(dimse.Elements.Count, dimseRead.Elements.Count);

                    for (int i = 0; i < dimse.Elements.Count; i++)
                    {
                        var el1 = dimse.Elements[i];
                        var el2 = dimseRead.Elements[i];
                        Assert.AreEqual(el1.Tag, el2.Tag);
                        Assert.AreEqual(el1.DData, el2.DData);
                    }

                    //Make sure this DIMSE was written with data
                    Assert.IsTrue(dimse.HasData);

                    //REad the data
                    var dataPds = PDataProcessor.ReadPDataTFs(net);
                    var data = PDataProcessor.GetDataObject(dataPds, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN);

                    DICOMAssert.AreEqual(data, dimse.Data);
                }
            }
        }

        [TestMethod()]
        public void GetChunksTest()
        {
            //Generate a DIMSE
            var dimse = CFind.CreateStudyQuery("123456");
            var pContext = new PresentationContext()
            {
                AbstractSyntax = AbstractSyntax.STUDY_FIND,
                TransferSyntaxes = new List<string>() { TransferSyntaxHelper.IMPLICIT_VR_LITTLE_ENDIAN }
            };

            var bytes = PDataMessenger.GetChunks(dimse.Data, 16534, pContext);
            var bytes1 = bytes[0];
            var dcm = DICOMObject.Read(bytes1);

            DICOMAssert.AreEqual(dcm, dimse.Data);
        }
    }
}