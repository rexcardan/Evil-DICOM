using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.Processors;
using EvilDICOM.Network.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOMTests
{
    /// <summary>
    /// Tests to perform a CStore operation against the DICOM TK library DICOMSCP
    /// </summary>
    [TestClass]
    public class CStoreSCUTest
    {
        //[TestMethod]
        //public void PerformCStore()
        //{
        //    //Launch SCP
        //    var entity = Entity.CreateLocal("EvilDICOM", 110);
        //    var scp = Entity.CreateLocal("STORESCP", 105);
            
        //    var scu = new DICOMSCU(entity);
        //    scu.SupportedTransferSyntaxes = new List<string>() { TransferSyntaxHelper.EXPLICIT_VR_LITTLE_ENDIAN };
        //    var d1 = Properties.Resources.explicitLittleEndian;
        //    var dcm = DICOMObject.Read(d1);
        //    var cstore = scu.GenerateCStoreRequest(dcm);
        //    var sb = new StringBuilder();
        //    scu.Logger.LogRequested += (toLog, prior) => { sb.AppendLine(toLog); };
        //    scu.SendMessage(cstore, scp);
        //    var resp = sb.ToString();
        //    var test = "";
        //}

        //[TestMethod]
        //public void PerformCStore2()
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        using (var bs = new BufferedStream(ms))
        //        {
        //            var ns = new NetworkBinaryReader(bs);
        //            var entity = Entity.CreateLocal("EvilDICOM", 110);
        //            var scp = Entity.CreateLocal("STORESCP", 105);
        //            var scu = new DICOMSCU(entity);

        //            var d1 = Properties.Resources.explicitLittleEndian;
        //            var dcm = DICOMObject.Read(d1);
        //            var cstore = scu.GenerateCStoreRequest(dcm);
        //            var pc = new EvilDICOM.Network.PDUs.Items.PresentationContext()
        //            { TransferSyntaxes = new List<string>() { TransferSyntaxHelper.IMPLICIT_VR_LITTLE_ENDIAN } };
        //            var pdas = PDataMessenger.GetPDataTFs(cstore, pc);

        //            foreach (var pd in pdas)
        //            {
        //                var msg = pd.Write();
        //                ms.Write(msg, 0, msg.Length);
        //            }
        //            bs.Position = 0;

        //            var pdatas = PDataProcessor.ReadPDataTFs(ns);
        //            var dicom = PDataProcessor.GetDataObject(pdatas, TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN);
        //            var test = "";
        //        }
        //    }
        //}


    }
}
