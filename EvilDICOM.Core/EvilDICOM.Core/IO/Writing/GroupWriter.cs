using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Element;
using System.IO;

namespace EvilDICOM.Core.IO.Writing
{
    public class GroupWriter
    {
        public static int WriteGroupLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d, IDICOMElement el)
        {
            byte[] groupBytes = WriteGroupBytesLittleEndian(d, settings, el.Tag.Group);
            int length = groupBytes.Length;
            UnsignedLong ul = el as UnsignedLong;
            ul.Data = new uint[]{(uint)length};
            DICOMElementWriter.WriteLittleEndian(dw, settings, ul);
            dw.Write(groupBytes);
            return d.Elements.Where(elm => elm.Tag.Group == ul.Tag.Group).ToList().Count - 1;
        }

        private static byte[] WriteGroupBytesLittleEndian(DICOMObject d, DICOMWriteSettings settings, string groupID)
        {
            List<IDICOMElement> groupElements = d.Elements.Where(el => el.Tag.Group == groupID).ToList();
            byte[] groupBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                using (DICOMBinaryWriter groupDW = new DICOMBinaryWriter(stream))
                {
                    foreach (IDICOMElement el in groupElements)
                    {
                        if (!IsGroupHeader(el))
                        {
                            DICOMElementWriter.WriteLittleEndian(groupDW, settings, el);
                        }
                    }
                }
                groupBytes = stream.ToArray();
            }
            return groupBytes;
        }

        public static bool IsGroupHeader(IDICOMElement el)
        {
            return el.Tag.Element == "0000";
        }
    }
}
