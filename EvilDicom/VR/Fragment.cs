using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Helper;
using EvilDicom.Components;

namespace EvilDicom.VR
{
    public class Fragment:DICOMElement
    {
        internal void WriteBytes(System.IO.BinaryWriter b, bool isLittleEndian)
        {
            DICOMWriter.WriteTag(b, new Tag(TagHelper.SEQUENCE_ITEM), isLittleEndian);
            DICOMWriter.WriteLength(b,Constants.EncodeType.IMPLICIT, ByteData.Length,isLittleEndian);
            DICOMWriter.WriteBytes(b, ByteData, isLittleEndian);
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


