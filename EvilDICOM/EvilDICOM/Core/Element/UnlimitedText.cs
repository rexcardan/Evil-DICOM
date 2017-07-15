#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the UnlimitedText VR type
    /// </summary>
    public class UnlimitedText : AbstractElement<string>
    {
        public UnlimitedText()
        {
            VR = VR.UnlimitedText;
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
            get { return DataContainer.SingleValue; }
            set
            {
                DataContainer = DataContainer ?? new DICOMData<string>();
                DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value);
            }
        }
    }
}