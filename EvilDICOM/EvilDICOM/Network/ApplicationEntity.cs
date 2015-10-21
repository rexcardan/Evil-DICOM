using EvilDICOM.Network.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network
{
    public class Entity
    {
        private Entity()
        {

        }

        public Entity(string aeTitle, string ipAddress, int port)
        {
            this.AeTitle = aeTitle;
            this.IpAddress = ipAddress;
            this.Port = port;
        }

        public string AeTitle { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }

        public static Entity CreateLocal(string aeTitle, int port)
        {
            return new Entity()
            {
                AeTitle = aeTitle,
                IpAddress = IpHelper.LocalIPAddress(),
                Port = port
            };
        }
    }
}
