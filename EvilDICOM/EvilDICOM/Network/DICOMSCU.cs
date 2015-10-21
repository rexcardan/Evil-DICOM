using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Network.DIMSE;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Messaging;
using System.Net;

namespace EvilDICOM.Network
{
    public class DICOMSCU : DICOMServiceClass
    {
        public DICOMSCU(Entity ae) : base(ae) { }

        public void SendMessage(AbstractDIMSERequest dimse, Entity ae)
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ae.IpAddress), ae.Port);
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(dimse, assoc);
            assoc.Listen();
        }

        public IEnumerable<CFindResponse> GetResponse(CFindRequest cFind, Entity ae)
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ae.IpAddress), ae.Port);
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(cFind, assoc);
            List<CFindResponse> responses = new List<CFindResponse>();

            EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CFindResponse> action = null;
            action = (resp, asc) =>
             {
                 responses.Add(resp);
                 if (resp.Status != (ushort)Status.PENDING)
                 {
                     this.DIMSEService.CFindResponseReceived -= action;
                 }
             };
            this.DIMSEService.CFindResponseReceived += action;
            assoc.Listen();
            return responses;
        }

        public IEnumerable<CMoveResponse> GetResponse(CMoveRequest cMove, Entity ae)
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Parse(ae.IpAddress), ae.Port);
            var assoc = new Association(this, client) { AeTitle = ae.AeTitle };
            PDataMessenger.Send(cMove, assoc);
            List<CMoveResponse> responses = new List<CMoveResponse>();

            EvilDICOM.Network.Services.DIMSEService.DIMSEResponseHandler<CMoveResponse> action = null;
            action = (resp, asc) =>
            {
                responses.Add(resp);
                if (resp.Status != (ushort)Status.PENDING)
                {
                    this.DIMSEService.CMoveResponseReceived -= action;
                }
            };
            this.DIMSEService.CMoveResponseReceived += action;
            assoc.Listen();
            return responses;
        }

        public CStoreRequest GenerateCStoreRequest(Core.DICOMObject toSend, ushort messageId = 1)
        {
            var sel = toSend.GetSelector();
            var cStoreReq = new CStoreRequest();
            cStoreReq.AffectedSOPClassUID = sel.SOPClassUID.Data;
            cStoreReq.Priority = 1;
            cStoreReq.Data = toSend;
            cStoreReq.MessageID = messageId;
            cStoreReq.AffectedSOPInstanceUID = sel.SOPInstanceUID.Data;
            cStoreReq.MoveOrigAETitle = this.ApplicationEntity.AeTitle;
            cStoreReq.MoveOrigMessageID = messageId;
            return cStoreReq;
        }
    }
}
