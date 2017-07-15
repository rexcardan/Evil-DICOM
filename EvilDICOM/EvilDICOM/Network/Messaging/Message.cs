using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.Messaging
{
    public class Message<T> : IMessage
    {
        public T Payload { get; set; }
        public MessageType Type { get; set; }

        public object DynPayload
        {
            get { return Payload; }
        }
    }
}