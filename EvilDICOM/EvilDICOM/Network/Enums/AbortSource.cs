namespace EvilDICOM.Network.Enums
{
    public enum AbortSource : byte
    {
        DICOM_UL_SERV_USER = 0x00,
        RESERVED = 0x01,
        DICOM_UL_SERV_PROVIDER = 0x02
    }
}