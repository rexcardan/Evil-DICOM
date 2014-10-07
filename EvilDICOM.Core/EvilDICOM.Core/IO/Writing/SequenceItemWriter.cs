using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Writing
{
    public class SequenceItemWriter
    {
        private static byte[] _endOfSequenceItem_LE = new byte[] { 0xFE, 0xFF, 0x0D, 0xE0, 0x00, 0x00, 0x00, 0x00 };
        private static byte[] _endOfSequenceItem_BE = new byte[] { 0xFF, 0xFE, 0xE0, 0x0D, 0x00, 0x00, 0x00, 0x00 };

        public static void WriteItemsLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, List<DICOMObject> items)
        {
            dw.Write(WriteItemsLittleEndian(settings, items));
        }

        public static byte[] WriteItemsLittleEndian(DICOMWriteSettings settings, List<DICOMObject> items)
        {
            byte[] allItemBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                using (DICOMBinaryWriter itemDw = new DICOMBinaryWriter(stream))
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

        public static void WriteItemLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d)
        {
            DICOMTagWriter.WriteLittleEndian(dw, TagHelper.SEQUENCE_ITEM);
            using (MemoryStream stream = new MemoryStream())
            {
                using (DICOMBinaryWriter itemDw = new DICOMBinaryWriter(stream))
                {
                    DICOMObjectWriter.Write(itemDw, settings, d);
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

        private static void WriteIndefiniteLittleEndian(DICOMBinaryWriter dw, byte[] itemBytes)
        {
            dw.Write(new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });
            dw.Write(itemBytes);
            dw.Write(_endOfSequenceItem_LE);
        }



    }
}
