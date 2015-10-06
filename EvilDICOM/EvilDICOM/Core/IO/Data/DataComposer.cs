using System.Text;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

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
                case VR.SignedLong:
                    var sl = el as SignedLong;
                    return LittleEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.SignedShort:
                    var sis = el as SignedShort;
                    return LittleEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    var uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    var ul = el as UnsignedLong;
                    return LittleEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.UnsignedShort:
                    var ush = el as UnsignedShort;
                    return LittleEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default:
                    return GetStringBytes(vr, el);
            }
        }

        public static byte[] GetDataBigEndian(IDICOMElement el)
        {
            VR vr = VRDictionary.GetVRFromType(el);
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
                case VR.OtherWordString:
                    var ows = el as OtherWordString;
                    return ows.DataContainer.MultipicityValue.ToArray();
                case VR.SignedLong:
                    var sl = el as SignedLong;
                    return BigEndianWriter.WriteSignedLong(sl.DataContainer);
                case VR.SignedShort:
                    var sis = el as SignedShort;
                    return BigEndianWriter.WriteSignedShort(sis.DataContainer);
                case VR.Unknown:
                    var uk = el as Unknown;
                    return DataRestriction.EnforceEvenLength(uk.DataContainer.MultipicityValue.ToArray(), vr);
                case VR.UnsignedLong:
                    var ul = el as UnsignedLong;
                    return BigEndianWriter.WriteUnsignedLong(ul.DataContainer);
                case VR.UnsignedShort:
                    var ush = el as UnsignedShort;
                    return BigEndianWriter.WriteUnsignedShort(ush.DataContainer);
                default:
                    return GetStringBytes(vr, el);
            }
        }

        public static byte[] GetStringBytes(VR vr, IDICOMElement el)
        {
            string data;
            byte[] unpadded;
            switch (vr)
            {
                case VR.AgeString:
                    var age = el as AgeString;
                    data = age.DataContainer.SingleValue;
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ApplicationEntity:
                    var ae = el as ApplicationEntity;
                    unpadded = GetASCIIBytes(ae.DataContainer.SingleValue);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.CodeString:
                    var cs = el as CodeString;
                    unpadded = GetASCIIBytes(cs.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Date:
                    var d = el as Date;
                    data = StringDataComposer.ComposeDate(d.Data);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DateTime:
                    var dt = el as DateTime;
                    data = StringDataComposer.ComposeDateTime(dt.DataContainer.SingleValue);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.DecimalString:
                    var ds = el as DecimalString;
                    data = StringDataComposer.ComposeDecimalString(ds.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.IntegerString:
                    var iSt = el as IntegerString;
                    data = StringDataComposer.ComposeIntegerString(iSt.DataContainer.MultipicityValue.ToArray());
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongString:
                    var ls = el as LongString;
                    unpadded = GetASCIIBytes(ls.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.LongText:
                    var lt = el as LongText;
                    unpadded = GetASCIIBytes(lt.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.PersonName:
                    var pn = el as PersonName;
                    unpadded = GetASCIIBytes(pn.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortString:
                    var ss = el as ShortString;
                    unpadded = GetASCIIBytes(ss.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.ShortText:
                    var st = el as ShortText;
                    unpadded = GetASCIIBytes(st.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.Time:
                    var t = el as Time;
                    data = StringDataComposer.ComposeTime(t.DataContainer.SingleValue);
                    unpadded = GetASCIIBytes(data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UnlimitedText:
                    var ut = el as UnlimitedText;
                    unpadded = GetASCIIBytes(ut.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                case VR.UniqueIdentifier:
                    var ui = el as UniqueIdentifier;
                    unpadded = GetASCIIBytes(ui.Data);
                    return DataRestriction.EnforceEvenLength(unpadded, vr);
                default:
                    return null;
            }
        }

        public static byte[] GetASCIIBytes(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                return Encoding.UTF8.GetBytes(s);
            }
            return new byte[0];
        }
    }
}