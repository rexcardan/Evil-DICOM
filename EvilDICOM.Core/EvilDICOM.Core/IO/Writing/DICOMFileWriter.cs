using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMFileWriter
    {
        public static void WriteLittleEndian(string filePath, DICOMWriteSettings settings, DICOMObject toWrite)
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
            WriteLittleEndian(filePath, DICOMWriteSettings.Default(), toWrite);
        }
    }
}
