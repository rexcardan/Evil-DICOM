﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs.Items;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Network.PDUs
{
    /// <summary>
    /// Class Accept.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Interfaces.IPDU" />
    public class Accept : IPDU
    {
        /// <summary>
        /// Gets or sets the protocol version.
        /// </summary>
        /// <value>The protocol version.</value>
        public int ProtocolVersion { get; set; }
        /// <summary>
        /// Gets or sets the called entity title.
        /// </summary>
        /// <value>The called entity title.</value>
        public string CalledEntityTitle { get; set; }
        /// <summary>
        /// Gets or sets the calling entity title.
        /// </summary>
        /// <value>The calling entity title.</value>
        public string CallingEntityTitle { get; set; }
        /// <summary>
        /// Gets or sets the application context.
        /// </summary>
        /// <value>The application context.</value>
        public string ApplicationContext { get; set; }
        /// <summary>
        /// Gets or sets the presentation contexts.
        /// </summary>
        /// <value>The presentation contexts.</value>
        public List<PresentationContext> PresentationContexts { get; set; }
        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        /// <value>The user information.</value>
        public UserInfo UserInfo { get; set; }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Write()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    dw.Write((byte)PDUType.A_ASSOC_ACCEPT);
                    dw.WriteNullBytes(1); //Reserved Null byte
                    byte[] body = WriteBody();
                    LengthWriter.WriteBigEndian(dw, body.Length, 4);
                    dw.Write(body);
                    written = stream.ToArray();
                }
            }
            return written;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public PDUType Type
        {
            get { return PDUType.A_ASSOC_ACCEPT; }
        }

        /// <summary>
        /// Writes the body.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        private byte[] WriteBody()
        {
            var body = new byte[0];
            var stream = new MemoryStream();
            using (var dw = new DICOMBinaryWriter(stream))
            {
                //Main body
                LengthWriter.WriteBigEndian(dw, ProtocolVersion, 2); //Protocol Version
                dw.WriteNullBytes(2); //Reserved Null bytes
                dw.Write(CalledEntityTitle.PadRight(16));
                dw.Write(CallingEntityTitle.PadRight(16));
                dw.WriteNullBytes(32); //Reserved Null bytes
                ItemWriter.WriteApplicationContext(dw, ApplicationContext);
                foreach (PresentationContext pc in PresentationContexts)
                {
                    ItemWriter.WritePresentationCtxAcceptType(dw, pc);
                }
                ItemWriter.WriteUserInfo(dw, UserInfo);
                body = stream.ToArray();
            }

            return body;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("A-ACCEPT");
            sb.AppendLine("-----------------------------");
            sb.AppendLine(string.Format("Called Entity Title : {0}", CalledEntityTitle));
            sb.AppendLine(string.Format("Calling Entity Title : {0}", CallingEntityTitle));
            sb.AppendLine(string.Format("Application Context : {0}", ApplicationContext));
            sb.AppendLine(string.Format("Protocol Version : {0}", ProtocolVersion));
            sb.AppendLine();
            sb.AppendLine(UserInfo.ToString());
            foreach (PresentationContext ctx in PresentationContexts)
            {
                sb.AppendLine(ctx.ToString());
            }
            sb.AppendLine();
            return sb.ToString();
        }

        /// <summary>
        /// Generates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="contexts">The contexts.</param>
        /// <returns>Accept.</returns>
        public static Accept Generate(Request request, List<PresentationContext> contexts)
        {
            var accept = new Accept
            {
                ApplicationContext = request.ApplicationContext,
                CalledEntityTitle = request.CalledEntityTitle,
                CallingEntityTitle = request.CallingEntityTitle,
                PresentationContexts = contexts,
                ProtocolVersion = request.ProtocolVersion,
                UserInfo = new UserInfo
                {
                    AsynchronousOperations = null,
                    ImplementationUID = Constants.EVIL_DICOM_IMP_UID,
                    ImplementationVersion = Constants.EVIL_DICOM_IMP_VERSION,
                    MaxPDULength = request.UserInfo.MaxPDULength
                }
            };
            return accept;
        }
    }
}