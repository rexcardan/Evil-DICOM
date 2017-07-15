#region

using System;
using System.Text;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.Helpers;

#endregion

namespace EvilDICOM.Core.IO.Reading
{
    public class VRReader
    {
        public static VR ReadVR(DICOMBinaryReader dr)
        {
            var vr = dr.ReadChars(2);
            var foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            if (foundVR == VR.Null)
                throw new Exception(ExceptionHelper.VRReadException);
            return foundVR;
        }

        public static VR PeekVR(DICOMBinaryReader dr)
        {
            var vrBytes = dr.Peek(2);
            var vr = Encoding.UTF8.GetChars(vrBytes);
            var foundVR = VRDictionary.GetVRFromAbbreviation(new string(vr));
            return foundVR;
        }
    }
}