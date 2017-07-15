namespace EvilDICOM.Core.Enums
{
    /// <summary>
    ///     An enum representing the transfer syntax of a given DICOM file.
    /// </summary>
    public enum TransferSyntax
    {
        IMPLICIT_VR_LITTLE_ENDIAN,
        EXPLICIT_VR_LITTLE_ENDIAN,
        EXPLICIT_VR_BIG_ENDIAN,
        RLE_LOSSLESS,
        JPEG_BASELINE,
        JPEG_EXTENDED,
        JPEG_PROGRESSIVE,
        JPEG_LOSSLESS_14,
        JPEG_LOSSLESS_15,
        JPEG_LOSSLESS_14_S1,
        JPEG_LS_LOSSLESS,
        JPEG_LS_NEAR_LOSSLESS,
        JPEG_2000_LOSSLESS,
        JPEG_2000
    }
}