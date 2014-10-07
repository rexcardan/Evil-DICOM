using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Element;
using System.Reflection;
using EvilDICOM.Core.IO.Writing;

namespace EvilDICOM.Core.IO.Data
{
    public class DataComposer
    {
        public static byte[] GetDataLittleEndian(IDICOMElement el)
        {
            VR vr = VRDictionary.GetVRFromType(el);
            switch (vr)
            {
                case VR.AttributeTag:
                    AttributeTag at = el as AttributeTag;
                    return LittleEndianWriter.WriteTag(at.DataContainer);
                case VR.FloatingPointDouble:
                    FloatingPointDouble fpd = el as FloatingPointDouble;
                    return LittleEndianWriter.WriteDoublePrecision(fpd.DataContainer);
                case VR.FloatingPointSingle:
                    FloatingPointSingle fps = el as FloatingPointSingle;
                    return LittleEndianWriter.WriteSinglePrecision(fps.DataContainer);
                case VR.OtherByteString:
                    OtherByteString obs = el as OtherByteString;
                    return DataRestriction.EnforceEvenLength(obs.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.OtherFloatString:
                    OtherFloatString ofs = el as OtherFloatString;
                    return ofs.DataContainer.MultipicityValue.ToArray();
                case VR.OtherWordString:
                    OtherWordString ows = el as OtherWordString;
                    return ows.DataContainer.MultipicityValue.ToArray();
                case VR.SignedLong:
                    SignedLong sl = el as SignedLong;
                    return LittleEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.SignedShort:
                    SignedShort sis = el as SignedShort;
                    return LittleEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    Unknown uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    UnsignedLong ul = el as UnsignedLong;
                    return LittleEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.UnsignedShort:
                    UnsignedShort ush = el as UnsignedShort;
                    return LittleEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default: return GetStringBytes(vr, el);
            }
        }

        public static byte[] GetDataBigEndian(IDICOMElement el)
        {
            VR vr = VRDictionary.GetVRFromType(el);
            switch (vr)
            {
                case VR.AttributeTag:
                    AttributeTag at = el as AttributeTag;
                    return BigEndianWriter.WriteTag(at.DataContainer);
                case VR.FloatingPointDouble:
                    FloatingPointDouble fpd = el as FloatingPointDouble;
                    return BigEndianWriter.WriteDoublePrecision(fpd.DataContainer);
                case VR.FloatingPointSingle:
                    FloatingPointSingle fps = el as FloatingPointSingle;
                    return BigEndianWriter.WriteSinglePrecision(fps.DataContainer);
                case VR.OtherByteString:
                    OtherByteString obs = el as OtherByteString;
                    return DataRestriction.EnforceEvenLength(obs.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.OtherFloatString:
                    OtherFloatString ofs = el as OtherFloatString;
                    return ofs.DataContainer.MultipicityValue.ToArray();
                case VR.OtherWordString:
                    OtherWordString ows = el as OtherWordString;
                    return ows.DataContainer.MultipicityValue.ToArray();
                case VR.SignedLong:
                    SignedLong sl = el as SignedLong;
                    return BigEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.SignedShort:
                    SignedShort sis = el as SignedShort;
                    return BigEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    Unknown uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    UnsignedLong ul = el as UnsignedLong;
                    return BigEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.UnsignedShort:
                    UnsignedShort ush = el as UnsignedShort;
                    return BigEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default: return GetStringBytes(vr, el);
            }
        }

        public static byte[] GetStringBytes(VR vr, IDICOMElement el)
        {
            string data;
            byte[] unpadded;
            switch (vr)
            {
                case VR.AgeString:
                    AgeString age = el as AgeString;
                    data = age.DataContainer.SingleValue;
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ApplicationEntity:
                    ApplicationEntity ae = el as ApplicationEntity;
                    unpadded = GetASCIIBytes(ae.DataContainer.SingleValue);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.CodeString:
                    CodeString cs = el as CodeString;
                    unpadded = GetASCIIBytes(cs.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Date:
                    Date d = el as Date;
                    data = StringDataComposer.ComposeDate(d.Data);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DateTime:
                    EvilDICOM.Core.Element.DateTime dt = el as EvilDICOM.Core.Element.DateTime;
                    data = StringDataComposer.ComposeDateTime(dt.DataContainer.SingleValue);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DecimalString:
                    DecimalString ds = el as DecimalString;
                    data = StringDataComposer.ComposeDecimalString(ds.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.IntegerString:
                    IntegerString iSt = el as IntegerString;
                    data = StringDataComposer.ComposeIntegerString(iSt.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongString:
                    LongString ls = el as LongString;
                    unpadded = GetASCIIBytes(ls.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongText:
                    LongText lt = el as LongText;
                    unpadded = GetASCIIBytes(lt.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.PersonName:
                    PersonName pn = el as PersonName;
                    unpadded = GetASCIIBytes(pn.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortString:
                    ShortString ss = el as ShortString;
                    unpadded = GetASCIIBytes(ss.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortText:
                    ShortText st = el as ShortText;
                    unpadded = GetASCIIBytes(st.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Time:
                    Time t = el as Time;
                    data = StringDataComposer.ComposeTime(t.DataContainer.SingleValue);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UnlimitedText:
                    UnlimitedText ut = el as UnlimitedText;
                    unpadded = GetASCIIBytes(ut.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UniqueIdentifier:
                    UniqueIdentifier ui = el as UniqueIdentifier;
                    unpadded = GetASCIIBytes(ui.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                default: return null;
            }
        }

        public static byte[] GetASCIIBytes(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return new ASCIIEncoding().GetBytes(s);
            }
            else
            {
                return new byte[0];
            }
        }

    }
}
