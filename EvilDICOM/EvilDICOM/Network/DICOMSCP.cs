#region

using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using EvilDICOM.Network.PDUs.Items;
using System;
using System.Diagnostics;
using System.Text;

#endregion

namespace EvilDICOM.Network
{
    public class DICOMSCP : DICOMServiceClass
    {
        public delegate void StopHandler();

        private readonly TcpListener _server;
        private bool _started;
        private bool _requestStop = false;

        public DICOMSCP(Entity ae) : base(ae)
        {
            _server = new TcpListener(IPAddress.Parse(ApplicationEntity.IpAddress), ApplicationEntity.Port);
#if NETCOREAPP

            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
#endif
        }

        public bool IsListening
        {
            get { return _started; }
        }

        public async void ListenForIncomingAssociations(bool keepListenerRunning)
        {
            _requestStop = false;
            while (!_requestStop)
            {
                if (!_started)
                {
                    _server.Start();
                    _started = true;
                }
                try
                {
                    var connection = await _server.AcceptTcpClientAsync();
                    SpinUpAssociation(connection);
                    if (!keepListenerRunning || _requestStop)
                        break;
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
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
                asc.PresentationContexts.Add(new PresentationContext
                {
                    AbstractSyntax = ab,
                    TransferSyntaxes = SupportedTransferSyntaxes
                });
            return asc;
        }

        public void Stop()
        {
            _requestStop = true;
            _server.Stop();
            _started = false;
            RaisedSCPStopped();
        }

        public event StopHandler SCPStopped;

        public void RaisedSCPStopped()
        {
            if (SCPStopped != null)
                SCPStopped();
        }
    }
}