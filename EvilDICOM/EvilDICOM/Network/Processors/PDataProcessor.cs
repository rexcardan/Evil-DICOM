using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;
using EvilDICOM.Network.Readers;

namespace EvilDICOM.Network.Processors
{
    /// <summary>
    /// Class PDataProcessor.
    /// </summary>
    public class PDataProcessor
    {
        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="asc">The asc.</param>
        /// <returns>AbstractDIMSE.</returns>
        public static AbstractDIMSE Process(IMessage message, Association asc)
        {
            var pdata = message.DynPayload as PDataTF;
            var pdatas = new List<PDataTF> { pdata };
            if (!pdata.Items.Any(i => i.Fragment.IsLastItem))
            {
                //Bigger than one container
                try
                {
                    pdatas = pdatas.Concat(ReadPDataTFs(asc.Reader)).ToList();
                }
                catch (Exception e)
                {
                    asc.Logger.Log(e.Message);
                    asc.RequestAbort();
                }
            }
            if (pdatas.Any(p => p.Items.Any(i => i.Fragment.IsCommandObject)))
            {
                return ProcessCommand(pdatas, asc);
            }
            else
            {
                asc.Logger.Log("Free DICOM object not assciated with command:");
                int id;
                var txSyntax = GetTransferSyntax(asc, pdatas, out id);
                var data = GetDataObject(pdatas, txSyntax);
                asc.Logger.Log("<-- DICOM OBJ" );
                foreach (var el in data.Elements)
                {
                    asc.Logger.Log(el);
                }
                
                return null;
            }
        }

        /// <summary>
        /// Reads the p data t fs.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>List&lt;PDataTF&gt;.</returns>
        /// <exception cref="System.Exception">Problem reading PDataTF chain.\n + e.Message</exception>
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

        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="pDatas">The p datas.</param>
        /// <param name="asc">The asc.</param>
        /// <returns>AbstractDIMSE.</returns>
        private static AbstractDIMSE ProcessCommand(List<PDataTF> pDatas, Association asc)
        {
            DICOMObject dcm = GetCommandObject(pDatas);
            AbstractDIMSE dimse;
            bool success = DIMSEReader.TryReadDIMSE(dcm, out dimse);
            if (!success)
            {
                asc.Logger.Log("DIMSE could not be read!");
            }
            if (dimse.HasData)
            {
                NetworkBinaryReader dr = asc.Reader;
                List<PDataTF> dataPds = ReadPDataTFs(dr);
                int id;
                var txSyntax = GetTransferSyntax(asc, dataPds, out id);
                dimse.DataPresentationContextId = id;
                dimse.Data = GetDataObject(dataPds, txSyntax);
            }
            DIMSEProcessor.Process(dimse, asc);
            return dimse;
        }

        /// <summary>
        /// Gets the transfer syntax.
        /// </summary>
        /// <param name="asc">The asc.</param>
        /// <param name="dataPds">The data PDS.</param>
        /// <param name="presCtxId">The pres CTX identifier.</param>
        /// <returns>TransferSyntax.</returns>
        private static TransferSyntax GetTransferSyntax(Association asc, List<PDataTF> dataPds, out int presCtxId)
        {
            var ctx =
                asc.PresentationContexts.FirstOrDefault(p => p.Id == dataPds.First().Items.First().PresentationContextID);
            presCtxId = ctx.Id;
            return ctx != null ? TransferSyntaxHelper.GetSyntax(ctx.TransferSyntaxes.First()) : TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN;
        }

        /// <summary>
        /// Gets the command object.
        /// </summary>
        /// <param name="pDatas">The p datas.</param>
        /// <returns>DICOMObject.</returns>
        public static DICOMObject GetCommandObject(List<PDataTF> pDatas)
        {
            byte[] commandBytes = DIMSEReader.MergePDataTFData(pDatas);
            return DIMSEReader.CreateDICOMObject(commandBytes);
        }

        /// <summary>
        /// Gets the data object.
        /// </summary>
        /// <param name="pDatas">The p datas.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
        public static DICOMObject GetDataObject(List<PDataTF> pDatas, TransferSyntax syntax)
        {
            byte[] dicomBytes = DIMSEReader.MergePDataTFData(pDatas);
            return DIMSEReader.CreateDICOMObject(dicomBytes, syntax);
        }
    }
}