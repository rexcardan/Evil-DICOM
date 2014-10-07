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
        public static bool IsGroupHeader(IDICOMElement el)
        {
            return el.Tag.Element == "0000";
        }

        public static int WriteGroup(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d, IDICOMElement el)
        {
            byte[] groupBytes = WriteGroupBytes(d, settings, el.Tag.Group);
            int length = groupBytes.Length;
            UnsignedLong ul = el as UnsignedLong;
            ul.SetData((uint)length);
            DICOMElementWriter.Write(dw, settings, ul);
            dw.Write(groupBytes);
            return d.Elements.Where(elm => elm.Tag.Group == ul.Tag.Group).ToList().Count - 1;
        }

        public static byte[] WriteGroupBytes(DICOMObject d, DICOMWriteSettings settings, string groupId)
        {
            List<IDICOMElement> groupElements = d.Elements.Where(el => el.Tag.Group == groupId).ToList();
            byte[] groupBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                using (DICOMBinaryWriter groupDW = new DICOMBinaryWriter(stream))
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
