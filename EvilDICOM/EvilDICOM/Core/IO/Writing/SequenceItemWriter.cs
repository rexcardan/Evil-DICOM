using System.Collections.Generic;
using System.IO;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Writing
{
    /// <summary>
    /// Class SequenceItemWriter.
    /// </summary>
    public class SequenceItemWriter
    {
        /// <summary>
        /// The _end of sequence item_ le
        /// </summary>
        private static readonly byte[] _endOfSequenceItem_LE = { 0xFE, 0xFF, 0x0D, 0xE0, 0x00, 0x00, 0x00, 0x00 };
        /// <summary>
        /// The _end of sequence item_ be
        /// </summary>
        private static byte[] _endOfSequenceItem_BE = { 0xFF, 0xFE, 0xE0, 0x0D, 0x00, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Writes the items little endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="items">The items.</param>
        public static void WriteItemsLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings,
            List<DICOMObject> items)
        {
            dw.Write(WriteItemsLittleEndian(settings, items));
        }

        /// <summary>
        /// Writes the items little endian.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="items">The items.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] WriteItemsLittleEndian(DICOMWriteSettings settings, List<DICOMObject> items)
        {
            byte[] allItemBytes;
            using (var stream = new MemoryStream())
            {
                using (var itemDw = new DICOMBinaryWriter(stream))
                {
                    foreach (DICOMObject d in items)
                    {
                        WriteItemLittleEndian(itemDw, settings, d);
                    }
                }

                allItemBytes = stream.ToArray();
            }
            return allItemBytes;
        }

        /// <summary>
        /// Writes the item little endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="d">The d.</param>
        public static void WriteItemLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d)
        {
            DICOMTagWriter.WriteLittleEndian(dw, TagHelper.SEQUENCE_ITEM);
            using (var stream = new MemoryStream())
            {
                using (var itemDw = new DICOMBinaryWriter(stream))
                {
                    DICOMObjectWriter.Write(itemDw, settings, d, true);
                    if (!settings.DoWriteIndefiniteSequences)
                    {
                        LengthWriter.Write(dw, VR.Null, settings, (int)stream.Length);
                        dw.Write(stream.ToArray());
                    }
                    else
                    {
                        WriteIndefiniteLittleEndian(dw, stream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Writes the indefinite little endian.
        /// </summary>
        /// <param name="dw">The dw.</param>
        /// <param name="itemBytes">The item bytes.</param>
        private static void WriteIndefiniteLittleEndian(DICOMBinaryWriter dw, byte[] itemBytes)
        {
            dw.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            dw.Write(itemBytes);
            dw.Write(_endOfSequenceItem_LE);
        }
    }
}