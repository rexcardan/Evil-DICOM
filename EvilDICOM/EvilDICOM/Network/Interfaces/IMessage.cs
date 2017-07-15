#region

using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.Interfaces
{
    public interface IMessage
    {
        MessageType Type { get; }
        object DynPayload { get; }
    }
}