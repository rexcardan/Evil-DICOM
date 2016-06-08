namespace EvilDICOM.Core.IO.Writing
{
    /// <summary>
    /// Class DICOMPreambleWriter.
    /// </summary>
    public static class DICOMPreambleWriter
    {
        /// <summary>
        /// Writes the specified dw.
        /// </summary>
        /// <param name="dw">The dw.</param>
        public static void Write(DICOMBinaryWriter dw)
        {
            dw.WriteNullBytes(128);
            dw.Write("DICM");
        }
    }
}