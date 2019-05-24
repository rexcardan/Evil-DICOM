using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network;
using EvilDICOM.Network.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCPTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var scp = new DICOMSCP(Entity.CreateLocal("SCP", 9999));
            ////new ConsoleLogger(scp.Logger);
            //scp.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
            //int i = 1;
            //scp.DIMSEService.CStoreService.CStorePayloadAction = (dcm, asc) =>
            //{
            //    Thread.Sleep(6000);
            //    Assert.AreEqual(dcm.SOPClass, SOPClass.CTImageStorage);
            //    Assert.AreEqual(dcm.PixelStream.Length, 10000);
            //    Assert.AreEqual(dcm.GetSelector().PatientID.Data, "123456");
            //    Console.WriteLine($"Received {i++} files");
            //    return true;
            //};
            //scp.ListenForIncomingAssociations(true);


            var scu = new DICOMSCU(Entity.CreateLocal("SCU", 9998));
            scu.IdleTimeout = 10000;
            ushort msg = 1;
            foreach(var dcm in GenerateDICOMFiles())
            {
                var resp = scu.GetCStorer(scp.ApplicationEntity).SendCStore(dcm, ref msg);
                if(resp == null)
                {
                    Console.WriteLine("SCU Timeout!");
                    break;
                }
                else if(resp.Status == (ushort)Status.FAILURE)
                {
                    Console.WriteLine("No connection");
                    break;
                }
            }
            Console.Read();
        }

        private static IEnumerable<DICOMObject> GenerateDICOMFiles()
        {
            for (int i = 0; i < 100; i++)
            {
                var dcm = new DICOMObject();
                dcm.Add(DICOMForge.SOPInstanceUID(UIDHelper.GenerateUID()));
                dcm.Add(DICOMForge.PatientID("123456"));
                dcm.Add(DICOMForge.SOPClassUID(SOPClassUID.CTImageStorage));
                dcm.Add(DICOMForge.TransferSyntaxUID(TransferSyntaxHelper.IMPLICIT_VR_LITTLE_ENDIAN));
                dcm.Add(DICOMForge.PixelData(new byte[10000]));
                yield return dcm;
            }
        }
    }
}
