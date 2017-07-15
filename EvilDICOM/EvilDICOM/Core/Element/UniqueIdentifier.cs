#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the UniqueIdentifier VR type
    /// </summary>
    public class UniqueIdentifier : AbstractElement<string>
    {
        public UniqueIdentifier()
        {
            VR = VR.UniqueIdentifier;
        }

        public UniqueIdentifier(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.UniqueIdentifier;
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
                DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(64, value);
            }
        }
    }
}