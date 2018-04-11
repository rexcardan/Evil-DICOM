using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network;
using EvilDICOM.Network.SCUOps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //var path = @"\\hnas1-users\USERS\rcardan\Desktop\DOSE.1.2.246.352.71.7.173326327737.2655264.20171204090341.dcm";
            //var scu = new DICOMSCU(Entity.CreateLocal("TestAE", 11122));
            //var cStorer = new CStorer(scu, Entity.CreateLocal("DVTK_STR_SCP", 104));
            //var dcm = DICOMObject.Read(path);
            //ushort msg = 1;
            //var resp = cStorer.SendCStore(dcm, ref msg);


            var scp = new DICOMSCP(Entity.CreateLocal("DVTK_STR_SCP", 104));
            scp.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
            scp.ListenForIncomingAssociations(true);
            scp.Logger.LogRequested += Logger_LogRequested;
            Console.Read();
        }

        private static void Logger_LogRequested(string toLog, EvilDICOM.Core.Enums.LogPriority priority)
        {
            Console.WriteLine(toLog);
        }
    }
}
