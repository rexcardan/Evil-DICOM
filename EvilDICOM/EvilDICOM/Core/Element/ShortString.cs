using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    public class ShortString : AbstractElement<string>
    {
        public ShortString()
        {
        }

        public ShortString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.ShortString;
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
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(16, value);
            }
        }
    }
}