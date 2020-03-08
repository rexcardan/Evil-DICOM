#region

using EvilDICOM.Core.Element;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Image;
using System.Collections.Generic;
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
                .ForEach(e =>
                {
                    dcm.Elements.Remove(e);
                });
        }

        #region IMAGE PROPERTIES

        /// <summary>
        ///     Grabs the pixel data bytes and sends it as a stream. Returns null if no pixel data element is found.
        /// </summary>
        public static PixelStream GetPixelStream(this DICOMObject dcm)
        {

            var pixelData = dcm.FindFirst(TagHelper.Pixel​Data) as AbstractElement<byte>;
            if (pixelData != null)
                return new PixelStream(pixelData.DataContainer.MultipicityValue.ToArray());
            return null;
        }

        public static void SetPixelStream(this DICOMObject dcm, IEnumerable<byte> value)
        {
            var pixelData = dcm.FindFirst(TagHelper.Pixel​Data) as AbstractElement<byte>;
            if (pixelData != null)
                pixelData.Data_ = value.ToArray().ToList();
        }

        #endregion
    }
}