using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the UnlimitedText VR type
    /// </summary>
    public class UnlimitedText : AbstractElement<string>
    {
        public UnlimitedText()
        {
        }

        public UnlimitedText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.UnlimitedText;
        }

        /// <summary>
        ///     Data is overriden to enforce length restriction
        /// </summary>
        public override string Data
        {
            get { return base.DataContainer.SingleValue; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<string>();
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value);
            }
        }
    }
}