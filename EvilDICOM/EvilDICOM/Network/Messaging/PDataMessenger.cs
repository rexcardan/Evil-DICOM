#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Extensions;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;

#endregion

namespace EvilDICOM.Network.Messaging
{
    public class PDataMessenger
    {
        public static void Send(AbstractDIMSEBase dimse, Association asc, PresentationContext pContext = null)
        {
            if (asc.State != NetworkState.TRANSPORT_CONNECTION_OPEN)
            {
                asc.OutboundMessages.Enqueue(dimse);
                AssociationMessenger.SendRequest(asc, dimse.AffectedSOPClassUID);
            }
            else if (!asc.IsClientConnected)
            {
                //Connection lost
                asc.Logger.Log("TCP connection has been lost. Ending association");     
            }
            else
            {
                asc.Logger.Log("--> DIMSE" + dimse.GetLogString());
                dimse.LogData(asc);
                pContext = pContext ?? asc.PresentationContexts.First(a => a.AbstractSyntax == dimse.AffectedSOPClassUID);
                var maxPDU = asc.UserInfo.MaxPDULength;
                WriteDimseToStream(dimse, asc.Stream, pContext, maxPDU);
                if(dimse is AbstractDIMSEResponse && !(dimse is NEventReportResponse))
                {
                    asc.State = NetworkState.AWAITING_RELEASE;
                }
                else if(dimse is NEventReportResponse)
                {
                    AssociationMessenger.SendReleaseRequest(asc);
                }
                else
                {
                    asc.State = NetworkState.ASSOCIATION_ESTABLISHED_WAITING_ON_DATA;
                }
            }
        }

        public static void WriteDimseToStream(AbstractDIMSEBase dimse, Stream stream, PresentationContext pContext, int maxPDULength = 16384)
        {
            var pds = GetPDataTFs(dimse, pContext, maxPDULength);
            if (pds.Count > 0 && stream.CanWrite)
            {
                foreach (var pd in pds)
                {
                    var message = pd.Write();
                    stream.Write(message, 0, message.Length);
                }
            }
        }

        public static List<PDataTF> GetPDataTFs(AbstractDIMSEBase dimse, PresentationContext pContext,
            int maxPDULength = 16384)
        {
            var list = new List<PDataTF>();
            var commandEls = dimse.Elements;
            list.Add(new PDataTF(new DICOMObject(dimse.Elements), true, true, pContext));

            var dataDIMSE = dimse as AbstractDIMSE;
            if (dataDIMSE != null && dataDIMSE.Data != null)
            {
                var chunks = GetChunks(dataDIMSE.Data, maxPDULength, pContext);
                chunks
                    .Select((c, i) => new PDataTF(c, i == chunks.Count - 1, false, pContext))
                    .ToList()
                    .ForEach(list.Add);
            }
            return list;
        }


        /// <summary>
        ///     Splits the DICOM object into chunks that are within the max PDU size
        /// </summary>
        /// <param name="dicomObject"> the DICOM objec to be split</param>
        /// <param name="maxPduSize">the max length (in bytes) for a PDU</param>
        /// <param name="asc">the association that the file will be sent</param>
        /// <returns></returns>
        public static List<byte[]> GetChunks(DICOMObject dicomObject, int maxPduSize, PresentationContext pc)
        {
            byte[] dicomBytes;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    var tx = TransferSyntaxHelper.GetSyntax(pc.TransferSyntaxes.First());
                    DICOMObjectWriter.WriteSameSyntax(dw,
                        new DICOMIOSettings
                        {
                            TransferSyntax = tx,
                            DoWriteIndefiniteSequences = false
                        }, dicomObject);
                    dicomBytes = stream.ToArray();
                }
            }

            var split = new List<byte[]>();
            var i = 0;
            while (i < dicomBytes.Length)
            {
                var toTake = dicomBytes.Length >= maxPduSize - 6 ? maxPduSize - 6 : dicomBytes.Length;
                var fragment = dicomBytes.Skip(i).Take(toTake).ToArray();
                i += fragment.Length;
                split.Add(fragment);
            }
            return split;
        }
    }
}