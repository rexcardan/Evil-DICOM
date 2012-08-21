using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Writing
{
    public class VRWriter
    {
        public static void WriteVR(DICOMBinaryWriter dw, DICOMWriteSettings settings, IDICOMElement toWrite)
        {
            if (!(settings.TransferSyntax == TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN))
            {
                VR vr = VRDictionary.GetVRFromType(toWrite);
                string abbreviation = VRDictionary.GetAbbreviationFromVR(vr);
                dw.Write(abbreviation);
            }
        }

        public static void WriteVR(DICOMBinaryWriter dw, DICOMWriteSettings settings, VR vr)
        {
            if (!(settings.TransferSyntax == TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN))
            {
                string abbreviation = VRDictionary.GetAbbreviationFromVR(vr);
                dw.Write(abbreviation);
            }
        }
    }
}
