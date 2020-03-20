#region

using System;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

#endregion

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
                case "UC":
                    return VR.UnlimitedCharacter;
                case "UR":
                    return VR.UniversalResourceId;
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
                case "OD":
                    return VR.OtherDoubleString;
                case "OL":
                    return VR.OtherLongString;
                case "SQ":
                    return VR.Sequence;
                case "UN":
                    return VR.Unknown;

                case "OV":
                    return VR.Other64BitVeryLongString;
                case "SV":
                    return VR.Signed64BitVeryLong;
                case "UV":
                    return VR.Unsigned64BitVeryLong;

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
                case VR.UnlimitedCharacter:
                    return "UC";
                case VR.UniversalResourceId:
                    return "UR";
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
                case VR.OtherDoubleString:
                    return "OD";
                case VR.OtherLongString:
                    return "OL";
                case VR.Sequence:
                    return "SQ";
                case VR.Unknown:
                    return "UN";

                case VR.Other64BitVeryLongString:
                    return "OV";
                case VR.Signed64BitVeryLong:
                    return "SV";
                case VR.Unsigned64BitVeryLong:
                    return "UV";
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
            var t = el.GetType();
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
            var t = el.GetType();
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
                case VR.OtherDoubleString:
                case VR.OtherLongString:
                case VR.Other64BitVeryLongString:
                case VR.OtherWordString:
                case VR.Signed64BitVeryLong:
                case VR.Unsigned64BitVeryLong:
                case VR.OtherFloatString:
                case VR.UniversalResourceId:
                case VR.UnlimitedCharacter:
                case VR.UnlimitedText:
                case VR.Sequence:
                case VR.Unknown:
                    return VREncoding.ExplicitLong;
                case VR.Null:
                    return VREncoding.Implicit;
                default:
                    return VREncoding.ExplicitShort;
            }
        }

        public static Type GetDataTypeFromVR(VR vr)
        {
            switch (vr)
            {
                case VR.CodeString:
                case VR.ShortString:
                case VR.LongString:
                case VR.ShortText:
                case VR.LongText:
                case VR.UnlimitedCharacter:
                case VR.UniversalResourceId:
                case VR.UnlimitedText:
                case VR.ApplicationEntity:
                case VR.PersonName:
                case VR.UniqueIdentifier:
                case VR.AgeString:
                    return typeof(string);
                case VR.Date:
                case VR.Time:
                case VR.DateTime:
                    return typeof(System.DateTime?);
                case VR.IntegerString:
                    return typeof(int);
                case VR.DecimalString:
                    return typeof(double);
                case VR.SignedShort:
                    return typeof(short);
                case VR.UnsignedShort:
                    return typeof(ushort);
                case VR.SignedLong:
                    return typeof(int);
                case VR.UnsignedLong:
                    return typeof(uint);
                case VR.AttributeTag:
                    return typeof(Tag);
                case VR.FloatingPointSingle:
                    return typeof(float);
                case VR.FloatingPointDouble:
                    return typeof(double);
                case VR.OtherByteString:
                case VR.OtherWordString:
                case VR.OtherFloatString:
                case VR.OtherDoubleString:
                case VR.Other64BitVeryLongString:
                case VR.OtherLongString:
                    return typeof(byte);
                case VR.Sequence:
                    return typeof(DICOMObject);
                case VR.Signed64BitVeryLong:
                    return typeof(long);
                case VR.Unsigned64BitVeryLong:
                    return typeof(ulong);
                default:
                    return typeof(object);
            }
        }
    }
}