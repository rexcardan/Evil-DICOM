using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMFileWriter
    {
        /// <summary>
        /// Writes DICOM file out as a file of a specified path
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
        /// Write DICOM file out (bytes) to a specified stream
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

        public static void WriteLittleEndian(string filePath, DICOMObject toWrite)
        {
            var settings = DICOMWriteSettings.Default();
            var currentUID = toWrite.FindFirst(TagHelper.TRANSFER_SYNTAX_UID);
            if (currentUID != null) { settings.TransferSyntax = TransferSyntaxHelper.GetSyntax(currentUID); }

            //TODO Currently don't support BigEndian writing : switch syntax to supported
            if (settings.TransferSyntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
                settings.TransferSyntax = TransferSyntax.EXPLICIT_VR_LITTLE_ENDIAN;
            }

            Write(filePath, settings, toWrite);
        }
    }
}
