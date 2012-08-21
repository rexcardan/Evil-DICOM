using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Reading;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.Element
{
    public class ElementFactory
    {
        public static IDICOMElement GenerateElement(AbstractElement d, object data,TransferSyntax syntax)
        {
            //HANDLE NUMBERS
            if (syntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
                switch (d.VR)
                {
                    case VR.AttributeTag: return new AttributeTag(d.Tag, BigEndianReader.ReadTag(data as byte[]));
                    case VR.FloatingPointDouble: return new FloatingPointDouble(d.Tag, BigEndianReader.ReadDoublePrecision(data as byte[]));
                    case VR.FloatingPointSingle: return new FloatingPointSingle(d.Tag, BigEndianReader.ReadSinglePrecision(data as byte[]));
                    case VR.SignedLong: return new SignedLong(d.Tag, BigEndianReader.ReadSignedLong(data as byte[]));
                    case VR.SignedShort: return new SignedShort(d.Tag, BigEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong: return new UnsignedLong(d.Tag, BigEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.UnsignedShort: return new UnsignedShort(d.Tag, BigEndianReader.ReadUnsignedShort(data as byte[]));
                }
            }
            else
            {
                switch (d.VR)
                {
                    case VR.AttributeTag: return new AttributeTag(d.Tag, LittleEndianReader.ReadTag(data as byte[]));
                    case VR.FloatingPointDouble: return new FloatingPointDouble(d.Tag, LittleEndianReader.ReadDoublePrecision(data as byte[]));
                    case VR.FloatingPointSingle: return new FloatingPointSingle(d.Tag, LittleEndianReader.ReadSinglePrecision(data as byte[]));
                    case VR.SignedLong: return new SignedLong(d.Tag, LittleEndianReader.ReadSignedLong(data as byte[]));
                    case VR.SignedShort: return new SignedShort(d.Tag, LittleEndianReader.ReadSignedShort(data as byte[]));
                    case VR.UnsignedLong: return new UnsignedLong(d.Tag, LittleEndianReader.ReadUnsignedLong(data as byte[]));
                    case VR.UnsignedShort: return new UnsignedShort(d.Tag, LittleEndianReader.ReadUnsignedShort(data as byte[]));
                }
            }  
           //HANDLE ALL OTHERS
            switch (d.VR)
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
                    return ReadString(d, data);

                //HANDLE BYTE DATA
                case VR.Sequence:
                    return new Sequence { Tag = d.Tag, Items = SequenceReader.ReadItems(data as byte[], syntax) };
                case VR.OtherByteString:
                    return new OtherByteString(d.Tag, data as byte[]);
                case VR.OtherFloatString:
                    return new OtherFloatString(d.Tag, data as byte[]);
                case VR.OtherWordString:
                    return new OtherWordString(d.Tag, data as byte[]);                  
                default:
                    return new Unknown(d.Tag, data as byte[]);               
            }
        }

        /// <summary>
        /// Reads string data and creates the appropriate DICOM element
        /// </summary>
        /// <param name="d">an abstract element holding the tag of the object</param>
        /// <param name="data">the string data as an object (fresh from the DICOM reader)</param>
        /// <param name="vr"></param>
        /// <returns></returns>
        private static IDICOMElement ReadString(AbstractElement d, object data)
        {
            switch (d.VR)
            {
                case VR.AgeString:
                    return new AgeString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.ApplicationEntity:
                    return new ApplicationEntity(d.Tag, DICOMString.Read(data as byte[]));
                case VR.CodeString:
                    return new CodeString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.Date:
                    return new Date(d.Tag, DICOMString.Read(data as byte[]));
                case VR.DateTime:
                    return new DateTime(d.Tag, DICOMString.Read(data as byte[]));
                case VR.DecimalString:
                    return new DecimalString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.IntegerString:
                    return new IntegerString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.LongString:
                    return new LongString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.LongText:
                    return new LongText(d.Tag, DICOMString.Read(data as byte[]));
                case VR.PersonName:
                    return new PersonName(d.Tag, DICOMString.Read(data as byte[]));
                case VR.ShortString:
                    return new ShortString(d.Tag, DICOMString.Read(data as byte[]));
                case VR.ShortText:
                    return new ShortText(d.Tag, DICOMString.Read(data as byte[]));
                case VR.Time:
                    return new Time(d.Tag, DICOMString.Read(data as byte[]));
                case VR.UnlimitedText:
                    return new UnlimitedText(d.Tag, DICOMString.Read(data as byte[]));
                case VR.UniqueIdentifier:
                    return new UniqueIdentifier(d.Tag, DICOMString.Read(data as byte[]));
                default:
                    return new Unknown(d.Tag, data as byte[]);
            }
        }
    }
}
