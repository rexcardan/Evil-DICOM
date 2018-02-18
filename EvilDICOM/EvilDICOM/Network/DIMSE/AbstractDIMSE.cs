#region

using EvilDICOM.Core;
using EvilDICOM.Network.Enums;

#endregion

namespace EvilDICOM.Network.DIMSE
{
    public abstract class AbstractDIMSE : AbstractDIMSEBase
    {
        private DICOMObject _data;

        public bool HasData
        {
            get
            {
                return DataSetType != (ushort)CommandDataSetType.EMPTY; //0x01 0x01
            }
        }

        public DICOMObject Data
        {
            get { return _data; }
            set
            {
                _data = value;
                DataSetType = value != null ? (ushort)CommandDataSetType.HAS_DATA : (ushort)CommandDataSetType.EMPTY;
            }
        }


        //TODO Move this to the association class
        internal int DataPresentationContextId { get; set; }
    }
}