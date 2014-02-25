using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvilDICOM.Core.Interfaces;
using EvilDICOM.Core.Enums;
using EvilDICOM.Core.IO.Writing;
using EvilDICOM.Core.IO.Data;
using EvilDICOM.Core.Dictionaries;

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
                return base.DataContainer != null ? DataContainer.MultipicityValue : null;
            }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<DICOMObject>(); base.DataContainer.MultipicityValue = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Tag.ToString(), VR.ToString(), string.Format(" : {0} Items", Items.Count));
        }
    }
}
