#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.PDUs.Items;
using EvilDICOM.Network.Processors;
using EvilDICOM.Network.Readers;

#endregion

namespace EvilDICOM.Network
{
    public class Association
    {
        private TcpClient _client;
        private bool _abortRequested;
        private bool _cancelRequested;

        public Association(DICOMServiceClass serviceClass, TcpClient client)
        {
            _client = client;
            ServiceClass = serviceClass;
            Stream = new BufferedStream(client.GetStream());
            Reader = new NetworkBinaryReader(Stream);
            PresentationContexts = new List<PresentationContext>();
            IpAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            Port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
            PDUProcessor = new PDUProcessor();
            PDataProcessor = new PDataProcessor();
            State = NetworkState.IDLE;
            OutboundMessages = new ConcurrentQueue<AbstractDIMSEBase>();
        }

        public bool IsClientConnected
        {
            get
            {
                return _client != null && _client.Connected;
            }
        }

        public DICOMServiceClass ServiceClass { get; private set; }

        /// <summary>
        ///     The agreed upon presentation context of the association
        /// </summary>
        public List<PresentationContext> PresentationContexts { get; set; }

        /// <summary>
        ///     The last time of communication of this association
        /// </summary>
        public DateTime LastActive { get; set; }

        /// <summary>
        ///     The user info containing maximum PDataTF packet size
        /// </summary>
        public UserInfo UserInfo { get; set; }


        public PDUProcessor PDUProcessor { get; set; }
        public PDataProcessor PDataProcessor { get; set; }

        public EventLogger Logger
        {
            get { return ServiceClass.Logger; }
        }

        public ConcurrentQueue<AbstractDIMSEBase> OutboundMessages { get; set; }
        public BufferedStream Stream { get; private set; }
        public NetworkBinaryReader Reader { get; private set; }
        private NetworkState state;
        public NetworkState State
        {
            get { return state; }
            set
            {
                Logger.Log($"|-----{value}-----|");
                state = value;
            }
        }

        public void Listen(TimeSpan? maxWaitTime = null)
        {
            maxWaitTime = maxWaitTime ?? TimeSpan.FromSeconds(25);
            IdleClock = IdleClock ?? new Stopwatch();
            IdleClock.Reset();
            IdleClock.Start();

            while (State != NetworkState.CLOSING_ASSOCIATION && IdleClock.Elapsed < maxWaitTime)
            {
                if (_abortRequested)
                {
                    Logger.Log("Abort requested...aborting.");
                    HandleAbort();
                    break;
                }
                if (_cancelRequested)
                {
                    Logger.Log("Cancellation requested...cancelling.");
                    HandleCancel();
                }

                if (State != NetworkState.CLOSING_ASSOCIATION &&
                   State != NetworkState.TRANSPORT_CONNECTION_OPEN)
                {
                    var message = Read(maxWaitTime.Value.TotalMilliseconds - IdleClock.ElapsedMilliseconds);

                    if (message != null)
                    {
                        try
                        {
                            IdleClock.Restart();
                            Process(message);
                            Stream.Flush();
                            IdleClock.Restart();
                        }
                        catch (IOException e)
                        {
                            Logger.Log($"Network connection was lost. {e.Message}", LogPriority.ERROR);
                            break;//Connection was lost
                        }
                    }
                }

                if (State == NetworkState.TRANSPORT_CONNECTION_OPEN && !OutboundMessages.IsEmpty)
                {
                    while (OutboundMessages.Any())
                        if (State == NetworkState.TRANSPORT_CONNECTION_OPEN)
                        {
                            AbstractDIMSEBase dimse;
                            if (OutboundMessages.TryDequeue(out dimse))
                                PDataMessenger.Send(dimse, this);
                        }
                }

                if (!IsClientConnected)
                {
                    Logger.Log("Connection closed - ending association."); break;
                }
            }
            if (State != NetworkState.CLOSING_ASSOCIATION)
            {
                Logger.Log("Network timeout - closing association.");
            }
        }

        private void HandleCancel()
        {
            AbstractDIMSEBase cancel;
            OutboundMessages.TryPeek(out cancel);
            if (cancel is CCancel)
            {
                OutboundMessages.TryDequeue(out cancel);
                Stream.Flush();
                PDataMessenger.Send(cancel, this);
            }
        }

        private void HandleAbort()
        {
            AssociationMessenger.SendAbort(this);
            State = NetworkState.CLOSING_ASSOCIATION;
        }

        public IMessage Read(double msToWait)
        {
            IMessage message = null;
           // message = PDUReader.Read(Reader);
            Task.Run(() =>
             {
                 try
                 {
                     message = PDUReader.Read(Reader);
                 }
                 catch (Exception e)
                 {
                     Logger.Log(e.Message);
                 }
             }).Wait(TimeSpan.FromMilliseconds(msToWait));
            return message;
        }

        public void Process(IMessage message)
        {
            if (message != null)
                switch (message.Type)
                {
                    case MessageType.PDU:
                        PDUProcessor.Process(message, this);
                        break;
                    case MessageType.PDATA_TF:
                        PDataProcessor.Process(message, this);
                        break;
                    case MessageType.ERROR:
                        ErrorProcessor.Process(message);
                        break;
                }
        }

        public void Release()
        {
            try
            {
                Stream.Flush();
                State = NetworkState.CLOSING_ASSOCIATION;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        public void RequestAbort()
        {
            _abortRequested = true;
        }

        public override string ToString()
        {
            return string.Format("ASSOCIATION\nIP Address : {0}\nPort :{1}\n", IpAddress, Port);
        }

        public void Cancel(CCancel cancel)
        {
            _cancelRequested = true;
            OutboundMessages.Enqueue(cancel);
        }

        #region ASSOCIATION IDENTITY

        /// <summary>
        ///     The foreign AeTitle
        /// </summary>
        public string AeTitle { get; set; }

        /// <summary>
        ///     The ip address of the foreign service class
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        ///     The port of the foreign service class
        /// </summary>
        public int Port { get; set; }

        public Stopwatch IdleClock { get; private set; }

        #endregion

        #region MESSAGING

        public void SendMessage(byte[] message)
        {
            if (message != null && Stream.CanWrite)
                Stream.Write(message, 0, message.Length);
        }

        public void SendMessage(IPDU message)
        {
            SendMessage(message.Write());
        }

        #endregion
    }
}