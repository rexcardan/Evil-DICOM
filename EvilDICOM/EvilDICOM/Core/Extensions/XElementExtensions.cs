#region

using System;
using System.Linq;
using System.Xml.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.Extensions
{
    public static class XElementExtensions
    {
        public static IDICOMElement ToDICOMElement(this XElement xel)
        {
            if (xel.Name != "DICOMElement")
                throw new ArgumentException("XML element must be of type DICOMElement <DICOMElement ...");
            var attr = xel.Attributes();
            if (!attr.Any(a => a.Name == "VR")) throw new ArgumentException("XML element must have attribute 'VR'.");
            var vr = VRDictionary.GetVRFromAbbreviation(attr.First(a => a.Name == "VR").Value);
            if (!attr.Any(a => a.Name == "Tag")) throw new ArgumentException("XML element must have attribute 'Tag'.");
            var tag = new Tag(attr.First(a => a.Name == "Tag").Value);
            var data = xel.Elements("Data").Select(d => d.Value).ToArray();
            return ElementFactory.GenerateElementFromStringData(tag, vr, data);
        }
    }
}