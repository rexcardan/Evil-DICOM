using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the LongString VR type
    /// </summary>
    public class LongString : AbstractElement<string>
    {
        public LongString()
        {
            VR = VR.LongString;
        }

        public LongString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.LongString;
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
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(64, value);
            }
        }
    }
}