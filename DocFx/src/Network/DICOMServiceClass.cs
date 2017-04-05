using System.Collections.Generic;
using System.Net.Sockets;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.Services;
using EvilDICOM.Core.Logging;

namespace EvilDICOM.Network
{
    public abstract class DICOMServiceClass
    {
        public DICOMServiceClass(Entity ae)
        {
            ApplicationEntity = ae;
            SupportedTransferSyntaxes = new List<string>
            {
                TransferSyntaxHelper.IMPLICIT_VR_LITTLE_ENDIAN,
                TransferSyntaxHelper.EXPLICIT_VR_LITTLE_ENDIAN
            };
            SupportedAbstractSyntaxes = new List<string>();
            DIMSEService = new DIMSEService();
            AssociationService = new AssociationService();
            Logger = new EventLogger();
        }

        public AssociationService AssociationService { get; set; }
        public DIMSEService DIMSEService { get; set; }
        public Entity ApplicationEntity { get; set; }
        public List<string> SupportedTransferSyntaxes { get; set; }
        public List<string> SupportedAbstractSyntaxes { get; set; }
        public EventLogger Logger { get; set; }
    }
}