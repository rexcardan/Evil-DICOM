#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilDICOM.Core;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Network.Enums;
using EvilDICOM.Network.Interfaces;
using EvilDICOM.Network.PDUs.Items;

#endregion

namespace EvilDICOM.Network.PDUs
{
    public class PDataTF : IPDU
    {
        public PDataTF()
        {
            Items = new List<PDVItem>();
        }

        public PDataTF(DICOMObject dicom, bool isLastItem, bool isCommandObject, PresentationContext context)
            : this()
        {
            byte[] data;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    var settings = new DICOMIOSettings();
                    settings.TransferSyntax = isCommandObject
                        ? TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN
                        : TransferSyntaxHelper.GetSyntax(context.TransferSyntaxes[0]);
                    DICOMObjectWriter.Write(dw, settings, dicom,true);
                    data = stream.ToArray();
                }
            }
            var frag = new PDVItemFragment();
            frag.Data = data;
            frag.IsLastItem = isLastItem;
            frag.IsCommandObject = isCommandObject;
            var item = new PDVItem();
            item.Fragment = frag;
            item.PresentationContextID = context.Id;
            Items.Add(item);
        }

        public PDataTF(byte[] data, bool isLastItem, bool isCommandObject, PresentationContext context)
            : this()
        {
            var frag = new PDVItemFragment();
            frag.Data = data;
            frag.IsLastItem = isLastItem;
            frag.IsCommandObject = isCommandObject;
            var item = new PDVItem();
            item.Fragment = frag;
            item.PresentationContextID = context.Id;
            Items.Add(item);
        }

        public List<PDVItem> Items { get; set; }

        public byte[] Write()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    dw.Write((byte) PDUType.P_DATA_TRANSFER);
                    dw.WriteNullBytes(1); //Reserved Null byte
                    var items = WriteItems();
                    LengthWriter.WriteBigEndian(dw, items.Length, 4);
                    dw.Write(items);
                    written = stream.ToArray();
                }
            }
            return written;
        }

        public PDUType Type
        {
            get { return PDUType.P_DATA_TRANSFER; }
        }

        public byte[] GetMergedItemData()
        {
            var data = new List<byte>();
            Items
                .Select(i => i.Fragment.Data)
                .ToList()
                .ForEach(d => data.AddRange(d));
            return data.ToArray();
        }

        private byte[] WriteItems()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    foreach (var item in Items)
                        ItemWriter.WritePDVItem(dw, item);
                    written = stream.ToArray();
                }
            }
            return written;
        }
    }
}