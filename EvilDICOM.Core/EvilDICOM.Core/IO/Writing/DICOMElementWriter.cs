using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using System.IO;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.IO.Writing
{
    public class DICOMElementWriter
    {
        public static void WriteLittleEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, IDICOMElement toWrite)
        {
            VR vr = VRDictionary.GetVRFromType(toWrite);
            if (vr == VR.Sequence)
            {
                SequenceWriter.WriteLittleEndian(dw, settings, toWrite);
            }
            else
            {
                DICOMTagWriter.WriteLittleEndian(dw, toWrite.Tag);
                VRWriter.WriteVR(dw, settings, vr);               
                DataWriter.WriteLittleEndian(dw, vr, settings, toWrite);
            }
           
        }

        public static void WriteBigEndian(DICOMBinaryWriter dw, DICOMWriteSettings settings, IDICOMElement toWrite)
        {
            DICOMTagWriter.WriteBigEndian(dw, toWrite.Tag);
            VR vr = VRDictionary.GetVRFromType(toWrite);
            VRWriter.WriteVR(dw, settings, vr);
            DataWriter.WriteBigEndian(dw, vr, settings, toWrite);
        }

        public static void Write(DICOMBinaryWriter dw, DICOMWriteSettings settings, IDICOMElement el)
        {
            if (settings.TransferSyntax == TransferSyntax.EXPLICIT_VR_BIG_ENDIAN)
            {
                WriteBigEndian(dw, settings, el);
            }
            else
            {
                WriteLittleEndian(dw, settings, el);
            }
        }
    }
}
