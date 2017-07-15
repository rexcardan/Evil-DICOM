#region

using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

#endregion

namespace EvilDICOM.Network.PDUs
{
    public class ReleaseResponse : IPDU
    {
        public byte[] Write()
        {
            return new byte[] {0x06, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00};
        }

        public PDUType Type
        {
            get { return PDUType.A_RELEASE_RESPONSE; }
        }

        public override string ToString()
        {
            return "RELEASE RP\n-----------------------------\n";
        }
    }
}