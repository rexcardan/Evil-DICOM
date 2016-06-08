using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the DecimalString VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{System.Double}" />
    public class DecimalString : AbstractElement<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalString"/> class.
        /// </summary>
        public DecimalString()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public DecimalString(Tag tag, string data)
            : base(tag, StringDataParser.ParseDecimalString(data))
        {
            VR = VR.DecimalString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public DecimalString(Tag tag, double data)
            : base(tag, data)
        {
            VR = VR.DecimalString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public DecimalString(Tag tag, double[] data)
            : base(tag, data)
        {
            VR = VR.DecimalString;
        }
    }
}