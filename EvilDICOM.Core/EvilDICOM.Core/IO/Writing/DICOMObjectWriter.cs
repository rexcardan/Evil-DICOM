using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using System.IO;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMObjectWriter
    {
        public static void WriteObjectLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d)
        {
            for (int i = 0; i < d.Elements.Count; i++)
            {
                IDICOMElement el = d.Elements[i];
                DICOMWriteSettings currentSettings = IsFileMetaGroup(el) ? settings.GetFileMetaSettings() : settings;
                if (GroupWriter.IsGroupHeader(el))
                {
                    int skip = GroupWriter.WriteGroupLittleEndian(dw, currentSettings, d, el);
                    i += skip;
                }
                else
                {
                    DICOMElementWriter.WriteLittleEndian(dw, currentSettings, el);
                }
            }
        }

        public static bool IsFileMetaGroup(IDICOMElement el)
        {
            return el.Tag.Group == "0002";
        }
    }
}
