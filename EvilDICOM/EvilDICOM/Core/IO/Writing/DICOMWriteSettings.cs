using EvilDICOM.Core.Enums;

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
                TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN,
                DoWriteIndefiniteSequences = false
            };
        }

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