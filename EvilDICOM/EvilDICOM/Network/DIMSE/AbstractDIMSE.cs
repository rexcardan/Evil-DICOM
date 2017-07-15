#region

using EvilDICOM.Core;
using C = EvilDICOM.Network.Enums.CommandField;

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
                return DataSetType != 257; //0x01 0x01
            }
        }

        public DICOMObject Data
        {
            get { return _data; }
            set
            {
                _data = value;
                DataSetType = value != null ? (ushort) 0 : (ushort) 257;
            }
        }


        //TODO Move this to the association class
        internal int DataPresentationContextId { get; set; }
    }
}