#region

using System;
using System.Collections.Generic;
using System.IO;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.PDUs;

#endregion

namespace EvilDICOM.Network.Readers
{
    public class DIMSEReader
    {
        public static DICOMObject CreateDICOMObject(byte[] dimse,
            TransferSyntax syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            long bytesRead;
            var dMessage = DICOMObjectReader.ReadObject(dimse,
                syntax, out bytesRead);
            return dMessage;
        }

        /// <summary>
        ///     Takes a list of PData transfer objects and writes them to a byte array for outgoing messaging
        /// </summary>
        /// <param name="data">the PData transfer objects to be sent</param>
        /// <returns>a byte array containing the PData objects</returns>
        public static byte[] MergePDataTFData(List<PDataTF> data)
        {
            var merged = new byte[0];
            using (var stream = new MemoryStream())
            {
                foreach (var d in data)
                {
                    foreach (var item in d.Items)
                        stream.Write(item.Fragment.Data, 0, item.Fragment.Data.Length);

                }
                merged = stream.ToArray();
            }
            return merged;
        }

        public static bool TryReadDIMSE(DICOMObject dcm, out AbstractDIMSE dimse)
        {
            dimse = null;
            try
            {
                var command = dcm.FindFirst(TagHelper.CommandField) as UnsignedShort;
                var commandField = command != null ? (uint?)command.Data : null;
                if (commandField != null)
                {
                    switch (commandField)
                    {
                        case (ushort)CommandField.C_ECHO_RQ:
                            dimse = ReadDIMSERequest<CEchoRequest>(dcm);
                            break;
                        case (ushort)CommandField.C_ECHO_RP:
                            dimse = ReadDIMSEResponse<CEchoResponse>(dcm);
                            break;
                        case (ushort)CommandField.C_STORE_RQ:
                            dimse = ReadDIMSERequest<CStoreRequest>(dcm);
                            break;
                        case (ushort)CommandField.C_STORE_RP:
                            dimse = ReadDIMSEResponse<CStoreResponse>(dcm);
                            break;
                        case (ushort)CommandField.C_FIND_RQ:
                            dimse = ReadDIMSERequest<CFindRequest>(dcm);
                            break;
                        case (ushort)CommandField.C_FIND_RP:
                            dimse = ReadDIMSEResponse<CFindResponse>(dcm);
                            break;
                        case (ushort)CommandField.C_MOVE_RQ:
                            dimse = ReadDIMSERequest<CMoveRequest>(dcm);
                            break;
                        case (ushort)CommandField.C_MOVE_RP:
                            dimse = ReadDIMSEResponse<CMoveResponse>(dcm);
                            break;
                        case (ushort)CommandField.C_GET_RQ:
                            dimse = ReadDIMSERequest<CGetRequest>(dcm);
                            break;
                        case (ushort)CommandField.C_GET_RP:
                            dimse = ReadDIMSEResponse<CGetResponse>(dcm);
                            break;
                        case (ushort)CommandField.N_ACTION_RQ:
                            dimse = ReadDIMSERequest<NActionRequest>(dcm);
                            break;
                        case (ushort)CommandField.N_ACTION_RP:
                            dimse = ReadDIMSEResponse<NActionResponse>(dcm);
                            break;
                        case (ushort)CommandField.N_EVENT_REPORT_RQ:
                            dimse = ReadDIMSERequest<NEventReportRequest>(dcm);
                            break;
                        case (ushort)CommandField.N_EVENT_REPORT_RP:
                            dimse = ReadDIMSEResponse<NEventReportResponse>(dcm);
                            break;
                    }
                    return true;
                }
                else { return false; } // No command field
            }
            catch(Exception e)
            {
                return false;
            }
        }

        //TODO Merge these methods
        private static T ReadDIMSERequest<T>(DICOMObject dcm) where T : AbstractDIMSERequest
        {
            var req = (T)Activator.CreateInstance(typeof(T), dcm);
            return req;
        }

        private static T ReadDIMSEResponse<T>(DICOMObject dcm) where T : AbstractDIMSEResponse
        {
            var req = (T)Activator.CreateInstance(typeof(T), dcm);
            return req;
        }
    }
}