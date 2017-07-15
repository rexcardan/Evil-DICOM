#region

using EvilDICOM.Core.Enums;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the OtherFloatString VR type
    /// </summary>
    public class OtherLongString : AbstractElement<byte>
    {
        public OtherLongString()
        {
            VR = VR.OtherLongString;
        }

        public OtherLongString(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.OtherLongString;
        }
    }
}