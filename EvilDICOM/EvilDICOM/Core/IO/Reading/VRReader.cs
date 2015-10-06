using System;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;

namespace EvilDICOM.Core.IO.Reading
{
    public class VRReader
    {
        public static VR Read(DICOMBinaryReader dr)
        {
            char[] vr = dr.ReadChars(2);
            VR foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            if (foundVR == VR.Null)
            {
                throw new Exception(ExceptionHelper.VRReadException);
            }
            return foundVR;
        }
    }
}