using System.Collections.Generic;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

namespace EvilDICOM.Core.IO.Reading
{
    public class DICOMObjectReader
    {
        public static DICOMObject ReadObject(DICOMBinaryReader dr, TransferSyntax syntax)
        {
            List<IDICOMElement> elements = DICOMElementReader.ReadAllElements(dr, syntax);
            return new DICOMObject(elements);
        }

        public static DICOMObject ReadObject(byte[] objectBytes, TransferSyntax syntax)
        {
            long bytesRead;
            return ReadObject(objectBytes, syntax, out bytesRead);
        }

        public static DICOMObject ReadObject(byte[] objectBytes, TransferSyntax syntax, out long bytesRead)
        {
            var dcm = DICOMObject.Read(objectBytes, TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN);
            List<IDICOMElement> elements;
            using (var dr = new DICOMBinaryReader(objectBytes))
            {
                elements = DICOMElementReader.ReadAllElements(dr, syntax);
                bytesRead = dr.StreamPosition;
            }
            return new DICOMObject(elements);
        }
    }
}