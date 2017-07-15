namespace EvilDICOM.Network.Enums
{
    public enum AbortReason : byte
    {
        REASON_NOT_SPECIFIED = 0x00,
        UNRECOGNIZED_PDU = 0x01,
        UNEXPECTED_PDU = 0x02,
        RESERVED = 0x03,
        UNRECOGNIZED_PDU_PARAMETER = 0x04,
        UNEXPECTED_PDU_PARAMETER = 0x05,
        INVALID_PDU_PARAMETER = 0x06
    }
}