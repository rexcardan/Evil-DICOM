using EvilDICOM.Core.Enums;
using System.Collections.Generic;
using System.Text;

namespace EvilDICOM.Core.Dictionaries
{
    public class EncodingDictionary
    {
        public static Dictionary<string, string> encodingMap = new Dictionary<string, string>()
        {
            {"ISO IR 13","shift_jis"},
            {"ISO IR 100","iso-8859-1"},
            {"ISO IR 101","iso-8859-2"},
            {"ISO IR 109","iso-8859-3"},
            {"ISO IR 110","iso-8859-4"},
            {"ISO IR 144","iso-8859-5"},
            {"ISO IR 127","iso-8859-6"},
            {"ISO IR 126","iso-8859-7"},
            {"ISO IR 138","iso-8859-8"},
            {"ISO IR 148","iso-8859-9"},
            {"ISO 2022 IR 6","us-ascii"},
            {"ISO 2022 IR 13","iso-2022-jp"},
            {"ISO 2022 IR 87","iso-2022-jp"},
            {"ISO 2022 IR 100","iso-8859-1"},
            {"ISO 2022 IR 101","iso-8859-2"},
            {"ISO 2022 IR 109","iso-8859-3"},
            {"ISO 2022 IR 110","iso-8859-4"},
            {"ISO 2022 IR 144","iso-8859-5"},
            {"ISO 2022 IR 127","iso-8859-6"},
            {"ISO 2022 IR 126","iso-8859-7"},
            {"ISO 2022 IR 138","iso-8859-8"},
            {"ISO 2022 IR 148","iso-8859-9"},
            {"ISO IR 166","windows-874"},
            {"ISO 2022 IR 166","windows-874"},
            {"ISO 2022 IR 149","x-cp20949"},
            {"ISO IR 192","utf-8"},
            {"GB18030","GB18030"},
        };

        public static Encoding GetEncodingFromISO(string isoStandard)
        {
            var iso = isoStandard.Replace("_", string.Empty).ToUpper();
            if (encodingMap.ContainsKey(iso)) { return Encoding.GetEncoding(encodingMap[iso]); }
            else return Encoding.UTF8;
        }

        public static Encoding GetEncodingFromISO(StringEncoding enc)
        {
            return GetEncodingFromISO(enc.ToString());
        }
    }
}
