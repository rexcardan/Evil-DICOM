using System;
using System.Collections.Generic;
using System.Linq;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Network.Associations.PDUs;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.Messaging;
using EvilDICOM.Network.PDUs;
using EvilDICOM.Network.PDUs.Items;
using System.IO;

namespace EvilDICOM.Network.Readers
{
    public class PDUReader
    {
        public static IMessage Read(NetworkBinaryReader dr)
        {
            byte key = dr.Take(1)[0];

            IMessage pdu;
            switch (key)
            {
                //-----------------------------A-REQUEST------------------------------
                case (byte)PDUType.A_ASSOC_REQUEST:
                    pdu = ReadAssociationRequest(dr);
                    break;
                //-----------------------------A-ACCEPT------------------------------
                case (byte)PDUType.A_ASSOC_ACCEPT:
                    pdu = ReadAssociationAccept(dr);
                    break;
                //-----------------------------A-REJECT------------------------------
                case (byte)PDUType.A_ASSOC_REJECT:
                    pdu = ReadAssociationReject(dr);
                    break;
                //-----------------------------A-ABORT------------------------------
                case (byte)PDUType.A_ABORT:
                    pdu = ReadAbort(dr);
                    break;
                //-----------------------------A-RELEASE-RQ------------------------------
                case (byte)PDUType.A_RELEASE_REQUEST:
                    pdu = ReadReleaseRequest(dr);
                    break;
                //-----------------------------A-RELEASE-RP------------------------------
                case (byte)PDUType.A_RELEASE_RESPONSE:
                    pdu = ReadReleaseResponse(dr);
                    break;
                //-----------------------------P-DATA-TF------------------------------
                case (byte)PDUType.P_DATA_TRANSFER:
                    pdu = ReadPDFData(dr, true);
                    break;
                //-----------------------------UNKNOWN------------------------------
                default:
                    throw new Exception("Unknown PDU");
            }

            return pdu;
        }

        public static Message<Abort> ReadAbort(NetworkBinaryReader dr)
        {
            var abort = new Abort();
            dr.Skip(1); //Skip null and header
            dr.Skip(4); //Skip length
            dr.Skip(2); //Skip null
            abort.Source = (AbortSource)dr.Take(1).First();
            abort.Reason = (AbortReason)dr.Skip(1).Take(1).First();
            return new Message<Abort> { Payload = abort, Type = MessageType.PDU };
        }

        public static Message<ReleaseRequest> ReadReleaseRequest(NetworkBinaryReader dr)
        {
            var relReq = new ReleaseRequest();
            if (relReq.Write().Skip(1).SequenceEqual(dr.Take(9)))
            {
                return new Message<ReleaseRequest> { Payload = relReq, Type = MessageType.PDU };
            }
            //Invalid release request
            throw new Exception("Release request was invalid. Did not match signature.");
        }

        public static Message<ReleaseResponse> ReadReleaseResponse(NetworkBinaryReader dr)
        {
            var relRes = new ReleaseResponse();
            if (relRes.Write().Skip(1).SequenceEqual(dr.Take(9)))
            {
                return new Message<ReleaseResponse> { Payload = relRes, Type = MessageType.PDU };
            }
            //Invalid release response
            throw new Exception("Release response was invalid. Did not match signature.");
        }

        public static Message<Accept> ReadAssociationAccept(NetworkBinaryReader dr)
        {
            var acc = new Accept();
            acc.PresentationContexts = new List<PresentationContext>();

            dr.Skip(1); //PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr.Take(4));

            using (DICOMBinaryReader sub = dr.GetSubStream(length))
            {
                acc.ProtocolVersion = LengthReader.ReadBigEndian(sub, 2);
                sub.Skip(2); //Reserved Null Bytes
                acc.CalledEntityTitle = sub.ReadString(16).Trim();
                acc.CallingEntityTitle = sub.ReadString(16).Trim();
                sub.Skip(32); //Reserved Null Bytes
                acc.ApplicationContext = ItemReader.ReadApplicationContext(sub);
                while (sub.Peek(1).First() == (byte)ItemType.PRESENTATION_CONTEXT_ACCEPT)
                {
                    PresentationContext context = ItemReader.ReadPresentationCtxAccept(sub);
                    if (context != null)
                    {
                        acc.PresentationContexts.Add(context);
                    }
                }
                acc.UserInfo = ItemReader.ReadUserInfo(sub);
            }
            return new Message<Accept> { Payload = acc, Type = MessageType.PDU };
        }

        public static Message<Reject> ReadAssociationReject(NetworkBinaryReader dr)
        {
            var reject = new Reject();
            dr.Skip(1); //Skip null and header
            dr.Skip(4); //Skip length
            dr.Skip(1); //Skip null
            reject.Result = (RejectResult)dr.Take(1).First();
            reject.Source = (RejectSource)dr.Take(1).First();
            reject.Reason = dr.Take(1).First();
            return new Message<Reject> { Payload = reject, Type = MessageType.PDU };
        }

        public static Message<Request> ReadAssociationRequest(NetworkBinaryReader dr)
        {
            var rq = new Request();
            rq.PresentationContexts = new List<PresentationContext>();
            dr.Skip(1); //PDU ID and Reserved Null Byte
            int length = LengthReader.ReadBigEndian(dr.Take(4));
            using (DICOMBinaryReader sub = dr.GetSubStream(length))
            {
                rq.ProtocolVersion = LengthReader.ReadBigEndian(sub, 2);
                sub.Skip(2); //Reserved Null Bytes
                rq.CalledEntityTitle = sub.ReadString(16).Trim();
                rq.CallingEntityTitle = sub.ReadString(16).Trim();
                sub.Skip(32); //Reserved Null Bytes
                rq.ApplicationContext = ItemReader.ReadApplicationContext(sub);
                while (sub.Peek(1)[0] == (byte)ItemType.PRESENTATION_CONTEXT_REQUEST)
                {
                    PresentationContext context = ItemReader.ReadPresentationCtxRequest(sub);
                    rq.PresentationContexts.Add(context);
                }
                rq.UserInfo = ItemReader.ReadUserInfo(sub);
            }
            return new Message<Request> { Payload = rq, Type = MessageType.PDU };
        }

        public static Message<PDataTF> ReadPDFData(NetworkBinaryReader dr, bool firstByteRead = false, int i = 0)
        {
            PDataTF pdata = null;
            pdata = new PDataTF();
            if (!firstByteRead)
            {
                dr.Skip(1);
            }
            dr.Skip(1); // PDU ID and Reserved Null Byte
            var lengthBytes = dr.Take(4);
            int length = LengthReader.ReadBigEndian(lengthBytes);
            using (DICOMBinaryReader dbr = dr.GetSubStream(length))
            {
                while (dbr.StreamPosition < dbr.StreamLength)
                {
                    PDVItem item = ItemReader.ReadPDVItem(dbr);
                    pdata.Items.Add(item);
                }
            }
            return new Message<PDataTF> { Payload = pdata, Type = MessageType.PDATA_TF };
        }
    }
}