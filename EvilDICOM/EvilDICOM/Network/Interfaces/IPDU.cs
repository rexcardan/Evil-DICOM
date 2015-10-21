using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.Interfaces
{
    public interface IPDU
    {
        PDUType Type { get; }
        byte[] Write();
    }
}