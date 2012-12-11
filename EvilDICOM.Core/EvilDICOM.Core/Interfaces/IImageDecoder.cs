using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilDICOM.Core.Interfaces
{
    /// <summary>
    /// Contains inteface methods to decode and encode
    /// </summary>
    public interface IEncoderDecoder
    {
        byte[] DecodeToBMP(byte[] pixelData);
        byte[] Encode(byte[] bmp);
    }
}
