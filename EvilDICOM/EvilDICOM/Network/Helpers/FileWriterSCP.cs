using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Helpers
{
    /// <summary>
    /// A SCP that writes incoming files to a specified location
    /// </summary>
    public class FileWriterSCP : DICOMSCP
    {
        private string _storagePath;
        private CancellationToken _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriterSCP"/> class.
        /// </summary>
        /// <param name="ae">The ae.</param>
        /// <param name="storageLocation">The storage location.</param>
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
