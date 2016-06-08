using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.Logging;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs.Items;
using EvilDICOM.Network.Processors;
using EvilDICOM.Network.Readers;
using EvilDICOM.Network.Messaging;
using System.IO;

namespace EvilDICOM.Network
{
    /// <summary>
    /// Class Association.
    /// </summary>
    public class Association
    {
        /// <summary>
        /// The _abort requested
        /// </summary>
        private bool _abortRequested = false;
        /// <summary>
        /// The _cancel requested
        /// </summary>
        private bool _cancelRequested = false;
        /// <summary>
        /// The _memory logger
        /// </summary>
        private ByteMemoryLogger _memoryLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Association"/> class.
        /// </summary>
        /// <param name="serviceClass">The service class.</param>
        /// <param name="client">The client.</param>
        public Association(DICOMServiceClass serviceClass, TcpClient client)
        {
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

        /// <summary>
        /// Gets the service class.
        /// </summary>
        /// <value>The service class.</value>
        public DICOMServiceClass ServiceClass { get; private set; }

        /// <summary>
        /// Sets up a file log of incoming messages
        /// </summary>
        /// <param name="folderStoragePath">The folder storage path.</param>
        public void SetDebugMode(string folderStoragePath)
        {
            _memoryLogger = new ByteMemoryLogger(folderStoragePath);
            Reader.SetLogger(_memoryLogger);
            this.ServiceClass.DIMSEService.DoLogBytes = true;
        }

        /// <summary>
        /// Dimses the service_ c store request received.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="asc">The asc.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void DIMSEService_CStoreRequestReceived(CStoreRequest req, Association asc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The agreed upon presentation context of the association
        /// </summary>
        /// <value>The presentation contexts.</value>
        public List<PresentationContext> PresentationContexts { get; set; }

        /// <summary>
        /// The last time of communication of this association
        /// </summary>
        /// <value>The last active.</value>
        public DateTime LastActive { get; set; }

        /// <summary>
        /// The user info containing maximum PDataTF packet size
        /// </summary>
        /// <value>The user information.</value>
        public UserInfo UserInfo { get; set; }


        /// <summary>
        /// Gets or sets the pdu processor.
        /// </summary>
        /// <value>The pdu processor.</value>
        public PDUProcessor PDUProcessor { get; set; }
        /// <summary>
        /// Gets or sets the p data processor.
        /// </summary>
        /// <value>The p data processor.</value>
        public PDataProcessor PDataProcessor { get; set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public EventLogger Logger
        {
            get { return ServiceClass.Logger; }
        }

        /// <summary>
        /// Gets or sets the outbound messages.
        /// </summary>
        /// <value>The outbound messages.</value>
        public ConcurrentQueue<AbstractDIMSEBase> OutboundMessages { get; set; }
        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public BufferedStream Stream { get; private set; }
        /// <summary>
        /// Gets the reader.
        /// </summary>
        /// <value>The reader.</value>
        public NetworkBinaryReader Reader { get; private set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public NetworkState State { get; set; }

        #region ASSOCIATION IDENTITY

        /// <summary>
        /// The foreign AeTitle
        /// </summary>
        /// <value>The ae title.</value>
        public string AeTitle { get; set; }

        /// <summary>
        /// The ip address of the foreign service class
        /// </summary>
        /// <value>The ip address.</value>
        public string IpAddress { get; set; }

        /// <summary>
        /// The port of the foreign service class
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        #endregion

        /// <summary>
        /// Listens the specified maximum wait time.
        /// </summary>
        /// <param name="maxWaitTime">The maximum wait time.</param>
        public void Listen(TimeSpan? maxWaitTime = null)
        {
            maxWaitTime = maxWaitTime ?? TimeSpan.FromSeconds(25);
            var clock = new Stopwatch();
            clock.Start();
            while (State != NetworkState.CLOSING_ASSOCIATION && clock.Elapsed < maxWaitTime)
            {
                if (_abortRequested) { HandleAbort(); break; }
                if (_cancelRequested) { HandleCancel(); }

                IMessage message = Read();
                if (message == null) { break; } //Error
                clock.Restart();
                Process(message);
                Stream.Flush();
                clock.Restart();
            }
        }

        /// <summary>
        /// Handles the cancel.
        /// </summary>
        private void HandleCancel()
        {
            AbstractDIMSEBase cancel;
            OutboundMessages.TryPeek(out cancel);
            if (cancel is CCancel)
            {
                OutboundMessages.TryDequeue(out cancel);
                this.Stream.Flush();
                PDataMessenger.Send(cancel,this);
            }
        }

        /// <summary>
        /// Handles the abort.
        /// </summary>
        private void HandleAbort()
        {
            AssociationMessenger.SendAbort(this);
            State = NetworkState.CLOSING_ASSOCIATION;
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>IMessage.</returns>
        public IMessage Read()
        {
            try
            {
                IMessage message = PDUReader.Read(Reader);
                return message;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return null;
            }

        }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Process(IMessage message)
        {
            if (message != null)
            {
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
        }

        /// <summary>
        /// Releases this instance.
        /// </summary>
        public void Release()
        {
            State = NetworkState.CLOSING_ASSOCIATION;
            Stream.Flush();
        }

        /// <summary>
        /// Requests the abort.
        /// </summary>
        public void RequestAbort()
        {
            _abortRequested = true;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("ASSOCIATION\nIP Address : {0}\nPort :{1}\n", IpAddress, Port);
        }

        #region MESSAGING

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(byte[] message)
        {
            if (message != null && Stream.CanWrite)
            {
                Stream.Write(message, 0, message.Length);
            }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(IPDU message)
        {
            SendMessage(message.Write());
        }

        #endregion

        /// <summary>
        /// Cancels the specified cancel.
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        public void Cancel(CCancel cancel)
        {
            _cancelRequested = true;
            OutboundMessages.Enqueue(cancel);
        }
    }
}