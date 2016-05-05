using System;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;
using EvilDICOM.Core.Element;

namespace EvilDICOM.Core.IO.Reading
{
    public class VRReader
    {
        public static VR Read(Tag t, DICOMBinaryReader dr)
        {
            char[] vr = dr.ReadChars(2);
            VR foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            if (foundVR == VR.Null)
            {
                var msg = string.Format("({0},{1}) : {2}", t.Group, t.Element, ExceptionHelper.VRReadException);
                throw new Exception(msg);
            }
            return foundVR;
        }
    }
}