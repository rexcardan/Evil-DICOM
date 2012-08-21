using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Data
{
    public class DataRestriction
    {
        public static string EnforceLengthRestriction(uint lengthLimit, string data)
        {
            if (data.Length > lengthLimit)
            {
                Console.Write(
                    string.Format("Not DICOM compliant. Attempted data input of {0} characters. Data size is limited to {1} characters. Read anyway.", data.Length, lengthLimit)
                    );
                return data;
            }
            else
            {
                return data;
            }
        }

        public static byte[] EnforceEvenLength(byte[] data, VR vr)
        {
            switch (vr)
            {
                case VR.UniqueIdentifier:
                case VR.OtherByteString:
                case VR.Unknown:
                    return DataPadder.PadNull(data);
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
                    return DataPadder.PadSpace(data);
                default: return data;
            }

        }
    }
}

