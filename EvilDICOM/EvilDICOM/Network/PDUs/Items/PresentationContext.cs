#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.PDUs.Items
{
    public class PresentationContext
    {
        public PresentationContext()
        {
            TransferSyntaxes = new List<string>();
        }

        public int Id { get; set; }
        public PresentationContextReason Reason { get; set; }
        public string AbstractSyntax { get; set; }
        public List<string> TransferSyntaxes { get; set; }

        public DICOMWriteSettings ToDICOMWriteSettings()
        {
            var settings = DICOMWriteSettings.Default();
            settings.TransferSyntax = TransferSyntaxHelper.GetSyntax(TransferSyntaxes.FirstOrDefault());
            return settings;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("PRESENATION CTX {0} - {1}", Id,
                Enum.GetName(typeof(PresentationContextReason), Reason)));
            sb.AppendLine(string.Format("Abstract Syntax : {0}", AbstractSyntax));
            sb.AppendLine(string.Format("Transfer Syntaxes : {0}", string.Join(" ,", TransferSyntaxes)));
            return sb.ToString();
        }
    }
}