#region

using System.Net;
using System.Net.Sockets;

#endregion

namespace EvilDICOM.Network.Helpers
{
    public class IpHelper
    {
        public static string LocalIPAddress()
        {
            IPHostEntry host;
            var localIP = "";
            host = Dns.GetHostEntryAsync(Dns.GetHostName()).Result;
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            return localIP;
        }
    }
}