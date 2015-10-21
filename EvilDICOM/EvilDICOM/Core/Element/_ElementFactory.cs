using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.IO.Reading;
using System.Linq;
using System;

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
        public static IDICOMElement GenerateElement(Tag tag, VR vr, object data, TransferSyntax syntax)
        {
            //HANDLE NUMBERS
            if (syntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
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
                    case VR.SignedShort:
                        return new SignedShort(tag, BigEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong:
                        return new UnsignedLong(tag, BigEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.UnsignedShort:
                        return new UnsignedShort(tag, BigEndianReader.ReadUnsignedShort(data as byte[]));
                }
            }
            else
            {
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
                    case VR.SignedShort:
                        return new SignedShort(tag, LittleEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong:
                        return new UnsignedLong(tag, LittleEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.UnsignedShort:
                        return new UnsignedShort(tag, LittleEndianReader.ReadUnsignedShort(data as byte[]));
                }
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
                case VR.UnlimitedText:
                case VR.UniqueIdentifier:
                    return ReadString(vr, tag, data);

                //HANDLE BYTE DATA
                case VR.Sequence:
                    return new Sequence { Tag = tag, Items = SequenceReader.ReadItems(data as byte[], syntax) };
                case VR.OtherByteString:
                    return new OtherByteString(tag, data as byte[]);
                case VR.OtherFloatString:
                    return new OtherFloatString(tag, data as byte[]);
                case VR.OtherWordString:
                    return new OtherWordString(tag, data as byte[]);
                default:
                    var unk = new Unknown(tag, data as byte[]);
                    unk.TransferSyntax = syntax;
                    return unk;
            }
        }


        /// <summary>
        ///     Generates a concrete element class from the VR, tag, data and string data (from XML). 
        /// </summary>
        /// <param name="tag">the tag of the element to be generated</param>
        /// <param name="vr">the VR of the element to be generated</param>
        /// <param name="data">the string data of the element</param>
        /// <param name="syntax">the transfer syntax by which to interepret the data</param>
        /// <returns>a concrete DICOM element that uses the interface IDICOMElement</returns>
        public static IDICOMElement GenerateElementFromStringData(Tag tag, VR vr, string[] data)
        {
            switch (vr)
            {
                case VR.AttributeTag:
                    return new AttributeTag(tag, new Tag(data.First()));
                case VR.FloatingPointDouble:
                    return new FloatingPointDouble(tag, data.Select(d => double.Parse(d)).ToArray());
                case VR.FloatingPointSingle:
                    return new FloatingPointSingle(tag, data.Select(d => float.Parse(d)).ToArray());
                case VR.SignedLong:
                    return new SignedLong(tag, data.Select(d => int.Parse(d)).ToArray());
                case VR.SignedShort:
                    return new SignedShort(tag, data.Select(d => short.Parse(d)).ToArray());
                case VR.UnsignedLong:
                    return new UnsignedLong(tag, data.Select(d => uint.Parse(d)).ToArray());
                case VR.UnsignedShort:
                    return new UnsignedShort(tag, data.Select(d => ushort.Parse(d)).ToArray());
                //HANDLE STRINGS
                case VR.AgeString: return new AgeString(tag, data.First());
                case VR.ApplicationEntity: return new ApplicationEntity(tag, data.First());
                case VR.CodeString: return new CodeString(tag, data.First());
                case VR.DecimalString: return new DecimalString(tag, data.Select(d => double.Parse(d)).ToArray());
                case VR.IntegerString: return new IntegerString(tag, data.Select(d => int.Parse(d)).ToArray());
                case VR.LongString: return new LongString(tag, data.First());
                case VR.LongText: return new LongText(tag, data.First());
                case VR.PersonName: return new PersonName(tag, data.First());
                case VR.ShortString: return new ShortString(tag, data.First());
                case VR.ShortText: return new ShortText(tag, data.First());
                case VR.UnlimitedText: return new UnlimitedText(tag, data.First());
                case VR.UniqueIdentifier: return new UniqueIdentifier(tag, data.First());
                //HANDLE DATES
                case VR.Date: return new Date(tag, StringDataParser.GetNullableDate(data.First()));
                case VR.DateTime: return new DateTime(tag, StringDataParser.GetNullableDate(data.First()));
                case VR.Time: return new Time(tag, StringDataParser.GetNullableDate(data.First()));
                //HANDLE BYTE DATA
                case VR.Sequence:
                    return new Sequence { Tag = tag, Items = data.Select(d => DICOMObject.FromXML(d)).ToList()};
                case VR.OtherByteString:
                    return new OtherByteString(tag, ByteHelper.HexStringToByteArray(data.First()));
                case VR.OtherFloatString:
                    return new OtherFloatString(tag, ByteHelper.HexStringToByteArray(data.First()));
                case VR.OtherWordString:
                    return new OtherWordString(tag, ByteHelper.HexStringToByteArray(data.First()));
                default:
                    return new Unknown(tag, ByteHelper.HexStringToByteArray(data.First()));
            }
        }

        /// <summary>
        ///     Reads string data and creates the appropriate DICOM element
        /// </summary>
        /// <param name="data">the string data as an object (fresh from the DICOM reader)</param>
        /// <param name="vr">the VR of the element to be generated</param>
        /// <returns>a concrete DICOM element that uses the interface IDICOMElement</returns>
        private static IDICOMElement ReadString(VR vr, Tag tag, object data)
        {
            switch (vr)
            {
                case VR.AgeString:
                    return new AgeString(tag, DICOMString.Read(data as byte[]));
                case VR.ApplicationEntity:
                    return new ApplicationEntity(tag, DICOMString.Read(data as byte[]));
                case VR.CodeString:
                    return new CodeString(tag, DICOMString.Read(data as byte[]));
                case VR.Date:
                    return new Date(tag, DICOMString.Read(data as byte[]));
                case VR.DateTime:
                    return new DateTime(tag, DICOMString.Read(data as byte[]));
                case VR.DecimalString:
                    return new DecimalString(tag, DICOMString.Read(data as byte[]));
                case VR.IntegerString:
                    return new IntegerString(tag, DICOMString.Read(data as byte[]));
                case VR.LongString:
                    return new LongString(tag, DICOMString.Read(data as byte[]));
                case VR.LongText:
                    return new LongText(tag, DICOMString.Read(data as byte[]));
                case VR.PersonName:
                    return new PersonName(tag, DICOMString.Read(data as byte[]));
                case VR.ShortString:
                    return new ShortString(tag, DICOMString.Read(data as byte[]));
                case VR.ShortText:
                    return new ShortText(tag, DICOMString.Read(data as byte[]));
                case VR.Time:
                    return new Time(tag, DICOMString.Read(data as byte[]));
                case VR.UnlimitedText:
                    return new UnlimitedText(tag, DICOMString.Read(data as byte[]));
                case VR.UniqueIdentifier:
                    return new UniqueIdentifier(tag, DICOMString.Read(data as byte[]));
                default:
                    return new Unknown(tag, data as byte[]);
            }
        }
    }
}