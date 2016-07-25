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
    public class PDataMessenger
    {
        public static void Send(AbstractDIMSEBase dimse, Association asc, PresentationContext pContext = null)
        {
            dimse.SetGroupLength();
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
                List<PDataTF> pds = GetPDataTFs(dimse, asc, pContext);
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

        public static List<PDataTF> GetPDataTFs(AbstractDIMSEBase dimse, Association asc, PresentationContext pContext = null)
        {
            pContext = pContext ?? asc.PresentationContexts.First(a => a.AbstractSyntax == dimse.AffectedSOPClassUID);
            var list = new List<PDataTF>();
            var commandEls = dimse.Elements;

            list.Add(new PDataTF(new DICOMObject(dimse.Elements), true, true, pContext));
            
            var dataDIMSE = dimse as AbstractDIMSE;
            if (dataDIMSE != null && dataDIMSE.Data != null)
            {
                List<byte[]> chunks = GetChunks(dataDIMSE.Data, asc.UserInfo.MaxPDULength, asc);
                chunks
                    .Select(
                        (c, i) =>
                            new PDataTF(c, i == chunks.Count - 1, false,
                                asc.PresentationContexts.First(a => a.AbstractSyntax == dimse.AffectedSOPClassUID)))
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
        private static List<byte[]> GetChunks(DICOMObject dicomObject, int maxPduSize, Association asc)
        {
            byte[] dicomBytes;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    DICOMObjectWriter.Write(dw,
                        new DICOMWriteSettings
                        {
                            TransferSyntax =
                                TransferSyntaxHelper.GetSyntax(asc.PresentationContexts.First().TransferSyntaxes.First()),
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