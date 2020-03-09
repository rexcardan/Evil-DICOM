#region

using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.Readers;
using Microsoft.Extensions.Logging;

#endregion

namespace EvilDICOM.Network.Processors
{
    public class PDataProcessor
    {
        public static AbstractDIMSE Process(IMessage message, Association asc)
        {
            var pdata = message.DynPayload as PDataTF;
            var pdatas = new List<PDataTF> {pdata};
            if (!pdata.Items.Any(i => i.Fragment.IsLastItem))
            {
                try
                {
                    pdatas = pdatas.Concat(ReadPDataTFs(asc.Reader)).ToList();
                }
                catch (Exception e)
                {
                    asc.Logger.LogInformation(e.Message);
                    asc.RequestAbort();
                }
            }
            if (pdatas.Any(p => p.Items.Any(i => i.Fragment.IsCommandObject)))
            {
                return ProcessCommand(pdatas, asc);
            }
            asc.Logger.LogInformation("Free DICOM object not assciated with command:");
            int id;
            var txSyntax = GetTransferSyntax(asc, pdatas, out id);
            var data = GetDataObject(pdatas, txSyntax);
            asc.Logger.LogInformation("<-- DICOM OBJ");
            foreach (var el in data.Elements)
                asc.Logger.LogInformation(el.ToString());

            return null;
        }

        public static List<PDataTF> ReadPDataTFs(NetworkBinaryReader dr)
        {
            var pDatas = new List<PDataTF>();
            do
            {
                try
                {
                    PDataTF pdu;
                    pdu = PDUReader.ReadPDFData(dr).Payload;
                    pDatas.Add(pdu);
                }
                catch (Exception e)
                {
                    throw new Exception("Problem reading PDataTF chain.\n" + e.Message);
                }
            } while (!pDatas.Any(p => p.Items.Any(it => it.Fragment.IsLastItem)));
            return pDatas;
        }

        private static AbstractDIMSE ProcessCommand(List<PDataTF> pDatas, Association asc)
        {
            var dcm = GetCommandObject(pDatas);
            AbstractDIMSE dimse;
            var success = DIMSEReader.TryReadDIMSE(dcm, out dimse);
            if (!success)
                asc.Logger.LogInformation("DIMSE could not be read!");
            if (dimse.HasData)
            {
                var dr = asc.Reader;
                var dataPds = ReadPDataTFs(dr);
                int id;
                var txSyntax = GetTransferSyntax(asc, dataPds, out id);
                dimse.DataPresentationContextId = id;
                dimse.Data = GetDataObject(dataPds, txSyntax);
            }
            DIMSEProcessor.Process(dimse, asc);
            return dimse;
        }

        private static TransferSyntax GetTransferSyntax(Association asc, List<PDataTF> dataPds, out int presCtxId)
        {
            var ctx =
                asc.PresentationContexts.FirstOrDefault(
                    p => p.Id == dataPds.First().Items.First().PresentationContextID);
            presCtxId = ctx.Id;
            return ctx != null
                ? TransferSyntaxHelper.GetSyntax(ctx.TransferSyntaxes.First())
                : TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
        }

        public static DICOMObject GetCommandObject(List<PDataTF> pDatas)
        {
            var commandBytes = DIMSEReader.MergePDataTFData(pDatas);
            return DIMSEReader.CreateDICOMObject(commandBytes);
        }

        public static DICOMObject GetDataObject(List<PDataTF> pDatas, TransferSyntax syntax)
        {
            var dicomBytes = DIMSEReader.MergePDataTFData(pDatas);
            return DIMSEReader.CreateDICOMObject(dicomBytes, syntax);
        }
    }
}