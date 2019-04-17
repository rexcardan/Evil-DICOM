using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var scp = new DICOMSCP(Entity.CreateLocal("SCP", 9999));
            new ConsoleLogger(scp.Logger);
            scp.SupportedAbstractSyntaxes = AbstractSyntax.ALL_RADIOTHERAPY_STORAGE;
            scp.DIMSEService.CStoreService.CStorePayloadAction = (dcm, asc) =>
            {
                return true;
            };
            scp.ListenForIncomingAssociations(true);
            Console.Read();
        }
    }
}
