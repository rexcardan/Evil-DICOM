using System.Collections;
using System.IO;
using System.Linq;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class ItemWriter.
    /// </summary>
    public class ItemWriter
    {
        /// <summary>
        /// Writes the uid item.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="iType">Type of the i.</param>
        /// <param name="uid">The uid.</param>
        private static void WriteUIDItem(DICOMBinaryWriter dw, ItemType iType, string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                dw.Write((byte) iType);
                dw.WriteNullBytes(1); // Reserved Null Byte
                LengthWriter.WriteBigEndian(dw, uid.Length, 2);
                dw.Write(uid);
            }
        }

        /// <summary>
        /// Writes the abstract syntax.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="uid">The uid.</param>
        public static void WriteAbstractSyntax(DICOMBinaryWriter dw, string uid)
        {
            WriteUIDItem(dw, ItemType.ABSTRACT_SYNTAX, uid);
        }

        /// <summary>
        /// Writes the application context.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="uid">The uid.</param>
        public static void WriteApplicationContext(DICOMBinaryWriter dw, string uid)
        {
            WriteUIDItem(dw, ItemType.APPLICATION_CONTEXT, uid);
        }

        /// <summary>
        /// Writes the asynchronous operations.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="ao">The ao.</param>
        public static void WriteAsyncOperations(DICOMBinaryWriter dw, AsyncOperations ao)
        {
            if (ao != null)
            {
                dw.Write((byte) ItemType.ASYNCHRONOUS_OPERATIONS_WINDOW);
                dw.WriteNullBytes(1); // Reserved Null Byte
                LengthWriter.WriteBigEndian(dw, 4, 2);
                LengthWriter.WriteBigEndian(dw, ao.MaxInvokeOperations, 2);
                LengthWriter.WriteBigEndian(dw, ao.MaxPerformOperations, 2);
            }
        }

        /// <summary>
        /// Writes the implementation class uid.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="uid">The uid.</param>
        public static void WriteImplementationClassUID(DICOMBinaryWriter dw, string uid)
        {
            WriteUIDItem(dw, ItemType.IMPLEMENTATION_CLASS_UID, uid);
        }

        /// <summary>
        /// Writes the implementation version.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="uid">The uid.</param>
        public static void WriteImplementationVersion(DICOMBinaryWriter dw, string uid)
        {
            WriteUIDItem(dw, ItemType.IMPLEMENTATION_VERSION_NAME, uid);
        }

        /// <summary>
        /// Writes the maximum length.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="length">The length.</param>
        public static void WriteMaxLength(DICOMBinaryWriter dw, int length)
        {
            dw.Write((byte) ItemType.MAXIMUM_LENGTH);
            dw.WriteNullBytes(1); // Reserved Null Byte
            LengthWriter.WriteBigEndian(dw, 4, 2);
            LengthWriter.WriteBigEndian(dw, length, 4);
        }

        /// <summary>
        /// Writes the PDV item.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="pdv">The PDV.</param>
        public static void WritePDVItem(DICOMBinaryWriter dw, PDVItem pdv)
        {
            //Write fragment first so we have length
            var fragment = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var fragDw = new DICOMBinaryWriter(stream))
                {
                    WritePDVFragment(fragDw, pdv.Fragment);
                    fragment = stream.ToArray();
                }
            }
            LengthWriter.WriteBigEndian(dw, fragment.Length + 1, 4);
            dw.Write((byte) pdv.PresentationContextID);
            dw.Write(fragment);
        }

        /// <summary>
        /// Writes the PDV fragment.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="frag">The frag.</param>
        public static void WritePDVFragment(DICOMBinaryWriter dw, PDVItemFragment frag)
        {
            WritePDVFragmentMessageHeader(dw, frag);
            dw.Write(frag.Data);
        }

        /// <summary>
        /// Writes the PDV fragment message header.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="frag">The frag.</param>
        private static void WritePDVFragmentMessageHeader(DICOMBinaryWriter dw, PDVItemFragment frag)
        {
            var bits = new BitArray(8);
            bits.Set(0, frag.IsCommandObject);
            bits.Set(1, frag.IsLastItem);
            var bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            dw.Write(bytes[0]);
        }

        /// <summary>
        /// Writes the type of the presentation CTX accept.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="pc">The pc.</param>
        public static void WritePresentationCtxAcceptType(DICOMBinaryWriter dw, PresentationContext pc)
        {
            dw.Write((byte) ItemType.PRESENTATION_CONTEXT_ACCEPT);
            dw.WriteNullBytes(1); //Reserved Null Byte
            byte[] internBytes; //Will use to get length
            using (var stream = new MemoryStream())
            {
                using (var intern = new DICOMBinaryWriter(stream))
                {
                    intern.Write((byte) pc.Id);
                    intern.WriteNullBytes(1);
                    intern.Write((byte) pc.Reason);
                    intern.WriteNullBytes(1);
                    WriteTransferSyntax(intern, pc.TransferSyntaxes.First());
                    internBytes = stream.ToArray();
                }
            }
            LengthWriter.WriteBigEndian(dw, internBytes.Length, 2);
            dw.Write(internBytes);
        }

        /// <summary>
        /// Writes the type of the presentation CTX request.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="pc">The pc.</param>
        public static void WritePresentationCtxRequestType(DICOMBinaryWriter dw, PresentationContext pc)
        {
            dw.Write((byte) ItemType.PRESENTATION_CONTEXT_REQUEST);
            dw.WriteNullBytes(1); //Reserved Null Byte
            byte[] internBytes; //Will use to get length
            using (var stream = new MemoryStream())
            {
                using (var intern = new DICOMBinaryWriter(stream))
                {
                    intern.Write((byte) pc.Id);
                    intern.WriteNullBytes(1);
                    intern.Write((byte) pc.Reason);
                    intern.WriteNullBytes(1);
                    WriteAbstractSyntax(intern, pc.AbstractSyntax);
                    foreach (string syntax in pc.TransferSyntaxes)
                    {
                        WriteTransferSyntax(intern, syntax);
                    }
                    internBytes = stream.ToArray();
                }
            }
            LengthWriter.WriteBigEndian(dw, internBytes.Length, 2);
            dw.Write(internBytes);
        }

        /// <summary>
        /// Writes the transfer syntax.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="uid">The uid.</param>
        public static void WriteTransferSyntax(DICOMBinaryWriter dw, string uid)
        {
            WriteUIDItem(dw, ItemType.TRANSFER_SYNTAX, uid);
        }

        /// <summary>
        /// Writes the user information.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="info">The information.</param>
        public static void WriteUserInfo(DICOMBinaryWriter dw, UserInfo info)
        {
            dw.Write((byte) ItemType.USER_INFO);
            dw.WriteNullBytes(1); // Reserved Null Byte
            var body = new byte[0];
            using (var stream = new MemoryStream()) //Will write inner object to get length
            {
                using (var wr = new DICOMBinaryWriter(stream))
                {
                    WriteMaxLength(wr, info.MaxPDULength);
                    WriteImplementationClassUID(wr, info.ImplementationUID);
                    WriteAsyncOperations(wr, info.AsynchronousOperations);
                    WriteImplementationVersion(wr, info.ImplementationVersion);
                    body = stream.ToArray();
                }
            }
            LengthWriter.WriteBigEndian(dw, body.Length, 2);
            dw.Write(body);
        }
    }
}