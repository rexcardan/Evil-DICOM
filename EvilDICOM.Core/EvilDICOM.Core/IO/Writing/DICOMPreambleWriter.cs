using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EvilDICOM.Core.IO.Writing;

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
