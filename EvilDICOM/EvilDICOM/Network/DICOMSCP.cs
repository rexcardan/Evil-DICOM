#region

using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using EvilDICOM.Network.PDUs.Items;

#endregion

namespace EvilDICOM.Network
{
    public class DICOMSCP : DICOMServiceClass
    {
        public delegate void StopHandler();

        private readonly TcpListener _server;
        private bool _started;

        public DICOMSCP(Entity ae) : base(ae)
        {
            _server = new TcpListener(IPAddress.Parse(ApplicationEntity.IpAddress), ApplicationEntity.Port);
        }

        public bool IsListening
        {
            get { return _started; }
        }

        public async void ListenForIncomingAssociations(bool keepListenerRunning)
        {
            while (true)
            {
                if (!_started)
                {
                    _server.Start();
                    _started = true;
                }
                var connection = await _server.AcceptTcpClientAsync();
                SpinUpAssociation(connection);
                if (!keepListenerRunning)
                    break;
            }
        }

        private void SpinUpAssociation(TcpClient client)
        {
            Task.Factory.StartNew(() =>
            {
                var asc = GenerateAssociation(client);
                Logger.Log(asc.ToString());
                asc.Listen();
                Logger.Log("Closing association with {0}.", asc.IpAddress, asc.Port);
#if NET45
                client.Close();
#else
                client.Dispose();
#endif
            });
        }

        /// <summary>
        /// Generates an association from a TCP client connection
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private Association GenerateAssociation(TcpClient connection)
        {
            var asc = new Association(this, connection);
            Logger.Log("Starting association with {0}.", asc.IpAddress, asc.Port);
            //Add supported presentation contexts
            foreach (var ab in SupportedAbstractSyntaxes)
                asc.PresentationContexts.Add(new PresentationContext
                {
                    AbstractSyntax = ab,
                    TransferSyntaxes = SupportedTransferSyntaxes
                });
            return asc;
        }

        public void Stop()
        {
            _server.Stop();
        }

        public event StopHandler SCPStopped;

        public void RaisedSCPStopped()
        {
            if (SCPStopped != null)
                SCPStopped();
        }
    }
}