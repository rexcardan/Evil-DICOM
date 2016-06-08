using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;

namespace EvilDICOM.Network.Messaging
{
    /// <summary>
    /// Class Message.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="EvilDICOM.Network.Interfaces.IMessage" />
    public class Message<T> : IMessage
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public T Payload { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public MessageType Type { get; set; }

        /// <summary>
        /// Gets the dyn payload.
        /// </summary>
        /// <value>The dyn payload.</value>
        public dynamic DynPayload
        {
            get { return Payload; }
        }
    }
}