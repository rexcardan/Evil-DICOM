using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenerator
{
    public class EntryParser
    {
        /// <summary>
        /// Gets the class name and generates an input parameter for the data type of this element
        /// </summary>
        /// <param name="g"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        internal static (string, SyntaxNode) Parse(SyntaxGenerator g, DictionaryData entry)
        {
            var readVr = new string(entry.VR.Replace("OR", "").Take(2).ToArray());
            if (readVr != "Se" && !string.IsNullOrEmpty(entry.Keyword))
            {
                var cName = GetVRFromAbbreviation(readVr).ToString();
                Type dataType = null;


                var instance = (IDICOMElement)(Activator.CreateInstance(typeof(DICOMObject).Assembly.FullName, $"EvilDICOM.Core.Element.{cName}").Unwrap());
                cName = cName == "DateTime" ? "Element.DateTime" : cName;
                dataType = instance.DatType;

                //Hack to make sure we are in right namespace - EvilDICOM has DateTime VR and so does system
                var dataTypeName = dataType.Name.StartsWith("Nullable") ? "System.DateTime?" : dataType.Name;

                //Initialize strings as empty string instead of null
                var parameter = g.ParameterDeclaration("data", g.IdentifierName($"params {dataTypeName}[]"), null, RefKind.None);

                return (cName, parameter);
            }
            return (null, null);
        }

        internal static Type ParseDataType(DictionaryData entry)
        {
            var readVr = new string(entry.VR.Replace("OR", "").Take(2).ToArray());
            if (readVr != "Se" && !string.IsNullOrEmpty(entry.Keyword))
            {
                var cName = GetVRFromAbbreviation(readVr).ToString();
                Type dataType = null;


                var instance = (IDICOMElement)(Activator.CreateInstance(typeof(DICOMObject).Assembly.FullName, $"EvilDICOM.Core.Element.{cName}").Unwrap());
                cName = cName == "DateTime" ? "Element.DateTime" : cName;
                dataType = instance.DatType;
                return dataType;
            }
            return null;
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
                default:
                    return string.Empty;
            }
        }

        public enum VR
        {
            CodeString,
            ShortString,
            LongString,
            ShortText,
            LongText,
            UnlimitedCharacter,
            UnlimitedText,
            ApplicationEntity,
            PersonName,
            UniqueIdentifier,
            Date,
            Time,
            DateTime,
            AgeString,
            IntegerString,
            DecimalString,
            SignedShort,
            UnsignedShort,
            SignedLong,
            UnsignedLong,
            AttributeTag,
            FloatingPointSingle,
            FloatingPointDouble,
            OtherByteString,
            OtherWordString,
            OtherFloatString,
            Sequence,
            Unknown,
            UniversalResourceId,
            Null,
            OtherDoubleString,
            OtherLongString
        }

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
                default:
                    return VR.Null;
            }
        }
    }
}
