#region

using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

#endregion

namespace EvilDICOM.Network.PDUs
{
    public class ReleaseRequest : IPDU
    {
        public byte[] Write()
        {
            return new byte[] {0x05, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00};
        }

        public PDUType Type
        {
            get { return PDUType.A_RELEASE_REQUEST; }
        }

        public override string ToString()
        {
            return "RELEASE RQ\n-----------------------------\n";
        }
    }
}