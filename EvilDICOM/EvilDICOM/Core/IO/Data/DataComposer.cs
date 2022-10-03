#region

using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Data
{
    public class DataComposer
    {
        public static byte[] GetDataLittleEndian(IDICOMElement el, StringEncoding enc)
        {
            var vr = VRDictionary.GetVRFromType(el);
            switch (vr)
            {
                case VR.AttributeTag:
                    var at = el as AttributeTag;
                    return LittleEndianWriter.WriteTag(at.DataContainer);
                case VR.FloatingPointDouble:
                    var fpd = el as FloatingPointDouble;
                    return LittleEndianWriter.WriteDoublePrecision(fpd.DataContainer);
                case VR.FloatingPointSingle:
                    var fps = el as FloatingPointSingle;
                    return LittleEndianWriter.WriteSinglePrecision(fps.DataContainer);
                case VR.OtherByteString:
                    var obs = el as OtherByteString;
                    return DataRestriction.EnforceEvenLength(obs.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.OtherFloatString:
                    var ofs = el as OtherFloatString;
                    return ofs.DataContainer.MultipicityValue.ToArray();
                case VR.OtherWordString:
                    var ows = el as OtherWordString;
                    return ows.DataContainer.MultipicityValue.ToArray();
                case VR.Other64BitVeryLongString:
                    var ovs = el as Other64BitVeryLong;
                    return ovs.DataContainer.MultipicityValue.ToArray();
                case VR.SignedLong:
                    var sl = el as SignedLong;
                    return LittleEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.Signed64BitVeryLong:
                    var sv = el as Signed64bitVeryLong;
                    return LittleEndianWriter.WriteSignedVeryLong(sv.DataContainer);
                case VR.SignedShort:
                    var sis = el as SignedShort;
                    return LittleEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    var uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    var ul = el as UnsignedLong;
                    return LittleEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.Unsigned64BitVeryLong:
                    var uv = el as Unsigned64bitVeryLong;
                    return LittleEndianWriter.WriteUnsignedVeryLong(uv.DataContainer);
                case VR.UnsignedShort:
                    var ush = el as UnsignedShort;
                    return LittleEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default:
                    return GetStringBytes(vr, el, enc);
            }
        }

        public static byte[] GetDataBigEndian(IDICOMElement el, StringEncoding enc)
        {
            var vr = VRDictionary.GetVRFromType(el);
            switch (vr)
            {
                case VR.AttributeTag:
                    var at = el as AttributeTag;
                    return BigEndianWriter.WriteTag(at.DataContainer);
                case VR.FloatingPointDouble:
                    var fpd = el as FloatingPointDouble;
                    return BigEndianWriter.WriteDoublePrecision(fpd.DataContainer);
                case VR.FloatingPointSingle:
                    var fps = el as FloatingPointSingle;
                    return BigEndianWriter.WriteSinglePrecision(fps.DataContainer);
                case VR.OtherByteString:
                    var obs = el as OtherByteString;
                    return DataRestriction.EnforceEvenLength(obs.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.OtherFloatString:
                    var ofs = el as OtherFloatString;
                    return ofs.DataContainer.MultipicityValue.ToArray();
                case VR.OtherDoubleString:
                    var ods = el as OtherDoubleString;
                    return ods.DataContainer.MultipicityValue.ToArray();
                case VR.OtherLongString:
                    var ols = el as OtherLongString;
                    return ols.DataContainer.MultipicityValue.ToArray();
                case VR.Other64BitVeryLongString:
                    var ovs = el as Other64BitVeryLong;
                    return ovs.DataContainer.MultipicityValue.ToArray();
                case VR.OtherWordString:
                    var ows = el as OtherWordString;
                    return ows.DataContainer.MultipicityValue.ToArray();
                case VR.SignedLong:
                    var sl = el as SignedLong;
                    return BigEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.Signed64BitVeryLong:
                    var sv = el as Signed64bitVeryLong;
                    return BigEndianWriter.WriteSignedVeryLong(sv.DataContainer);
                case VR.SignedShort:
                    var sis = el as SignedShort;
                    return BigEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    var uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    var ul = el as UnsignedLong;
                    return BigEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.Unsigned64BitVeryLong:
                    var uv = el as Unsigned64bitVeryLong;
                    return BigEndianWriter.WriteUnsignedVeryLong(uv.DataContainer);
                case VR.UnsignedShort:
                    var ush = el as UnsignedShort;
                    return BigEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default:
                    return GetStringBytes(vr, el, enc);
            }
        }

        public static byte[] GetStringBytes(VR vr, IDICOMElement el, StringEncoding enc)
        {
            string data;
            byte[] unpadded;
            switch (vr)
            {
                case VR.AgeString:
                    var age = el as AgeString;
                    data = StringDataComposer.ComposeMultipleString(age.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ApplicationEntity:
                    var ae = el as ApplicationEntity;
                    data = StringDataComposer.ComposeMultipleString(ae.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.CodeString:
                    var cs = el as CodeString;
                    data = StringDataComposer.ComposeMultipleString(cs.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Date:
                    var d = el as Date;
                    data = StringDataComposer.ComposeDates(d.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DateTime:
                    var dt = el as EvilDICOM.Core.Element.DateTime;
                    data = StringDataComposer.ComposeDateTimes(dt.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DecimalString:
                    var ds = el as DecimalString;
                    data = StringDataComposer.ComposeDecimalString(ds.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.IntegerString:
                    var iSt = el as IntegerString;
                    data = StringDataComposer.ComposeIntegerString(iSt.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongString:
                    var ls = el as LongString;
                    data = StringDataComposer.ComposeMultipleString(ls.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongText:
                    var lt = el as LongText;
                    data = StringDataComposer.ComposeMultipleString(lt.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.PersonName:
                    var pn = el as PersonName;
                    data = StringDataComposer.ComposeMultipleString(pn.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortString:
                    var ss = el as ShortString;
                    data = StringDataComposer.ComposeMultipleString(ss.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortText:
                    var st = el as ShortText; // VM=1 ALWAYS
                    unpadded = GetEncodedBytes(st.Data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Time:
                    var t = el as Time;
                    data = StringDataComposer.ComposeTimes(t.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UnlimitedText:
                    var ut = el as UnlimitedText; // VM=1 ALWAYS
                    unpadded = GetEncodedBytes(ut.Data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UnlimitedCharacter:
                    var uc = el as UnlimitedCharacter;
                    data = StringDataComposer.ComposeMultipleString(uc.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UniqueIdentifier:
                    var ui = el as UniqueIdentifier;
                    data = StringDataComposer.ComposeMultipleString(ui.Data_);
                    unpadded = GetEncodedBytes(data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UniversalResourceId:
                    var uid = el as UniversalResourceId; // VM=1 ALWAYS
                    unpadded = GetEncodedBytes(uid.Data, enc);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                default:
                    return null;
            }
        }

        public static byte[] GetEncodedBytes(string s, StringEncoding enc)
        {
            if (!string.IsNullOrEmpty(s))
                return EncodingDictionary.GetEncodingFromISO(enc).GetBytes(s);
            return new byte[0];
        }
    }
}