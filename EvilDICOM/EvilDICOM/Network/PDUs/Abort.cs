using System;
using System.IO;
using System.Text;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.Associations.PDUs
{
    public class Abort : IPDU
    {
        public AbortReason Reason { get; set; }
        public AbortSource Source { get; set; }

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

        public PDUType Type
        {
            get { return PDUType.A_ABORT; }
        }

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