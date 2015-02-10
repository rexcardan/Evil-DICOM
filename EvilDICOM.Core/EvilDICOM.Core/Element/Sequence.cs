using System.Collections.Generic;
using EvilDICOM.Core.Enums;

namespace EvilDICOM.Core.Element
{
    /// <summary>
    ///     Encapsulates the Sequence VR type
    /// </summary>
    public class Sequence : AbstractElement<DICOMObject>
    {
        public Sequence()
        {
            VR = VR.Sequence;
            Items = new List<DICOMObject>();
        }

        /// <summary>
        ///     Alternate property name for data (with a clearer name)
        /// </summary>
        public virtual List<DICOMObject> Items
        {
            get { return base.DataContainer != null ? DataContainer.MultipicityValue : null; }
            set
            {
                base.DataContainer = base.DataContainer ?? new DICOMData<DICOMObject>();
                base.DataContainer.MultipicityValue = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Tag, VR, string.Format(" : {0} Items", Items.Count));
        }
    }
}