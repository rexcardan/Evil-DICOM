using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the UnlimitedText VR type
    /// </summary>
    public class UnlimitedText : AbstractElement<string>
    {
        /// <summary>
        /// Data is overriden to enforce length restriction
        /// </summary>
        public new string Data
        {
            get { return base.Data.SingleValue; }
            set { base.Data = base.Data ?? new DICOMData<string>(); base.Data.SingleValue = DataRestriction.EnforceLengthRestriction(uint.MaxValue - 1, value); }
        }

        public UnlimitedText() : base() { }

        public UnlimitedText(Tag tag, string data)
            : base()
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.UnlimitedText;
        }
    }
}