using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the IntegerString VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{System.Int32}" />
    public class IntegerString : AbstractElement<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerString"/> class.
        /// </summary>
        public IntegerString()
        {
            VR = VR.IntegerString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public IntegerString(Tag tag, string data)
            : base(tag, StringDataParser.ParseIntegerString(data))
        {
            VR = VR.IntegerString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public IntegerString(Tag tag, int data)
            : base(tag, data)
        {
            VR = VR.IntegerString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public IntegerString(Tag tag, int[] data)
            : base(tag, data)
        {
            VR = VR.IntegerString;
        }
    }
}