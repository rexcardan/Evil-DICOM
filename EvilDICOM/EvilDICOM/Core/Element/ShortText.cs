#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the ShortText VR type
    /// </summary>
    public class ShortText : AbstractElement<string>
    {
        public ShortText()
        {
            VR = VR.ShortText;
        }

        public ShortText(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.ShortText;
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
                DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(1024, value);
            }
        }
    }
}