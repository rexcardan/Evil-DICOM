#region

using System.Collections.Generic;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network.Services;

#endregion

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