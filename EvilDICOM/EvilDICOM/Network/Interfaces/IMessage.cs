using EvilDICOM.Network.Enums;

namespace EvilDICOM.Network.Interfaces
{
    public interface IMessage
    {
        MessageType Type { get; }
        dynamic DynPayload { get; }
    }
}