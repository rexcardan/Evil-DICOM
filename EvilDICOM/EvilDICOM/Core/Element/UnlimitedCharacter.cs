#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    public class UnlimitedCharacter : AbstractElement<string>
    {
        public UnlimitedCharacter()
        {
            VR = VR.UnlimitedCharacter;
        }

        public UnlimitedCharacter(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.UnlimitedCharacter;
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