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
        public static void Write(string filePath, DICOMWriteSettings settings, DICOMObject toWrite)
        {
            using (DICOMBinaryWriter dw = new DICOMBinaryWriter(filePath))
            {
                DICOMPreambleWriter.Write(dw);
                TransferSyntaxHelper.SetSyntax(toWrite, settings.TransferSyntax);
                DICOMObjectWriter.WriteObjectLittleEndian(dw, settings, toWrite);
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
