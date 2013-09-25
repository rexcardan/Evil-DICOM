using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the ShortText VR type
    /// </summary>
    public class ShortText : AbstractElement<string>
    {  /// <summary>
        /// Data is overriden to enforce length restriction
        /// </summary>
        public new string Data
        {
            get { return base.DataContainer.SingleValue; }
            set { base.DataContainer = base.DataContainer?? new DICOMData<string>(); base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(1024, value); }
        }

        public ShortText() : base() { VR = Enums.VR.ShortText; }

        public ShortText(Tag tag, string data)
            : base()
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.ShortText;
        }
    }
}