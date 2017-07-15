#region

using System.Collections.Generic;
using EvilDICOM.Core.Enums;

#endregion

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
        public List<DICOMObject> Items
        {
            get { return DataContainer != null ? DataContainer.MultipicityValue : null; }
            set
            {
                DataContainer = DataContainer ?? new DICOMData<DICOMObject>();
                DataContainer.MultipicityValue = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Tag, VR, string.Format(" : {0} Items", Items.Count));
        }
    }
}