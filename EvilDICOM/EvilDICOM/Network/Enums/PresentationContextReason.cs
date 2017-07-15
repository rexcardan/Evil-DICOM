namespace EvilDICOM.Network.Enums
{
    public enum PresentationContextReason : byte
    {
        ACCEPTANCE = 0x00,
        USER_REJECTION = 0x01,
        NO_REASON = 0x02,
        ABSTRACT_SYNAX_NOT_SUPPORTED = 0x03,
        TRANSFER_SYNAXES_NOT_SUPPORTED = 0x04,
    }
}