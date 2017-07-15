using System.Collections.Generic;
using System.IO;
using System.Linq;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Writing
{
    public class GroupWriter
    {
        public static bool IsGroupHeader(IDICOMElement el)
        {
            return el.Tag.Element == "0000";
        }

        public static int WriteGroup(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d, IDICOMElement el)
        {
            byte[] groupBytes = WriteGroupBytes(d, settings, el.Tag.Group);
            int length = groupBytes.Length;
            var ul = el as UnsignedLong;
            ul.SetData((uint) length);
            DICOMElementWriter.Write(dw, settings, ul);
            dw.Write(groupBytes);
            return d.Elements.Where(elm => elm.Tag.Group == ul.Tag.Group).ToList().Count - 1;
        }

        public static byte[] WriteGroupBytes(DICOMObject d, DICOMWriteSettings settings, string groupId)
        {
            List<IDICOMElement> groupElements = d.Elements.Where(el => el.Tag.Group == groupId).ToList();
            byte[] groupBytes;
            using (var stream = new MemoryStream())
            {
                using (var groupDW = new DICOMBinaryWriter(stream))
                {
                    foreach (IDICOMElement el in groupElements)
                    {
                        if (!IsGroupHeader(el))
                        {
                            DICOMElementWriter.Write(groupDW, settings, el);
                        }
                    }
                }
                groupBytes = stream.ToArray();
            }
            return groupBytes;
        }
    }
}