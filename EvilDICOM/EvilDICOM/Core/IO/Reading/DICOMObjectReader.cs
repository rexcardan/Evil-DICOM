#region

using System.Collections.Generic;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Reading
{
    public class DICOMObjectReader
    {
        public static DICOMObject ReadObject(DICOMBinaryReader dr, TransferSyntax syntax, StringEncoding enc = StringEncoding.ISO_IR_192)
        {
            var elements = DICOMElementReader.ReadAllElements(dr, syntax, enc);
            return new DICOMObject(elements);
        }

        public static DICOMObject ReadObject(byte[] objectBytes, TransferSyntax syntax)
        {
            long bytesRead;
            return ReadObject(objectBytes, syntax, out bytesRead);
        }

        public static DICOMObject ReadObject(byte[] objectBytes, TransferSyntax syntax, out long bytesRead, StringEncoding enc = StringEncoding.ISO_IR_192)
        {
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(objectBytes))
            {
                elements = DICOMElementReader.ReadAllElements(dr, syntax, enc);
                bytesRead = dr.StreamPosition;
            }
            return new DICOMObject(elements);
        }
    }
}