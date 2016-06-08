using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.PDUs.Items
{
    /// <summary>
    /// Class PresentationContext.
    /// </summary>
    public class PresentationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationContext"/> class.
        /// </summary>
        public PresentationContext()
        {
            TransferSyntaxes = new List<string>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public PresentationContextReason Reason { get; set; }
        /// <summary>
        /// Gets or sets the abstract syntax.
        /// </summary>
        /// <value>The abstract syntax.</value>
        public string AbstractSyntax { get; set; }
        /// <summary>
        /// Gets or sets the transfer syntaxes.
        /// </summary>
        /// <value>The transfer syntaxes.</value>
        public List<string> TransferSyntaxes { get; set; }

        /// <summary>
        /// To the dicom write settings.
        /// </summary>
        /// <returns>DICOMWriteSettings.</returns>
        public DICOMWriteSettings ToDICOMWriteSettings()
        {
            DICOMWriteSettings settings = DICOMWriteSettings.Default();
            settings.TransferSyntax = TransferSyntaxHelper.GetSyntax(TransferSyntaxes.FirstOrDefault());
            return settings;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("PRESENATION CTX {0} - {1}", Id,
                Enum.GetName(typeof (PresentationContextReason), Reason)));
            sb.AppendLine(string.Format("Abstract Syntax : {0}", AbstractSyntax));
            sb.AppendLine(string.Format("Transfer Syntaxes : {0}", string.Join(" ,", TransferSyntaxes)));
            return sb.ToString();
        }
    }
}