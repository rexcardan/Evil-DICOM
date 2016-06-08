using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the AgeString VR type
    /// </summary>
    /// <seealso cref="EvilDICOM.Core.Element.AbstractElement{System.String}" />
    public class AgeString : AbstractElement<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgeString"/> class.
        /// </summary>
        public AgeString()
        {
            VR = VR.AgeString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgeString"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="data">The data.</param>
        public AgeString(Tag tag, string data)
            : base(tag, data)
        {
            VR = VR.AgeString;
        }

        /// <summary>
        /// The age stored in the element. Supplements the data property.
        /// </summary>
        /// <value>The age.</value>
        public Age Age
        {
            get { return StringDataParser.ParseAgeString(DataContainer.SingleValue); }
            set { DataContainer.SingleValue = StringDataComposer.ComposeAgeString(value); }
        }
    }

    /// <summary>
    /// A small class to help manipulate the age in the AgeString class
    /// </summary>
    public class Age
    {
        /// <summary>
        /// Enum Unit
        /// </summary>
        public enum Unit
        {
            /// <summary>
            /// The days
            /// </summary>
            DAYS,
            /// <summary>
            /// The weeks
            /// </summary>
            WEEKS,
            /// <summary>
            /// The months
            /// </summary>
            MONTHS,
            /// <summary>
            /// The years
            /// </summary>
            YEARS
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number { get; set; }
        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        public Unit Units { get; set; }
    }
}