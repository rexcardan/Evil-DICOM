#region

using System.IO;
using System.Threading;
using EvilDICOM.Core.IO.Writing;

#endregion

namespace EvilDICOM.Network.Helpers
{
    /// <summary>
    /// A SCP that writes incoming files to a specified location
    /// </summary>
    public class FileWriterSCP : DICOMSCP
    {
        private readonly string _storagePath;
        private CancellationToken _token;

        public FileWriterSCP(Entity ae, string storageLocation)
            : base(ae)
        {
            _token = new CancellationToken();
            _storagePath = storageLocation;
            DIMSEService.CStorePayloadAction = (dcm, asc) =>
            {
                var uid = dcm.GetSelector().SOPInstanceUID.Data;
                var path = Path.Combine(_storagePath, uid + ".dcm");
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    Logger.Log("Writing file {0}...", path);
                    DICOMFileWriter.Write(fs, DICOMWriteSettings.Default(), dcm);
                }
                return true;
            };
        }
    }
}