#region

using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMObjectWriter
    {
        public static bool IsFileMetaGroup(IDICOMElement el)
        {
            return el.Tag.Group == "0002";
        }

        public static void Write(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d,
            bool isSequenceItem = false)
        {
            for (var i = 0; i < d.Elements.Count; i++)
            {
                var el = d.Elements[i];
                if (!isSequenceItem) TransferSyntaxHelper.SetSyntax(d, settings.TransferSyntax);
                var currentSettings = IsFileMetaGroup(el) ? settings.GetFileMetaSettings() : settings;
                if (GroupWriter.IsGroupHeader(el))
                {
                    var skip = GroupWriter.WriteGroup(dw, currentSettings, d, el);
                    i += skip;
                }
                else
                {
                    DICOMElementWriter.Write(dw, currentSettings, el);
                }
            }
        }

        /// <summary>
        /// Ignores the rule of writing metadata in explicit VR little endian and instead writes all elements with the passed in syntax
        /// </summary>
        /// <param name="dw"></param>
        /// <param name="settings"></param>
        /// <param name="d"></param>
        /// <param name="isSequenceItem"></param>
        public static void WriteSameSyntax(DICOMBinaryWriter dw, DICOMWriteSettings settings, DICOMObject d,
            bool isSequenceItem = false)
        {
            for (var i = 0; i < d.Elements.Count; i++)
            {
                var el = d.Elements[i];
                if (!isSequenceItem) TransferSyntaxHelper.SetSyntax(d, settings.TransferSyntax);
                if (GroupWriter.IsGroupHeader(el))
                {
                    var skip = GroupWriter.WriteGroup(dw, settings, d, el);
                    i += skip;
                }
                else
                {
                    DICOMElementWriter.Write(dw, settings, el);
                }
            }
        }
    }
}