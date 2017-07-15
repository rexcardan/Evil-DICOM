#region

using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.Interfaces
{
    public interface IPDU
    {
        PDUType Type { get; }
        byte[] Write();
    }
}