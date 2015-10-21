namespace EvilDICOM.Network.Enums
{
    public enum RejectSource : byte
    {
        DICOM_UL_SERVICE_USER = 1,
        DICOM_UL_SERVICE_PROVIDER_ACSE = 2,
        DICOM_UL_SERVICE_PROVIDER_PRESENTATION = 3,
    }
}