namespace EvilDICOM.Core.IO.Writing
{
    public static class DICOMPreambleWriter
    {
        public static void Write(DICOMBinaryWriter dw)
        {
            dw.WriteNullBytes(128);
            dw.Write("DICM");
        }
    }
}