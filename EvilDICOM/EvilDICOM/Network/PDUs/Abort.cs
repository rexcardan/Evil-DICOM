using System;
using System.IO;
using System.Text;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.Associations.PDUs
{
    /// <summary>
    /// Class Abort.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Interfaces.IPDU" />
    public class Abort : IPDU
    {
        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public AbortReason Reason { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public AbortSource Source { get; set; }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Write()
        {
            var written = new byte[0];
            var stream = new MemoryStream();
            using (var dw = new DICOMBinaryWriter(stream))
            {
                dw.Write((byte)PDUType.A_ABORT);
                dw.WriteNullBytes(1); //Reserved Null byte
                LengthWriter.WriteBigEndian(dw, 4, 4);
                dw.WriteNullBytes(2); //Reserved Null bytes
                dw.Write((byte)Source);
                dw.Write((byte)Reason);
                written = stream.ToArray();
            }
            return written;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public PDUType Type
        {
            get { return PDUType.A_ABORT; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("A-ABORT");
            sb.AppendLine("-----------------------------");
            sb.AppendLine(string.Format("Reason : {0}", Enum.GetName(typeof(AbortReason), Reason)));
            sb.AppendLine(string.Format("Source : {0}", Enum.GetName(typeof(AbortSource), Source)));
            return sb.ToString();
        }
    }
}