using EvilDICOM.Network.PDUs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EvilDICOM.Network
{
    public class DICOMSCP : DICOMServiceClass
    {
        TcpListener _server;
        bool _started = false;

        public DICOMSCP(Entity ae) : base(ae) { _server = new TcpListener(IPAddress.Parse(ApplicationEntity.IpAddress), ApplicationEntity.Port); }

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
                {
                    break;
                }
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
                client.Close();
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
            {
                asc.PresentationContexts.Add(new PresentationContext()
                {
                    AbstractSyntax = ab,
                    TransferSyntaxes = SupportedTransferSyntaxes
                });
            }
            return asc;
        }

        public void Stop()
        {
            _server.Stop();
        }

        public bool IsListening
        {
            get { return _started; }
        }

        public delegate void StopHandler();

        public event StopHandler SCPStopped;

        public void RaisedSCPStopped()
        {
            if (SCPStopped != null)
            {
                SCPStopped();
            }
        }
    }
}
