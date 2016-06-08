﻿using System;
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
using EvilDICOM.Network.PDUs.Items;

namespace EvilDICOM.Network.Readers
{
    /// <summary>
    /// Class DIMSEReader.
    /// </summary>
    public class DIMSEReader
    {
        /// <summary>
        /// Creates the dicom object.
        /// </summary>
        /// <param name="dimse">The dimse.</param>
        /// <param name="syntax">The syntax.</param>
        /// <returns>DICOMObject.</returns>
        public static DICOMObject CreateDICOMObject(byte[] dimse,
            TransferSyntax syntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
        {
            long bytesRead;
            DICOMObject dMessage = DICOMObjectReader.ReadObject(dimse,
                syntax, out bytesRead);
            return dMessage;
        }

        /// <summary>
        /// Takes a list of PData transfer objects and writes them to a byte array for outgoing messaging
        /// </summary>
        /// <param name="data">the PData transfer objects to be sent</param>
        /// <returns>a byte array containing the PData objects</returns>
        public static byte[] MergePDataTFData(List<PDataTF> data)
        {
            var merged = new byte[0];
            using (var stream = new MemoryStream())
            {
                foreach (PDataTF d in data)
                {
                    foreach (PDVItem item in d.Items)
                    {
                        stream.Write(item.Fragment.Data, 0, item.Fragment.Data.Length);
                    }
                }
                merged = stream.ToArray();
            }
            return merged;
        }

        /// <summary>
        /// Tries the read dimse.
        /// </summary>
        /// <param name="dcm">The DCM.</param>
        /// <param name="dimse">The dimse.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool TryReadDIMSE(DICOMObject dcm, out AbstractDIMSE dimse)
        {
            dimse = null;
            var command = (dcm.FindFirst(TagHelper.COMMAND_FIELD) as UnsignedShort);
            uint? commandField = command != null ? (uint?) command.Data : null;
            if (commandField != null)
            {
                switch (commandField)
                {
                    case (ushort) CommandField.C_ECHO_RQ:
                        dimse = ReadDIMSERequest<CEchoRequest>(dcm);
                        break;
                    case (ushort) CommandField.C_ECHO_RP:
                        dimse = ReadDIMSEResponse<CEchoResponse>(dcm);
                        break;
                    case (ushort) CommandField.C_STORE_RQ:
                        dimse = ReadDIMSERequest<CStoreRequest>(dcm);
                        break;
                    case (ushort) CommandField.C_STORE_RP:
                        dimse = ReadDIMSEResponse<CStoreResponse>(dcm);
                        break;
                    case (ushort) CommandField.C_FIND_RQ:
                        dimse = ReadDIMSERequest<CFindRequest>(dcm);
                        break;
                    case (ushort) CommandField.C_FIND_RP:
                        dimse = ReadDIMSEResponse<CFindResponse>(dcm);
                        break;
                    case (ushort) CommandField.C_MOVE_RQ:
                        dimse = ReadDIMSERequest<CMoveRequest>(dcm);
                        break;
                    case (ushort) CommandField.C_MOVE_RP:
                        dimse = ReadDIMSEResponse<CMoveResponse>(dcm);
                        break;
                }
                return true;
            }
            return false;
        }

        //TODO Merge these methods
        /// <summary>
        /// Reads the dimse request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dcm">The DCM.</param>
        /// <returns>T.</returns>
        private static T ReadDIMSERequest<T>(DICOMObject dcm) where T : AbstractDIMSERequest
        {
            var req = (T) Activator.CreateInstance(typeof (T), dcm);
            return req;
        }

        /// <summary>
        /// Reads the dimse response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dcm">The DCM.</param>
        /// <returns>T.</returns>
        private static T ReadDIMSEResponse<T>(DICOMObject dcm) where T : AbstractDIMSEResponse
        {
            var req = (T) Activator.CreateInstance(typeof (T), dcm);
            return req;
        }
    }
}