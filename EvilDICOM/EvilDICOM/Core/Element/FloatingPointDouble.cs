using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the FloatingPointDouble VR type
    /// </summary>
    public class FloatingPointDouble : AbstractElement<double>
    {
        public FloatingPointDouble()
        {
            VR = VR.FloatingPointDouble;
        }

        public FloatingPointDouble(Tag tag, double data)
            : base(tag, data)
        {
            VR = VR.FloatingPointDouble;
        }

        public FloatingPointDouble(Tag tag, double[] data)
            : base(tag, data)
        {
            VR = VR.FloatingPointDouble;
        }
    }
}