using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the UniqueIdentifier VR type
    /// </summary>
    public class UniqueIdentifier : AbstractElement<string>
    {  /// <summary>
        /// Data is overriden to enforce length restriction
        /// </summary>
        public new string Data
        {
            get { return base.DataContainer.SingleValue; }
            set { base.DataContainer = base.DataContainer ?? new DICOMData<string>(); base.DataContainer.SingleValue = DataRestriction.EnforceLengthRestriction(64, value); }
        }

        public UniqueIdentifier()
            : base()
        {
            VR = Enums.VR.UniqueIdentifier;
        }

        public UniqueIdentifier(Tag tag, string data)
            : base()
        {
            Tag = tag;
            Data = data;
            VR = Enums.VR.UniqueIdentifier;
        }
    }
}
