using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Data;

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
            get { return base.DataContainer.SingleValue; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<string>();
                base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value);
            }
        }
    }
}
