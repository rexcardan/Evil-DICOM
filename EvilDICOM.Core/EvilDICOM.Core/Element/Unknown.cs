using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the Unknown VR type
    /// </summary>
    public class Unknown : AbstractElement<byte>
    {
        public Unknown()
        {
        }

        public Unknown(Tag tag, byte[] data)
            : base(tag, data)
        {
            VR = VR.Unknown;
        }
    }
}