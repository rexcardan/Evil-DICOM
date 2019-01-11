using System;
using System.Diagnostics;
using System.Reflection;
using EvilDICOM.Core;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CodeGenCore
{
    public static class EntryParser
    {
        private static readonly Assembly EvilDicomAssembly;

        static EntryParser()
        {
            EvilDicomAssembly = Assembly.LoadFrom("EvilDICOM");
        }

        public static (string className, SyntaxNode node) Parse(this SyntaxGenerator g, DictionaryData entry)
        {
            if (entry.VR.StartsWith("See"))
                return (null, null);

            //var vr = entry.VR[0..2];
            var vr = entry.VR.Substring(0, 2);

            var className = GetVrFromAbbreviation(vr).ToString();

            var i = (IDICOMElement)EvilDicomAssembly.CreateInstance($"EvilDICOM.Core.Element.{className}");

            if (className == "DateTime")
                className = "Element.DateTime";

            var dataType = i.DatType;

            // hack to make sure we are in right namespace - EvilDICOM has DateTime VR and so does system
            var dataTypeName = dataType.Name.StartsWith("Nullable") 
                ? "System.DateTime?" 
                : dataType.Name;

            // initialize strings as empty string instead of null
            var parameter = g.ParameterDeclaration("data", g.IdentifierName($"params {dataTypeName}[]"));

            return (className, parameter);
        }

        public enum ValueRepresentation
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

        public static ValueRepresentation GetVrFromAbbreviation(string vrAbbreviation)
        {
            switch (vrAbbreviation)
            {
                case "CS":
                    return ValueRepresentation.CodeString;
                case "SH":
                    return ValueRepresentation.ShortString;
                case "LO":
                    return ValueRepresentation.LongString;
                case "ST":
                    return ValueRepresentation.ShortText;
                case "LT":
                    return ValueRepresentation.LongText;
                case "UC":
                    return ValueRepresentation.UnlimitedCharacter;
                case "UR":
                    return ValueRepresentation.UniversalResourceId;
                case "UT":
                    return ValueRepresentation.UnlimitedText;
                case "AE":
                    return ValueRepresentation.ApplicationEntity;
                case "PN":
                    return ValueRepresentation.PersonName;
                case "UI":
                    return ValueRepresentation.UniqueIdentifier;
                case "DA":
                    return ValueRepresentation.Date;
                case "TM":
                    return ValueRepresentation.Time;
                case "DT":
                    return ValueRepresentation.DateTime;
                case "AS":
                    return ValueRepresentation.AgeString;
                case "IS":
                    return ValueRepresentation.IntegerString;
                case "DS":
                    return ValueRepresentation.DecimalString;
                case "SS":
                    return ValueRepresentation.SignedShort;
                case "US":
                    return ValueRepresentation.UnsignedShort;
                case "SL":
                    return ValueRepresentation.SignedLong;
                case "UL":
                    return ValueRepresentation.UnsignedLong;
                case "AT":
                    return ValueRepresentation.AttributeTag;
                case "FL":
                    return ValueRepresentation.FloatingPointSingle;
                case "FD":
                    return ValueRepresentation.FloatingPointDouble;
                case "OB":
                    return ValueRepresentation.OtherByteString;
                case "OW":
                    return ValueRepresentation.OtherWordString;
                case "OF":
                    return ValueRepresentation.OtherFloatString;
                case "OD":
                    return ValueRepresentation.OtherDoubleString;
                case "OL":
                    return ValueRepresentation.OtherLongString;
                case "SQ":
                    return ValueRepresentation.Sequence;
                case "UN":
                    return ValueRepresentation.Unknown;
                default:
                    Trace.WriteLine($"yo what {vrAbbreviation}");
                    return ValueRepresentation.Null;
            }
        }
    }
}
