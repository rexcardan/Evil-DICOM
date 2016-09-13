using System;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using System.Text;

namespace EvilDICOM.Core.IO.Reading
{
    public class VRReader
    {
        public static VR ReadVR(DICOMBinaryReader dr)
        {
            char[] vr = dr.ReadChars(2);
            VR foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            if (foundVR == VR.Null)
            {
                throw new Exception(ExceptionHelper.VRReadException);
            }
            return foundVR;
        }

        public static VR PeekVR(DICOMBinaryReader dr)
        {
            byte[] vrBytes = dr.Peek(2);
            char[] vr = Encoding.UTF8.GetChars(vrBytes);
            VR foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            return foundVR;
        }
    }
}