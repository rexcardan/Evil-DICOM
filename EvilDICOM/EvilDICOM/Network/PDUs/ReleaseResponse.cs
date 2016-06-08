using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.PDUs
{
    /// <summary>
    /// Class ReleaseResponse.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Interfaces.IPDU" />
    public class ReleaseResponse : IPDU
    {
        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Write()
        {
            return new byte[] {0x06, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00};
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public PDUType Type
        {
            get { return PDUType.A_RELEASE_RESPONSE; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return "RELEASE RP\n-----------------------------\n";
        }
    }
}