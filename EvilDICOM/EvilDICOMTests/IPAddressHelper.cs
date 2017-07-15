using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network.Tests
{
    [TestClass]
    public class IPAddressHelperTests
    {
        [TestMethod]
        public void TestIPAddressGet()
        {
            var expected = LocalIPAddress();
            var actual = EvilDICOM.Network.Helpers.IpHelper.LocalIPAddress();
            Assert.AreEqual(expected, actual);
        }

        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
