#region

using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#endregion

namespace EvilDICOM.Core.Extensions
{
    public static class DICOMObjectExtensions
    {
        /// <summary>
        /// Converts a DICOM object to XML format
        /// </summary>
        /// <param name="dcm"></param>
        /// <returns></returns>
        public static string ToXMLString(this DICOMObject dcm)
        {
            var els = dcm.Elements.Select(el => el.ToXElement()).ToList();
            var builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder))
            {
                var doc = new XDocument();
                var dcmEl = new XElement("DICOM");
                els.ForEach(dcmEl.Add);
                doc.Add(dcmEl);
                doc.Save(writer);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Removes all elements beginning with 0002
        /// </summary>
        /// <param name="dcm">DICOM object containing metadata header</param>
        public static void RemoveMetaHeader(this DICOMObject dcm)
        {
            string _metaGroup = "0002";
            dcm.Elements
                .Where(e => e.Tag.Group == _metaGroup)
                .ToList()
                .ForEach(e=>
                {
                    dcm.Elements.Remove(e);
                });
        }
    }
}