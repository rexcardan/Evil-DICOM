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
    }
}