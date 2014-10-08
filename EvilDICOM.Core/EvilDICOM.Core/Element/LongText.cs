using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the LongText VR type
    /// </summary>
    public class LongText : AbstractElement<string>
    {  
        /// <summary>
        /// Data overriden for enforcing length restriction
        /// </summary>
        public override string Data
        {
            get { return base.DataContainer.SingleValue; }
            set { base.DataContainer = base.DataContainer ?? new DICOMData<string>(); base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(10240, value); }
        }

        public LongText() : base() { VR = Enums.VR.LongText; }

        public LongText(Tag tag, string data) :base()
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.LongText;
        }
    }
}