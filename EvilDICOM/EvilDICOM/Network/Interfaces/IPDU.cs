using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.Interfaces
{
    /// <summary>
    /// Interface IPDU
    /// </summary>
    public interface IPDU
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        PDUType Type { get; }
        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        byte[] Write();
    }
}