using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDicom.Components;

namespace EvilDicom.VR
{
    public class NullVR : DICOMElement
    {
        public NullVR(byte[] bytes)
        {
            this.Tag = null;
            ByteData = bytes;
            EncodeType = Constants.EncodeType.IMPLICIT;
        }

        public override int Length
        {
            get
            {
                return ByteData.Length;
            }
        }
    }
}


//Copyright © 2012 Rex Cardan, Ph.D


