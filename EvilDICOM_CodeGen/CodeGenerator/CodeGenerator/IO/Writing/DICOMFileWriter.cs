using System.IO;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMFileWriter
    {
        /// <summary>
        ///     Writes DICOM file out as a file of a specified path
        /// </summary>
        /// <param name="filePath">the path to which to write the file</param>
        /// <param name="settings">the write settings</param>
        /// <param name="toWrite">the object to write</param>
        public static void Write(string filePath, DICOMWriteSettings settings, DICOMObject toWrite)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                Write(fs, settings, toWrite);
            }
        }

        /// <summary>
        ///     Write DICOM file out (bytes) to a specified stream
        /// </summary>
        /// <param name="stream">the stream to which to write the file</param>
        /// <param name="settings">the write settings</param>
        /// <param name="toWrite">the object to write</param>
        public static void Write(Stream stream, DICOMWriteSettings settings, DICOMObject toWrite)
        {
            settings = settings ?? DICOMWriteSettings.Default();
            using (var dw = new DICOMBinaryWriter(stream))
            {
                DICOMPreambleWriter.Write(dw);
                TransferSyntaxHelper.SetSyntax(toWrite, settings.TransferSyntax);
                DICOMObjectWriter.Write(dw, settings, toWrite);
            }
        }
    }
}