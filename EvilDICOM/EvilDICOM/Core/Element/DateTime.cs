using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the DateTime VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{System.DateTime?}" />
    public class DateTime : AbstractElement<System.DateTime?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTime"/> class.
        /// </summary>
        public DateTime()
        {
            VR = VR.DateTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTime"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public DateTime(Tag tag, string data)
            : base(tag, StringDataParser.ParseDateTime(data))
        {
            VR = VR.DateTime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTime"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public DateTime(Tag tag, System.DateTime? data)
            : base(tag, data)
        {
            VR = VR.DateTime;
        }
    }
}