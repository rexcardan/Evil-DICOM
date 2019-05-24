#region

using EvilDICOM.Core.Logging;
using System.Net;
using System.Net.Sockets;

#endregion

namespace EvilDICOM.Network.Helpers
{
    public class IpHelper
    {
        public static EventLogger Logger = new EventLogger();

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

        public static (IPEndPoint Address, bool Success) VerifyIPAddress(string inputIpAddress, int port)
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(inputIpAddress, out ipAddress))
            {
                Logger.Log($"Could not parse IP address {inputIpAddress}");
                return (null, false);
            }
            IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, port);
            return (ipLocalEndPoint, true);
        }
    }
}