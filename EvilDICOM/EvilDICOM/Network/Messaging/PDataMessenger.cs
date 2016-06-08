using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;

namespace EvilDICOM.Network.Messaging
{
    /// <summary>
    /// Class PDataMessenger.
    /// </summary>
    public class PDataMessenger
    {
        /// <summary>
        /// Sends the specified dimse.
        /// </summary>
        /// <param name="dimse">The dimse.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="pContext">The p context.</param>
        public static void Send(AbstractDIMSEBase dimse, Association asc, PresentationContext pContext = null)
        {
            if (asc.State != NetworkState.TRANSPORT_CONNECTION_OPEN)
            {
                asc.OutboundMessages.Enqueue(dimse);
                AssociationMessenger.SendRequest(asc, dimse.AffectedSOPClassUID);
            }
            else
            {
                asc.Logger.Log("--> DIMSE" + dimse.GetLogString());
                dimse.LogData(asc);
                var stream = asc.Stream;
                pContext = pContext ?? asc.PresentationContexts.First(a => a.AbstractSyntax == dimse.AffectedSOPClassUID);
                List<PDataTF> pds = GetPDataTFs(dimse, pContext, asc.UserInfo.MaxPDULength);
                if (pds.Count > 0 && stream.CanWrite)
                {
                    foreach (PDataTF pd in pds)
                    {
                        byte[] message = pd.Write();
                        stream.Write(message, 0, message.Length);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the p data t fs.
        /// </summary>
        /// <param name="dimse">The dimse.</param>
        /// <param name="pContext">The p context.</param>
        /// <param name="maxPDULength">Maximum length of the pdu.</param>
        /// <returns>List&lt;PDataTF&gt;.</returns>
        public static List<PDataTF> GetPDataTFs(AbstractDIMSEBase dimse, PresentationContext pContext, int maxPDULength = 16384)
        {
            var list = new List<PDataTF>();
            var commandEls = dimse.Elements;
            list.Add(new PDataTF(new DICOMObject(dimse.Elements), true, true, pContext));

            var dataDIMSE = dimse as AbstractDIMSE;
            if (dataDIMSE != null && dataDIMSE.Data != null)
            {
                List<byte[]> chunks = GetChunks(dataDIMSE.Data, maxPDULength, pContext);
                chunks
                    .Select( (c, i) => new PDataTF(c, i == chunks.Count - 1, false, pContext))
                    .ToList()
                    .ForEach(list.Add);
            }
            return list;
        }


        /// <summary>
        /// Splits the DICOM object into chunks that are within the max PDU size
        /// </summary>
        /// <param name="dicomObject">the DICOM objec to be split</param>
        /// <param name="maxPduSize">the max length (in bytes) for a PDU</param>
        /// <param name="pc">The pc.</param>
        /// <returns>List&lt;System.Byte[]&gt;.</returns>
        private static List<byte[]> GetChunks(DICOMObject dicomObject, int maxPduSize, PresentationContext pc)
        {
            byte[] dicomBytes;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    var tx = TransferSyntaxHelper.GetSyntax(pc.TransferSyntaxes.First());
                    DICOMObjectWriter.Write(dw,
                        new DICOMWriteSettings
                        {
                            TransferSyntax =tx,
                            DoWriteIndefiniteSequences = false
                        }, dicomObject);
                    dicomBytes = stream.ToArray();
                }
            }

            var split = new List<byte[]>();
            int i = 0;
            while (i < dicomBytes.Length)
            {
                int toTake = dicomBytes.Length >= (maxPduSize - 6) + i ? maxPduSize - 6 : dicomBytes.Length - i;
                byte[] fragment = dicomBytes.Skip(i).Take(toTake).ToArray();
                i += fragment.Length;
                split.Add(fragment);
            }
            return split;
        }
    }
}