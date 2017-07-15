namespace EvilDICOM.Core.Enums
{
    /// <summary>
    ///     An enum that contains all of the possible DICOM VR types.
    /// </summary>
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
}