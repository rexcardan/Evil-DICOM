using System;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.Dictionaries
{
    /// <summary>
    ///     General purpose class for working with VRs. It contains methods to convert ASCII string abbreviations into VR type,
    ///     the reverse,
    ///     and a few more useful methods when working with VR enums.
    /// </summary>
    public class VRDictionary
    {
        /// <summary>
        ///     Finds the VR type from an DICOM two letter abbreviation.
        /// </summary>
        /// <param name="vrAbbreviation">an DICOM two letter abbreviation</param>
        /// <returns>the VR type</returns>
        public static VR GetVRFromAbbreviation(string vrAbbreviation)
        {
            switch (vrAbbreviation)
            {
                case "CS":
                    return VR.CodeString;
                case "SH":
                    return VR.ShortString;
                case "LO":
                    return VR.LongString;
                case "ST":
                    return VR.ShortText;
                case "LT":
                    return VR.LongText;
                case "UT":
                    return VR.UnlimitedText;
                case "AE":
                    return VR.ApplicationEntity;
                case "PN":
                    return VR.PersonName;
                case "UI":
                    return VR.UniqueIdentifier;
                case "DA":
                    return VR.Date;
                case "TM":
                    return VR.Time;
                case "DT":
                    return VR.DateTime;
                case "AS":
                    return VR.AgeString;
                case "IS":
                    return VR.IntegerString;
                case "DS":
                    return VR.DecimalString;
                case "SS":
                    return VR.SignedShort;
                case "US":
                    return VR.UnsignedShort;
                case "SL":
                    return VR.SignedLong;
                case "UL":
                    return VR.UnsignedLong;
                case "AT":
                    return VR.AttributeTag;
                case "FL":
                    return VR.FloatingPointSingle;
                case "FD":
                    return VR.FloatingPointDouble;
                case "OB":
                    return VR.OtherByteString;
                case "OW":
                    return VR.OtherWordString;
                case "OF":
                    return VR.OtherFloatString;
                case "SQ":
                    return VR.Sequence;
                case "UN":
                    return VR.Unknown;
                default:
                    return VR.Null;
            }
        }

        /// <summary>
        ///     Creates the DICOM two letter abbreviation from a VR type.
        /// </summary>
        /// <param name="vr">the VR type</param>
        /// <returns>the DICOM two letter abbreviation</returns>
        public static string GetAbbreviationFromVR(VR vr)
        {
            switch (vr)
            {
                case VR.CodeString:
                    return "CS";
                case VR.ShortString:
                    return "SH";
                case VR.LongString:
                    return "LO";
                case VR.ShortText:
                    return "ST";
                case VR.LongText:
                    return "LT";
                case VR.UnlimitedText:
                    return "UT";
                case VR.ApplicationEntity:
                    return "AE";
                case VR.PersonName:
                    return "PN";
                case VR.UniqueIdentifier:
                    return "UI";
                case VR.Date:
                    return "DA";
                case VR.Time:
                    return "TM";
                case VR.DateTime:
                    return "DT";
                case VR.AgeString:
                    return "AS";
                case VR.IntegerString:
                    return "IS";
                case VR.DecimalString:
                    return "DS";
                case VR.SignedShort:
                    return "SS";
                case VR.UnsignedShort:
                    return "US";
                case VR.SignedLong:
                    return "SL";
                case VR.UnsignedLong:
                    return "UL";
                case VR.AttributeTag:
                    return "AT";
                case VR.FloatingPointSingle:
                    return "FL";
                case VR.FloatingPointDouble:
                    return "FD";
                case VR.OtherByteString:
                    return "OB";
                case VR.OtherWordString:
                    return "OW";
                case VR.OtherFloatString:
                    return "OF";
                case VR.Sequence:
                    return "SQ";
                case VR.Unknown:
                    return "UN";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        ///     Finds the VR enum from a specific DICOM element.
        /// </summary>
        /// <param name="el">the DICOM element</param>
        /// <returns>the VR type</returns>
        public static VR GetVRFromType(IDICOMElement el)
        {
            Type t = el.GetType();
            return EnumHelper.StringToEnum<VR>(t.Name);
        }

        /// <summary>
        ///     Finds the VR enum from a specific DICOM element type.
        /// </summary>
        /// <param name="el">the DICOM element</param>
        /// <returns>the VR type</returns>
        public static VR GetVRFromType(Type t)
        {
            return EnumHelper.StringToEnum<VR>(t.Name);
        }

        /// <summary>
        ///     Finds the VR enum from a specific DICOM element.
        /// </summary>
        /// <param name="el">the DICOM element</param>
        /// <returns>the VR abbreviation</returns>
        public static string GetAbbreviationFromType(IDICOMElement el)
        {
            Type t = el.GetType();
            var vr = EnumHelper.StringToEnum<VR>(t.Name);
            return GetAbbreviationFromVR(vr);
        }

        /// <summary>
        ///     Finds the VR enum from a specific DICOM element.
        /// </summary>
        /// <param name="el">the DICOM element</param>
        /// <returns>the VR abbreviation</returns>
        public static string GetAbbreviationFromType(Type t)
        {
            var vr = EnumHelper.StringToEnum<VR>(t.Name);
            return GetAbbreviationFromVR(vr);
        }

        /// <summary>
        ///     Determines the encoding, meaning how many bytes to write the VR and length parameters, from a VR type.
        ///     Options are explicit long (8 bytes), explicit short (4 bytes), or implicit (4 bytes). In Evil DICOM, the null
        ///     VR is used to represent an unknown VR (before dictionary lookup) that is implicitly encoded.
        /// </summary>
        /// <param name="vr">the VR type</param>
        /// <returns>the encoding method for this type</returns>
        public static VREncoding GetEncodingFromVR(VR vr)
        {
            switch (vr)
            {
                case VR.OtherByteString:
                case VR.OtherWordString:
                case VR.OtherFloatString:
                case VR.Sequence:
                case VR.Unknown:
                    return VREncoding.ExplicitLong;
                case VR.Null:
                    return VREncoding.Implicit;
                default:
                    return VREncoding.ExplicitShort;
            }
        }
    }
}