using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network;
using EvilDICOM.Network.SCUOps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"\\hnas1-users\USERS\rcardan\Desktop\ToSend\CT.1.2.246.352.71.3.173326327737.18364155.20171204090851.dcm";
            var scu = new DICOMSCU(Entity.CreateLocal("DICOMAnonSCU", 11122));
            var cStorer = new CStorer(scu, Entity.CreateLocal("DVTK_STR_SCP", 104));
            var dcm = DICOMObject.Read(path);
            ushort msg = 1;
            scu.Logger.LogRequested += Logger_LogRequested;
            var resp = cStorer.SendCStore(dcm, ref msg);

            Thread.Sleep(2000);
            scu = new DICOMSCU(Entity.CreateLocal("DVTK_STRC_SCU", 115));
            var verifier = new StorageVerifier(scu, Entity.CreateLocal("DVTK_STRC_SCP", 105));
            var results = verifier.VerifyStorage(new Dictionary<string, string>()
            {
                { dcm.GetSelector().SOPClassUID.Data, dcm.GetSelector().SOPInstanceUID.Data }
            }, 10000);
            Console.Read();

            //var scp = new DICOMSCP(Entity.CreateLocal("DVTK_STR_SCP", 104));
            //scp.DIMSEService.CStoreService.CStorePayloadAction = new Func<DICOMObject, Association, bool>((d, asc) =>
            //{
            //    return true;
            //});
            //scp.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
            //scp.SupportedAbstractSyntaxes.Add(AbstractSyntax.StorageCommitment_Push);
            //scp.ListenForIncomingAssociations(true);
            //scp.Logger.LogRequested += Logger_LogRequested;
            //Console.Read();
        }

        private static void Logger_LogRequested(string toLog, EvilDICOM.Core.Enums.LogPriority priority)
        {
            Console.WriteLine(toLog);
        }
    }
}
