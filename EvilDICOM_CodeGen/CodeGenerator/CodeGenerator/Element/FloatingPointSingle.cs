using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the FloatingPointSingle VR type
    /// </summary>
    public class FloatingPointSingle : AbstractElement<float>
    {
        public FloatingPointSingle()
        {
            VR = VR.FloatingPointSingle;
        }

        public FloatingPointSingle(Tag tag, float data)
            : base(tag, data)
        {
            VR = VR.FloatingPointSingle;
        }

        public FloatingPointSingle(Tag tag, float[] data)
            : base(tag, data)
        {
            VR = VR.FloatingPointSingle;
        }
    }
}