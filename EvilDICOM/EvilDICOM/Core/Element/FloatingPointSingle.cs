using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the FloatingPointSingle VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{System.Single}" />
    public class FloatingPointSingle : AbstractElement<float>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatingPointSingle"/> class.
        /// </summary>
        public FloatingPointSingle()
        {
            VR = VR.FloatingPointSingle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatingPointSingle"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public FloatingPointSingle(Tag tag, float data)
            : base(tag, data)
        {
            VR = VR.FloatingPointSingle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatingPointSingle"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public FloatingPointSingle(Tag tag, float[] data)
            : base(tag, data)
        {
            VR = VR.FloatingPointSingle;
        }
    }
}