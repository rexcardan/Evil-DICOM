using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the LongText VR type
    /// </summary>
    public class LongText : AbstractElement<string>
    {
        public LongText()
        {
            VR = VR.LongText;
        }

        public LongText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.LongText;
        }

        /// <summary>
        ///     Data overriden for enforcing length restriction
        /// </summary>
        public override string Data
        {
            get { return base.DataContainer.SingleValue; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<string>();
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(10240, value);
            }
        }
    }
}