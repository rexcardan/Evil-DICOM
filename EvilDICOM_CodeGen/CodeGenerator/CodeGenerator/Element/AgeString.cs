using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the AgeString VR type
    /// </summary>
    public class AgeString : AbstractElement<string>
    {
        public AgeString()
        {
            VR = VR.AgeString;
        }

        public AgeString(Tag tag, string data)
            : base(tag, data)
        {
            VR = VR.AgeString;
        }

        /// <summary>
        ///     The age stored in the element. Supplements the data property.
        /// </summary>
        public Age Age
        {
            get { return StringDataParser.ParseAgeString(DataContainer.SingleValue); }
            set { DataContainer.SingleValue = StringDataComposer.ComposeAgeString(value); }
        }
    }

    /// <summary>
    ///     A small class to help manipulate the age in the AgeString class
    /// </summary>
    public class Age
    {
        public enum Unit
        {
            DAYS,
            WEEKS,
            MONTHS,
            YEARS
        }

        public int Number { get; set; }
        public Unit Units { get; set; }
    }
}