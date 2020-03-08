#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.IO.Reading;
using System.Linq;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Responsible for building concrete elements from element pieces
    /// </summary>
    public class ElementFactory
    {
        /// <summary>
        ///     Generates a concrete element class from the VR, tag, data and syntax. Also directs the appropriate data
        ///     interpretation.
        /// </summary>
        /// <param name="tag">the tag of the element to be generated</param>
        /// <param name="vr">the VR of the element to be generated</param>
        /// <param name="data">the raw data to be procesed (byte array)</param>
        /// <param name="syntax">the transfer syntax by which to interepret the data</param>
        /// <returns>a concrete DICOM element that uses the interface IDICOMElement</returns>
        public static IDICOMElement GenerateElement(Tag tag, VR vr, object data, TransferSyntax syntax, StringEncoding enc)
        {
            //HANDLE NUMBERS
            if (syntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
                switch (vr)
                {
                    case VR.AttributeTag:
                        return new AttributeTag(tag, BigEndianReader.ReadTag(data as byte[]));
                    case VR.FloatingPointDouble:
                        return new FloatingPointDouble(tag, BigEndianReader.ReadDoublePrecision(data as byte[]));
                    case VR.FloatingPointSingle:
                        return new FloatingPointSingle(tag, BigEndianReader.ReadSinglePrecision(data as byte[]));
                    case VR.SignedLong:
                        return new SignedLong(tag, BigEndianReader.ReadSignedLong(data as byte[]));
                    case VR.Signed64BitVeryLong:
                        return new Signed64bitVeryLong(tag, BigEndianReader.ReadSignedVeryLong(data as byte[]));
                    case VR.SignedShort:
                        return new SignedShort(tag, BigEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong:
                        return new UnsignedLong(tag, BigEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.Unsigned64BitVeryLong:
                        return new Unsigned64bitVeryLong(tag, BigEndianReader.ReadUnsignedVeryLong(data as byte[]));
                    case VR.UnsignedShort:
                        return new UnsignedShort(tag, BigEndianReader.ReadUnsignedShort(data as byte[]));
                }
            else
                switch (vr)
                {
                    case VR.AttributeTag:
                        return new AttributeTag(tag, LittleEndianReader.ReadTag(data as byte[]));
                    case VR.FloatingPointDouble:
                        return new FloatingPointDouble(tag, LittleEndianReader.ReadDoublePrecision(data as byte[]));
                    case VR.FloatingPointSingle:
                        return new FloatingPointSingle(tag, LittleEndianReader.ReadSinglePrecision(data as byte[]));
                    case VR.SignedLong:
                        return new SignedLong(tag, LittleEndianReader.ReadSignedLong(data as byte[]));
                    case VR.Signed64BitVeryLong:
                        return new Signed64bitVeryLong(tag, LittleEndianReader.ReadSignedVeryLong(data as byte[]));
                    case VR.SignedShort:
                        return new SignedShort(tag, LittleEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong:
                        return new UnsignedLong(tag, LittleEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.Unsigned64BitVeryLong:
                        return new Unsigned64bitVeryLong(tag, LittleEndianReader.ReadUnsignedVeryLong(data as byte[]));
                    case VR.UnsignedShort:
                        return new UnsignedShort(tag, LittleEndianReader.ReadUnsignedShort(data as byte[]));
                }
            //HANDLE ALL OTHERS
            switch (vr)
            {
                //HANDLE STRINGS
                case VR.AgeString:
                case VR.ApplicationEntity:
                case VR.CodeString:
                case VR.Date:
                case VR.DateTime:
                case VR.DecimalString:
                case VR.IntegerString:
                case VR.LongString:
                case VR.LongText:
                case VR.PersonName:
                case VR.ShortString:
                case VR.ShortText:
                case VR.Time:
                case VR.UnlimitedCharacter:
                case VR.UnlimitedText:
                case VR.UniversalResourceId:
                case VR.UniqueIdentifier:
                    return ReadString(vr, tag, data, enc);

                //HANDLE BYTE DATA
                case VR.Sequence:
                    return new Sequence { Tag = tag, Items = SequenceReader.ReadItems(data as byte[], syntax, enc) };
                case VR.OtherByteString:
                    return new OtherByteString(tag, data as byte[]);
                case VR.OtherFloatString:
                    return new OtherFloatString(tag, data as byte[]);
                case VR.OtherWordString:
                    return new OtherWordString(tag, data as byte[]);
                case VR.OtherDoubleString:
                    return new OtherDoubleString(tag, data as byte[]);
                case VR.OtherLongString:
                    return new OtherLongString(tag, data as byte[]);
                case VR.Other64BitVeryLongString:
                    return new Other64BitVeryLong(tag, data as byte[]);
                default:
                    var unk = new Unknown(tag, data as byte[]);
                    unk.TransferSyntax = syntax;
                    return unk;
            }
        }


        /// <summary>
        ///     Reads string data and creates the appropriate DICOM element
        /// </summary>
        /// <param name="data">the string data as an object (fresh from the DICOM reader)</param>
        /// <param name="vr">the VR of the element to be generated</param>
        /// <returns>a concrete DICOM element that uses the interface IDICOMElement</returns>
        public static IDICOMElement ReadString(VR vr, Tag tag, object data, StringEncoding enc)
        {
            switch (vr)
            {
                case VR.AgeString:
                    return new AgeString(tag, DICOMString.Read(data as byte[], enc));
                case VR.ApplicationEntity:
                    return new ApplicationEntity(tag, DICOMString.Read(data as byte[], enc));
                case VR.CodeString:
                    return new CodeString() { Tag = tag, Data_ = DICOMString.ReadMultiple(data as byte[], enc) };
                case VR.Date:
                    var dateData = DICOMString.Read(data as byte[], enc);
                    if (dateData.Any(b => b == '-')) // Range ('-')
                    {
                        var parts = dateData.Split('-');
                        var dates = parts.Select(p => StringDataParser.ParseDate(p)).OrderBy(p => p).ToArray();
                        return new Date(tag, dates) { IsRange = true };
                    }
                    else
                    {
                        return new Date(tag, dateData);
                    }
                case VR.DateTime:
                    var dateTimeData = DICOMString.Read(data as byte[], enc);
                    if (dateTimeData.Any(b => b == '-')) // Range ('-')
                    {
                        var parts = dateTimeData.Split('-');
                        var dates = parts.Select(p => StringDataParser.ParseDateTime(p)).OrderBy(p => p).ToArray();
                        return new DateTime(tag, dates) { IsRange = true };
                    }
                    else
                    {
                        return new DateTime(tag, dateTimeData);
                    }

                case VR.DecimalString:
                    return new DecimalString(tag, DICOMString.Read(data as byte[], enc));
                case VR.IntegerString:
                    return new IntegerString(tag, DICOMString.Read(data as byte[], enc));
                case VR.LongString:
                    return new LongString(tag, DICOMString.Read(data as byte[], enc));
                case VR.LongText:
                    return new LongText() { Tag = tag, Data_ = DICOMString.ReadMultiple(data as byte[], enc) };
                case VR.PersonName:
                    return new PersonName() { Tag = tag, Data_ = DICOMString.ReadMultiple(data as byte[], enc) };
                case VR.ShortString:
                    return new ShortString() { Tag = tag, Data_ = DICOMString.ReadMultiple(data as byte[], enc) };
                case VR.ShortText:
                    return new ShortText(tag, DICOMString.Read(data as byte[], enc));
                case VR.Time:
                    var timeData = DICOMString.Read(data as byte[], enc);
                    if (timeData.Any(b => b == '-')) // Range ('-')
                    {
                        var parts = timeData.Split('-');
                        var dates = parts.Select(p => StringDataParser.ParseTime(p)).OrderBy(p => p).ToArray();
                        return new Time(tag, dates) { IsRange = true };
                    }
                    else
                    {
                        return new Time(tag, timeData);
                    }
                case VR.UnlimitedCharacter:
                    return new UnlimitedCharacter() { Tag = tag, Data_ = DICOMString.ReadMultiple(data as byte[], enc) };
                case VR.UnlimitedText:
                    return new UnlimitedText(tag, DICOMString.Read(data as byte[], enc));
                case VR.UniqueIdentifier:
                    return new UniqueIdentifier(tag, DICOMString.Read(data as byte[], enc));
                case VR.UniversalResourceId:
                    return new UniversalResourceId(tag, DICOMString.Read(data as byte[], enc));
                default:
                    return new Unknown(tag, data as byte[]);
            }
        }
    }
}