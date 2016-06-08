namespace EvilDICOM.Network.Enums
{
    /// <summary>
    /// Enum PresentationContextReason
    /// </summary>
    public enum PresentationContextReason : byte
    {
        /// <summary>
        /// The acceptance
        /// </summary>
        ACCEPTANCE = 0x00,
        /// <summary>
        /// The user rejection
        /// </summary>
        USER_REJECTION = 0x01,
        /// <summary>
        /// The no reason
        /// </summary>
        NO_REASON = 0x02,
        /// <summary>
        /// The abstractsynaxnotsupported
        /// </summary>
        ABSTRACT_SYNAX_NOT_SUPPORTED = 0x03,
        /// <summary>
        /// The transfersynaxesnotsupported
        /// </summary>
        TRANSFER_SYNAXES_NOT_SUPPORTED = 0x04,
    }
}