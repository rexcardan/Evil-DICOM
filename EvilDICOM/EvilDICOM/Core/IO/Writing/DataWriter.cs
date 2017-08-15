#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class DataWriter
    {
        public static void WriteLittleEndian(DICOMBinaryWriter dw, VR vr, DICOMIOSettings settings,
            IDICOMElement toWrite)
        {
            var data = DataComposer.GetDataLittleEndian(toWrite, settings.StringEncoding);
            LengthWriter.Write(dw, vr, settings, data != null ? data.Length : 0);
            dw.Write(data != null ? data : new byte[0]);
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, VR vr, DICOMIOSettings settings,
            IDICOMElement toWrite)
        {
            var data = DataComposer.GetDataBigEndian(toWrite, settings.StringEncoding);
            LengthWriter.Write(dw, vr, settings, data != null ? data.Length : 0);
            dw.Write(data != null ? data : new byte[0]);
        }
    }
}