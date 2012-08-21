using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Enums
{
    public enum VR
    {
        CodeString,
        ShortString,
        LongString,
        ShortText,
        LongText,
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
        Null
    }
}
