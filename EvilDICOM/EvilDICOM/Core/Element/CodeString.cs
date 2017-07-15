#region

using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

#endregion

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the CodeString VR type
    /// </summary>
    public class CodeString : AbstractElement<string>
    {
        public CodeString()
        {
            VR = VR.CodeString;
        }

        public CodeString(Tag tag, string data)
        {
            Tag = tag;
            Data = data;
            VR = VR.CodeString;
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
                DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(50, value);
            }
        }
    }
}