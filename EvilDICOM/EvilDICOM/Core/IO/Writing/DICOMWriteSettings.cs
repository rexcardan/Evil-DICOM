#region

using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMWriteSettings
    {
        public TransferSyntax TransferSyntax { get; set; }
        public bool DoWriteIndefiniteSequences { get; set; }

        public static DICOMWriteSettings Default()
        {
            return new DICOMWriteSettings
            {
                TransferSyntax = TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = false
            };
        }

        public static DICOMWriteSettings DefaultExplicit()
        {
            return new DICOMWriteSettings
            {
                TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = false
            };
        }

        /// <summary>
        /// Write settings when writing the meta header group 0002
        /// </summary>
        /// <returns></returns>
        public DICOMWriteSettings GetFileMetaSettings()
        {
            return new DICOMWriteSettings
            {
                TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = DoWriteIndefiniteSequences
            };
        }
    }
}