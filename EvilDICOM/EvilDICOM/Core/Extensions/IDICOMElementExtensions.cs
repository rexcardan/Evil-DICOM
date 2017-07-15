#region

using System.Collections.Generic;
using System.Xml.Linq;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.Extensions
{
    /// <summary>
    ///     Adds useful methods to the IDICOMElement interface
    /// </summary>
    public static class IDICOMElementExtensions
    {
        /// <summary>
        ///     Checks to see if a certain IDICOMElement is of a given VR type
        /// </summary>
        /// <param name="elem">the DICOM element in question</param>
        /// <param name="vr">the VR type to test the DICOM element</param>
        /// <returns>a boolean indicating whether or not the DICOM element is of a given VR type</returns>
        public static bool IsVR(this IDICOMElement elem, VR vr)
        {
            switch (vr)
            {
                case VR.CodeString:
                    return elem is CodeString;
                case VR.ShortString:
                    return elem is ShortString;
                case VR.LongString:
                    return elem is LongString;
                case VR.ShortText:
                    return elem is ShortText;
                case VR.LongText:
                    return elem is LongText;
                case VR.UnlimitedText:
                    return elem is UnlimitedText;
                case VR.ApplicationEntity:
                    return elem is ApplicationEntity;
                case VR.PersonName:
                    return elem is PersonName;
                case VR.UniqueIdentifier:
                    return elem is UniqueIdentifier;
                case VR.Date:
                    return elem is Date;
                case VR.Time:
                    return elem is Time;
                case VR.DateTime:
                    return elem is DateTime;
                case VR.AgeString:
                    return elem is AgeString;
                case VR.IntegerString:
                    return elem is IntegerString;
                case VR.DecimalString:
                    return elem is DecimalString;
                case VR.SignedShort:
                    return elem is SignedShort;
                case VR.UnsignedShort:
                    return elem is UnsignedShort;
                case VR.SignedLong:
                    return elem is SignedLong;
                case VR.UnsignedLong:
                    return elem is UnsignedLong;
                case VR.AttributeTag:
                    return elem is AttributeTag;
                case VR.FloatingPointSingle:
                    return elem is FloatingPointSingle;
                case VR.FloatingPointDouble:
                    return elem is FloatingPointDouble;
                case VR.OtherByteString:
                    return elem is OtherByteString;
                case VR.OtherWordString:
                    return elem is OtherWordString;
                case VR.OtherFloatString:
                    return elem is OtherFloatString;
                case VR.Sequence:
                    return elem is Sequence;
                case VR.Unknown:
                    return elem is Unknown;
                default:
                    return elem is AbstractElement<object>;
            }
        }

        public static XElement ToXElement(this IDICOMElement el)
        {
            var xel = new XElement("DICOMElement");
            xel.Add(new XAttribute("VR", VRDictionary.GetAbbreviationFromType(el)));
            xel.Add(new XAttribute("Tag", el.Tag.CompleteID));
            xel.Add(new XAttribute("Description", TagDictionary.GetDescription(el.Tag.CompleteID)));
            //Recursively add data if seq
            if (el.IsVR(VR.Sequence))
            {
                var seq = el as Sequence;
                for (var i = 0; i < seq.Items.Count; i++)
                {
                    var item = new XElement("Item");
                    foreach (var it in seq.Items[i].Elements)
                        item.Add(it.ToXElement());
                    xel.Add(item);
                }
            }
            else // Just add data
            {
                if (el.DatType != typeof(byte))
                {
                    foreach (var d in el.DData_)
                        xel.Add(new XElement("Data", d));
                }
                else
                {
                    //If data type is byte, write hex string
                    var array = (el.DData_ as List<byte>).ToArray();
                    var hex = ByteHelper.ByteArrayToHexString(array);
                    xel.Add(new XElement("Data", hex));
                }
            }
            return xel;
        }
    }
}