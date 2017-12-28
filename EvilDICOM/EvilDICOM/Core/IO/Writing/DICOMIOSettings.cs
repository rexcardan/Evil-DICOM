#region

using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMIOSettings
    {
        public TransferSyntax TransferSyntax { get; set; }
        public bool DoWriteIndefiniteSequences { get; set; }
        public StringEncoding StringEncoding { get; set; }

        public static DICOMIOSettings Default()
        {
            return new DICOMIOSettings
            {
                TransferSyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = false,
                StringEncoding = StringEncoding.ISO_IR_192
            };
        }

        public static DICOMIOSettings DefaultExplicit()
        {
            return new DICOMIOSettings
            {
                TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = false,
                StringEncoding = StringEncoding.ISO_IR_192
    };
        }

        /// <summary>
        /// Write settings when writing the meta header group 0002
        /// </summary>
        /// <returns></returns>
        public DICOMIOSettings GetFileMetaSettings()
        {
            return new DICOMIOSettings
            {
                TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = DoWriteIndefiniteSequences
            };
        }
    }
}