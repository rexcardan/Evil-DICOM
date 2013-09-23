using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.IO.Data;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    /// Encapsulates the Sequence VR type
    /// </summary>
    public class Sequence : AbstractElement<DICOMObject>
    {
        public Sequence()
            : base()
        {
            VR = Enums.VR.Sequence;
            Items = new List<DICOMObject>();
        }
        /// <summary>
        /// Alternate property name for data (with a clearer name)
        /// </summary>
        public List<DICOMObject> Items
        {
            get
            {
                return base.Data != null ? Data.MultipicityValue : null;
            }
            set
            {
                base.Data = base.Data ?? new DICOMData<DICOMObject>(); base.Data.MultipicityValue = value;
            }
        }
    }
}
