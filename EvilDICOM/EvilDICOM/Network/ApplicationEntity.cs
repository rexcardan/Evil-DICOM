#region

using EvilDICOM.Network.Helpers;

#endregion

namespace EvilDICOM.Network
{
    public class Entity
    {
        private Entity()
        {
        }

        public Entity(string aeTitle, string ipAddress, int port)
        {
            AeTitle = aeTitle;
            IpAddress = ipAddress;
            Port = port;
        }

        public string AeTitle { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        public static Entity CreateLocal(string aeTitle, int port)
        {
            return new Entity
            {
                AeTitle = aeTitle,
                IpAddress = IpHelper.LocalIPAddress(),
                Port = port
            };
        }

        public override string ToString()
        {
            return $"{nameof(AeTitle)}: {AeTitle}, {nameof(IpAddress)}: {IpAddress}, {nameof(Port)}: {Port}";
        }
    }
}