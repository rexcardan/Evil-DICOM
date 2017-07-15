#region

using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Interfaces;

#endregion

namespace EvilDICOM.Core.IO.Writing
{
    public class VRWriter
    {
        public static void WriteVR(DICOMBinaryWriter dw, DICOMWriteSettings settings, IDICOMElement toWrite)
        {
            if (!(settings.TransferSyntax == TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN))
            {
                var vr = VRDictionary.GetVRFromType(toWrite);
                var abbreviation = VRDictionary.GetAbbreviationFromVR(vr);
                dw.Write(abbreviation);
            }
        }

        public static void WriteVR(DICOMBinaryWriter dw, DICOMWriteSettings settings, VR vr)
        {
            if (settings.TransferSyntax != TransferSyntax.IMPLICIT_VR_LITTLE_ENDIAN)
            {
                var abbreviation = VRDictionary.GetAbbreviationFromVR(vr);
                dw.Write(abbreviation);
            }
        }
    }
}