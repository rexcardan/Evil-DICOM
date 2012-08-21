using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Element;
using EvilDICOM.Core;

namespace EvilDICOM.Core.Extensions
{
    public static class IDICOMElementExtensions
    {
        public static bool IsVR(this IDICOMElement elem, VR vr){
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
                    return elem is EvilDICOM.Core.Element.DateTime;
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
                default: return elem is AbstractElement;
            }
        }
    }
}
