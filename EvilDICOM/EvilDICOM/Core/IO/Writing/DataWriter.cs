using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.IO.Writing
{
    public class DataWriter
    {
        public static void WriteLittleEndian(DICOMBinaryWriter dw, VR vr, DICOMWriteSettings settings,
            IDICOMElement toWrite)
        {
            byte[] data = DataComposer.GetDataLittleEndian(toWrite);
            LengthWriter.Write(dw, vr, settings, data != null ? data.Length : 0);
            dw.Write(data != null ? data : new byte[0]);
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, VR vr, DICOMWriteSettings settings,
            IDICOMElement toWrite)
        {
            byte[] data = DataComposer.GetDataBigEndian(toWrite);
            LengthWriter.Write(dw, vr, settings, data != null ? data.Length : 0);
            dw.Write(data != null ? data : new byte[0]);
        }
    }
}