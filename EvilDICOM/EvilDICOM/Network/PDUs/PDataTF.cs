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

namespace EvilDICOM.Network.PDUs
{
    /// <summary>
    /// Class PDataTF.
    /// </summary>
    /// <seealso cref="EvilDICOM.Network.Interfaces.IPDU" />
    public class PDataTF : IPDU
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PDataTF"/> class.
        /// </summary>
        public PDataTF()
        {
            Items = new List<PDVItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PDataTF"/> class.
        /// </summary>
        /// <param name="dicom">The dicom.</param>
        /// <param name="isLastItem">if set to <c>true</c> [is last item].</param>
        /// <param name="isCommandObject">if set to <c>true</c> [is command object].</param>
        /// <param name="context">The context.</param>
        public PDataTF(DICOMObject dicom, bool isLastItem, bool isCommandObject, PresentationContext context)
            : this()
        {
            byte[] data;
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    var settings = new DICOMWriteSettings();
                    settings.TransferSyntax = isCommandObject
                        ? TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN
                        : TransferSyntaxHelper.GetSyntax(context.TransferSyntaxes[0]);
                    DICOMObjectWriter.Write(dw, settings, dicom);
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PDataTF"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="isLastItem">if set to <c>true</c> [is last item].</param>
        /// <param name="isCommandObject">if set to <c>true</c> [is command object].</param>
        /// <param name="context">The context.</param>
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

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<PDVItem> Items { get; set; }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] Write()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    dw.Write((byte) PDUType.P_DATA_TRANSFER);
                    dw.WriteNullBytes(1); //Reserved Null byte
                    byte[] items = WriteItems();
                    LengthWriter.WriteBigEndian(dw, items.Length, 4);
                    dw.Write(items);
                    written = stream.ToArray();
                }
            }
            return written;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public PDUType Type
        {
            get { return PDUType.P_DATA_TRANSFER; }
        }

        /// <summary>
        /// Gets the merged item data.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public byte[] GetMergedItemData()
        {
            var data = new List<byte>();
            Items
                .Select(i => i.Fragment.Data)
                .ToList()
                .ForEach(d => data.AddRange(d));
            return data.ToArray();
        }

        /// <summary>
        /// Writes the items.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        private byte[] WriteItems()
        {
            var written = new byte[0];
            using (var stream = new MemoryStream())
            {
                using (var dw = new DICOMBinaryWriter(stream))
                {
                    foreach (PDVItem item in Items)
                    {
                        ItemWriter.WritePDVItem(dw, item);
                    }
                    written = stream.ToArray();
                }
            }
            return written;
        }
    }
}