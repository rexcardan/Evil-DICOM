using System;
using System.Collections.Generic;
using EvilDICOM.Core.Extensions;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class ItemReader.
    /// </summary>
    public class ItemReader
    {
        /// <summary>
        /// Asserts the type of the item.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <exception cref="System.Exception"></exception>
        public static void AssertItemType(DICOMBinaryReader dr, string itemName, ItemType itemType)
        {
            byte header = dr.Peek(1)[0];
            if (header != (byte) itemType)
                throw new Exception();
        }

        /// <summary>
        /// Reads the uid item.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="iType">Type of the i.</param>
        /// <returns>System.String.</returns>
        private static string ReadUIDItem(DICOMBinaryReader dr, string itemName, ItemType iType)
        {
            AssertItemType(dr, itemName, iType);
            dr.Skip(2); // PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            return dr.ReadString(length).Trim();
        }

        /// <summary>
        /// Reads the abstract syntax.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.String.</returns>
        public static string ReadAbstractSyntax(DICOMBinaryReader dr)
        {
            return ReadUIDItem(dr, "Abstact Syntax", ItemType.ABSTRACT_SYNTAX);
        }

        /// <summary>
        /// Reads the application context.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.String.</returns>
        public static string ReadApplicationContext(DICOMBinaryReader dr)
        {
            return ReadUIDItem(dr, "Application Context", ItemType.APPLICATION_CONTEXT);
        }

        /// <summary>
        /// Reads the asynchronous operations.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>AsyncOperations.</returns>
        public static AsyncOperations ReadAsyncOperations(DICOMBinaryReader dr)
        {
            AssertItemType(dr, "Async Operations", ItemType.ASYNCHRONOUS_OPERATIONS_WINDOW);
            var ao = new AsyncOperations();
            dr.Skip(2); // // PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            ao.MaxInvokeOperations = LengthReader.ReadBigEndian(dr, 2);
            ao.MaxPerformOperations = LengthReader.ReadBigEndian(dr, 2);
            return ao;
        }

        /// <summary>
        /// Reads the implementation class uid.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.String.</returns>
        public static string ReadImplementationClassUID(DICOMBinaryReader dr)
        {
            return ReadUIDItem(dr, "Implementation Class UID", ItemType.IMPLEMENTATION_CLASS_UID);
        }

        /// <summary>
        /// Reads the implementation version.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.String.</returns>
        public static string ReadImplementationVersion(DICOMBinaryReader dr)
        {
            return ReadUIDItem(dr, "Implementation Version", ItemType.IMPLEMENTATION_VERSION_NAME);
        }

        /// <summary>
        /// Reads the maximum length.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public static int? ReadMaxLength(DICOMBinaryReader dr)
        {
            AssertItemType(dr, "Maximum Length", ItemType.MAXIMUM_LENGTH);
            dr.Skip(2); // PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            return LengthReader.ReadBigEndian(dr, 4);
        }

        /// <summary>
        /// Reads the PDV item.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>PDVItem.</returns>
        public static PDVItem ReadPDVItem(DICOMBinaryReader dr)
        {
            var pi = new PDVItem();
            int length = LengthReader.ReadBigEndian(dr, 4);
            pi.PresentationContextID = dr.Take(1)[0];
            pi.Fragment = ReadPDVFragment(dr, length - 1);
            return pi;
        }

        /// <summary>
        /// Reads the PDV fragment.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="length">The length.</param>
        /// <returns>PDVItemFragment.</returns>
        public static PDVItemFragment ReadPDVFragment(DICOMBinaryReader dr, int length)
        {
            var pif = new PDVItemFragment();
            byte messageHeader = dr.Take(1)[0];
            pif.IsCommandObject = messageHeader.GetBit(0);
            pif.IsLastItem = messageHeader.GetBit(1);
            pif.Data = dr.ReadBytes(length - 1);
            return pif;
        }

        /// <summary>
        /// Reads the presentation CTX request.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>PresentationContext.</returns>
        public static PresentationContext ReadPresentationCtxRequest(DICOMBinaryReader dr)
        {
            AssertItemType(dr, "Presentation Context Request", ItemType.PRESENTATION_CONTEXT_REQUEST);
            dr.Skip(2); // PDU id Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            return ReadPresentationCtxContents(dr.Take(length), true);
        }

        /// <summary>
        /// Reads the presentation CTX accept.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>PresentationContext.</returns>
        public static PresentationContext ReadPresentationCtxAccept(DICOMBinaryReader dr)
        {
            AssertItemType(dr, "Presentation Context Accept", ItemType.PRESENTATION_CONTEXT_ACCEPT);
            dr.Skip(2); // PDU id Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            return ReadPresentationCtxContents(dr.Take(length));
        }

        /// <summary>
        /// Reads the presentation CTX contents.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <param name="requestType">if set to <c>true</c> [request type].</param>
        /// <returns>PresentationContext.</returns>
        private static PresentationContext ReadPresentationCtxContents(byte[] contents, bool requestType = false)
        {
            var pc = new PresentationContext();
            pc.TransferSyntaxes = new List<string>();
            using (var dr = new DICOMBinaryReader(contents))
            {
                pc.Id = dr.Take(1)[0];
                dr.Skip(1); //Reserved Null Byte
                pc.Reason = (PresentationContextReason) Enum.ToObject(typeof (PresentationContextReason), dr.Take(1)[0]);
                dr.Skip(1); //Reserved Null Byte
                if (requestType)
                {
                    pc.AbstractSyntax = ReadAbstractSyntax(dr).Trim();
                }
                while (dr.StreamPosition < dr.StreamLength)
                {
                    long initPos = dr.StreamPosition;
                    pc.TransferSyntaxes.Add(ReadTransferSyntax(dr));
                    if (dr.StreamPosition == initPos)
                    {
                        break;
                    }
                }
            }
            return pc;
        }

        /// <summary>
        /// Reads the transfer syntax.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>System.String.</returns>
        public static string ReadTransferSyntax(DICOMBinaryReader dr)
        {
            return ReadUIDItem(dr, "Transfer Syntax", ItemType.TRANSFER_SYNTAX);
        }

        /// <summary>
        /// Reads the user information.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns>UserInfo.</returns>
        public static UserInfo ReadUserInfo(DICOMBinaryReader dr)
        {
            AssertItemType(dr, "User Info", ItemType.USER_INFO);
            var ui = new UserInfo();
            dr.Skip(2); // PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr, 2);
            if (length > 0)
            {
                ui.MaxPDULength = (int) ReadMaxLength(dr);
                ui.ImplementationUID = ReadImplementationClassUID(dr);
                if (dr.Peek(1)[0] == (byte) ItemType.ASYNCHRONOUS_OPERATIONS_WINDOW)
                {
                    ui.AsynchronousOperations = ReadAsyncOperations(dr);
                }
                ui.ImplementationVersion = ReadImplementationVersion(dr);
            }
            return ui;
        }
    }
}